using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Turret {

    // Use this for initialization
    public override IEnumerator Attack()
    {
        while(targets.Count > 0)
        {
            targets[0].GetComponent<Enemy>().health -= baseDamage;
            targets[0].GetComponent<Enemy>().CheckHealth();
            if (targets.Count > 0)
            {
                yield return new WaitForSeconds(1 / baseAttackSpeed);
            }
        }
    }
    public void Update()
    {
        if (targets.Count > 0)
        {
            Vector3 lookRotation = new Vector3(targets[0].transform.position.x, turretParts[1].transform.position.y, targets[0].transform.position.z);
            turretParts[1].transform.LookAt(lookRotation);
            turretParts[2].transform.LookAt(targets[0].transform);
        }
    }
}

