using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Berserker : Enemy {
    [Header("RageBuff")]
    public EnrageBuff enrageBuff;
    public GameObject rageParticles;
    bool enraged = false;
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
            audioSources[1].Play();
            enraged = true;
            Instantiate(rageParticles, transform.position, Quaternion.identity, gameObject.transform);
            movementspeed += enrageBuff.rageSpeedBuff;
            GetComponent<NavMeshAgent>().speed = movementspeed;
            attackSpeed += enrageBuff.rageAttackspeedBuff;
            GetComponent<Animator>().SetFloat("AttackSpeed", attackSpeed);
            health += enrageBuff.rageHealthBuff;
            maxHealth += enrageBuff.rageHealthBuff;
            damage += enrageBuff.rageDamageBuff;
        }
        if (health <= 0 && !dead)
        {
            StopAllCoroutines();
            //gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            Collider[] colliders = GetComponents<Collider>();
            for(int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = false;
            }
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponentInChildren<ParticleSystem>().Stop();
            LevelManager.levelManager.AddCurrency(worthCurrency, transform.position);
            GetComponent<Animator>().SetBool("canAttack", false);
            GetComponent<Animator>().SetBool("canWalk", false);
            GetComponent<Animator>().SetBool("canDie", true);
            StartCoroutine(Death());
        }
    }
    public override IEnumerator Attack()
    {
        base.Attack();
        while (true)
        {
            yield return new WaitForSeconds(attackTimeMarks[0] / baseAttackSpeed);
            audioSources[0].clip = attackSounds[Random.Range(0, attackSounds.Length)];
            audioSources[0].Play();
            target.GetComponent<Obstacle>().health -= damage;
            target.GetComponent<Obstacle>().CheckHealth();
            yield return new WaitForSeconds((attackTimeMarks[1] - attackTimeMarks[0]) / baseAttackSpeed);
            audioSources[0].clip = attackSounds[Random.Range(0, attackSounds.Length)];
            audioSources[0].Play();
            target.GetComponent<Obstacle>().health -= damage;
            target.GetComponent<Obstacle>().CheckHealth();
            yield return new WaitForSeconds((attackTimeMarks[2] - (attackTimeMarks[1] + attackTimeMarks[0])) / baseAttackSpeed);
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
