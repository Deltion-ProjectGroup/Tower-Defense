using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Berserker : Enemy {
    public float rageSpeedBuff;
    public float rageHealthBuff;
    public float rageDamageBuff;
    public float rageAttackspeedBuff;
    bool enraged;
    public Animation attackAnim;
    // Use this for initialization

    public override IEnumerator Attack()
    {

        yield return new WaitForSeconds(attackAnim.clip.length);
        //target.GetComponent<Obstacle>().health -= damage;
        yield return new WaitForSeconds(1 / attackSpeed);
        StartCoroutine(Attack());
    }
    public override void HealthCheck()
    {
        if(health <= maxHealth * 0.2f && !enraged)
        {
            enraged = true;
            movementspeed += rageSpeedBuff;
            GetComponent<NavMeshAgent>().speed = movementspeed;
            attackSpeed += rageAttackspeedBuff;
            health += rageHealthBuff;
            maxHealth += rageHealthBuff;
            damage += rageDamageBuff;
        }
        base.HealthCheck();
    }
}
