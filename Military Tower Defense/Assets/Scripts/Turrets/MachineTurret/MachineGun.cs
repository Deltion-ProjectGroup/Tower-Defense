using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Turret {

    public override IEnumerator Attack()
    {
        GameObject target = targets[0];
        target.GetComponent<Enemy>().health -= damage;
        if(target.GetComponent<Enemy>().health <= 0)
        {
            targets.RemoveAt(0);
            if(targets.Count == 0)
            {
                StopAllCoroutines();
            }
        }
        target.GetComponent<Enemy>().HealthCheck();
        yield return new WaitForSeconds(1 / attackSpeed);
        StartCoroutine(Attack());
    }
}
