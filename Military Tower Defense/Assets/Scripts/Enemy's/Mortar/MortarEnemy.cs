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
        distance *= 0.283f;
        force.y = 17;
        print(distance);
        force.z = distance;
        GameObject shot = Instantiate(ball, spawnPosition, Quaternion.identity);
        shot.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        yield return new WaitForSeconds(1 / attackSpeed);
        StartCoroutine(Attack());
    }
}
