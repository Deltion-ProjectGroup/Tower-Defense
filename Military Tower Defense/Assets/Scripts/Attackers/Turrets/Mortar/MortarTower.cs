using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarTower : Turret {
    Vector3 spawnPosition;
    public GameObject ball;
    public Transform spawnPoint;
    Vector3 force;
    // Use this for initialization

    public override IEnumerator Attack()
    {
        if(targets.Count > 0)
        {
            //yield return WaitForSeconds(animation);
            spawnPosition = transform.position;
            spawnPosition.y += 1.5f;
            float distance = Vector3.Distance(targets[0].transform.position, transform.position);
            print(distance);
            distance *= 0.283f;
            force.y = 17;
            force.z = distance;
            GameObject shot = Instantiate(ball, spawnPoint.position, turretParts[1].transform.rotation);
            shot.GetComponent<Rigidbody>().AddRelativeForce(force, ForceMode.Impulse);
            //shot.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            if (targets.Count > 0)
            {
                yield return new WaitForSeconds(1 / baseAttackSpeed);
                if (targets.Count > 0)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }
    public void Update()
    {
        if (targets.Count > 0)
        {
            Vector3 lookRotation = new Vector3(targets[0].GetComponent<Enemy>().heart.transform.position.x, turretParts[1].transform.position.y, targets[0].GetComponent<Enemy>().heart.transform.position.z);
            turretParts[1].transform.LookAt(lookRotation);
        }
    }
}
