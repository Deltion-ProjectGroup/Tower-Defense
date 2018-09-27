using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Turret {
    public override IEnumerator Attack()
    {
        if(targets.Count > 0)
        {
            GameObject target = targets[0];
            target.GetComponent<Enemy>().health -= damage;
            target.GetComponent<Enemy>().CheckHealth();
            /*if (targets.Count == 0)
            {
                StopAllCoroutines();
            }*/
            yield return new WaitForSeconds(1 / attackSpeed);
            StartCoroutine(Attack());
        }
    }
    public void Update()
    {
        if(targets.Count > 0)
        {
            Vector3 lookRotation = new Vector3(targets[0].transform.position.x, transform.position.y, targets[0].transform.position.z);
            transform.LookAt(lookRotation);
        }
    }
}
