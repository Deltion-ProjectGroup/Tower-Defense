using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Berserker : Enemy {
    [Header("RageBuff")]
    public EnrageBuff enrageBuff;
    public GameObject rageParticles;
    bool enraged;
    bool dead;
    // Use this for initialization

    public override void CheckHealth()
    {
        if (health < maxHealth)
        {
            if (!damaged)
            {
                damaged = true;
                healthbarHolder.SetActive(true);
            }
            healthbar.GetComponent<Image>().fillAmount = (1 / maxHealth) * health;
        }
        if (health <= maxHealth * 0.2f && !enraged)
        {
            enraged = true;
            Instantiate(rageParticles, transform.position, Quaternion.identity, gameObject.transform);
            movementspeed += enrageBuff.rageSpeedBuff;
            GetComponent<NavMeshAgent>().speed = movementspeed;
            attackSpeed += enrageBuff.rageAttackspeedBuff;
            health += enrageBuff.rageHealthBuff;
            maxHealth += enrageBuff.rageHealthBuff;
            damage += enrageBuff.rageDamageBuff;
        }
        if (health <= 0 && !dead)
        {
            dead = true;
            for (int i = 0; i < targettedBy.Count; i++)
            {
                targettedBy[i].GetComponent<Turret>().CleanTarget(gameObject);
                LevelManager.levelManager.RemoveEnemy(gameObject);
            }
            if (attacking)
            {
                target.GetComponent<Obstacle>().RemoveUnit(gameObject);
            }
            StopAllCoroutines();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponentInChildren<ParticleSystem>().Stop();
            LevelManager.levelManager.AddCurrency(worthCurrency);
            Destroy(gameObject, gameObject.GetComponentInChildren<ParticleSystem>().startLifetime);
        }
    }
    public override IEnumerator Attack()
    {
        base.Attack();
        while (true)
        {
            //yield return new WaitForSeconds(attackAnim.clip.length);
            target.GetComponent<Obstacle>().health -= damage;
            target.GetComponent<Obstacle>().CheckHealth();
            yield return new WaitForSeconds(1 / attackSpeed);
        }
    }
    [System.Serializable]
    public struct EnrageBuff
    {
        public float rageSpeedBuff;
        public float rageHealthBuff;
        public float rageDamageBuff;
        public float rageAttackspeedBuff;
    }
}
