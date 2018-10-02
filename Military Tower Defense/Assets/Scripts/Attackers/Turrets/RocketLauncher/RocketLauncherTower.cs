using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherTower : Turret {
    public GameObject missile;
    public Vector3 spawnModifier;
    public override IEnumerator Attack()
    {
        if(targets.Count > 0)
        {
            GameObject rocket = Instantiate(missile, spawnModifier + transform.position, Quaternion.identity);
            rocket.transform.LookAt(targets[0].transform);
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
            Vector3 lookRotation = new Vector3(targets[0].transform.position.x, transform.position.y, targets[0].transform.position.z);
            transform.LookAt(lookRotation);
        }
    }
}
