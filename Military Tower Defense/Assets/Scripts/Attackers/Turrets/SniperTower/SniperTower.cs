using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : Turret {

    // Use this for initialization
    public override IEnumerator Attack()
    {
        if(targets.Count > 0)
        {
            targets[0].GetComponent<Enemy>().health -= baseDamage;
            targets[0].GetComponent<Enemy>().CheckHealth();
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
            Vector3 lookRotation = new Vector3(targets[0].transform.position.x, turretParts[1].transform.position.y, targets[0].transform.position.z);
            turretParts[1].transform.LookAt(lookRotation);
        }
    }
}
