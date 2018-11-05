using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public override IEnumerator Attack()
    {
        base.Attack();
        while (true)
        {
            yield return new WaitForSeconds(attackTimeMarks[0] / baseAttackSpeed);
            audioSources[0].clip = attackSounds[Random.Range(0, attackSounds.Length)];
            audioSources[1].Play();
            audioSources[0].Play();
            RaycastHit hit;
            //Vector3 hitpoint = new Vector3(Random.Range((target.transform.position.x - hitScatterModifier), (target.transform.position.x + hitScatterModifier)), Random.Range((target.transform.position.y - hitScatterModifier), (target.transform.position.y + hitScatterModifier)), target.transform.position.z);
            if (Physics.Raycast(heart.transform.position, transform.forward, out hit, 1000000, targettable, QueryTriggerInteraction.Ignore))
            {
                GameObject hitEffect = Instantiate(impactParticle, hit.point, impactParticle.transform.rotation);
                Destroy(hitEffect, 3);
            }
            target.GetComponent<Obstacle>().health -= damage;
            target.GetComponent<Obstacle>().CheckHealth();
            yield return new WaitForSeconds((attackTimeMarks[1] - attackTimeMarks[0]) / baseAttackSpeed);
        }
    }
}