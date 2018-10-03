using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarEnemy : Enemy {
    Vector3 spawnPosition;
    public GameObject ball;
    Vector3 force;
    // Use this for initialization

    public override IEnumerator Attack()
    {
        //yield return WaitForSeconds(animation);
        spawnPosition = transform.position;
        spawnPosition.y += 1.5f;
        float distance = Vector3.Distance(target.transform.position, transform.position);
        distance *= 0.28f; //287
        force.y = 17;
        force.z = distance;
        GameObject shot = Instantiate(ball, spawnPosition, transform.rotation);
        shot.GetComponent<Rigidbody>().AddRelativeForce(force, ForceMode.Impulse);
        yield return new WaitForSeconds(1 / attackSpeed);
        StartCoroutine(Attack());
    }
}
