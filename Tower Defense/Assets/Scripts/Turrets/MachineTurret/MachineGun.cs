using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Turret {

    public override IEnumerator Attack()
    {
        targets[0].GetComponent<Enemy>().health -= damage;
        if(targets[0].GetComponent<Enemy>().health <= 0)
        {
            targets.RemoveAt(0);
            if(targets.Count == 0)
            {
                StopAllCoroutines();
            }
        }
        targets[0].GetComponent<Enemy>().HealthCheck();
        yield return new WaitForSeconds(1 / attackSpeed);
        StartCoroutine(Attack());
    }
}
