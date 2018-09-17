using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Berserker : Enemy {
    public EnrageBuff enrageBuff;
    bool enraged;
    public GameObject rageParticles;
    // Use this for initialization

    public override IEnumerator Attack()
    {
        target.GetComponent<Obstacle>().targettedBy.Add(gameObject);
        while (true)
        {
            //yield return new WaitForSeconds(attackAnim.clip.length);
            target.GetComponent<Obstacle>().health -= damage;
            target.GetComponent<Obstacle>().CheckHealth();
            yield return new WaitForSeconds(1 / attackSpeed);
            print("Attack");
        }
    }
    public override void HealthCheck()
    {
        if(health <= maxHealth * 0.2f && !enraged)
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
        if(health <= 0)
        {
            StopAllCoroutines();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponentInChildren<ParticleSystem>().Stop();
            for (int i = 0; i < targettedBy.Count; i++)
            {
                targettedBy[i].GetComponent<Turret>().targets.Remove(gameObject);
            }
            Destroy(gameObject, gameObject.GetComponentInChildren<ParticleSystem>().startLifetime);
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
