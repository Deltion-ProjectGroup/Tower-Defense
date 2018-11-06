using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarEnemy : Enemy {
    Vector3 spawnPosition;
    public GameObject ball;
    public float distanceModifier;
    Vector3 force;
    // Use this for initialization

    public override IEnumerator Attack()
    {
        while (attacking && target != null)
        {
            yield return new WaitForSeconds(attackTimeMarks[0]);
            if (attacking && target != null)
            {
                audioSources[0].clip = attackSounds[Random.Range(0, attackSounds.Length)];
                audioSources[0].Play();
                spawnPosition = transform.position;
                spawnPosition.y += 1.5f;
                float distance = Vector3.Distance(target.transform.position, transform.position);
                distance *= distanceModifier; //287
                force.y = 17;
                force.z = distance;
                GameObject shot = Instantiate(ball, spawnPosition, transform.rotation);
                shot.GetComponent<Rigidbody>().AddRelativeForce(force, ForceMode.Impulse);
            }
            yield return new WaitForSeconds((attackTimeMarks[1] - attackTimeMarks[0]) / attackSpeed);
        }
    }
}
