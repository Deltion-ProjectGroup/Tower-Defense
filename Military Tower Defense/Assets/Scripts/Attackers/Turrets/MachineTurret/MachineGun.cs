using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Turret {

    // Use this for initialization
    public GameObject turret;
    public GameObject gun;
    public override IEnumerator Attack()
    {
        if (targets.Count > 0)
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
            Vector3 lookRotation = new Vector3(targets[0].transform.position.x, turret.transform.position.y, targets[0].transform.position.z);
            turret.transform.LookAt(lookRotation);
            gun.transform.LookAt(targets[0].transform);
            Quaternion rotation = gun.transform.rotation;
            rotation.x = Mathf.Clamp(rotation.x, -24, 15.25f);
            Quaternion.
            print(rotation.x);
            gun.transform.rotation = rotation;
        }
    }
}

