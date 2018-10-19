using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Turret {
    public GameObject turret;
    public GameObject gun;
    public GameObject rangeIndicator;
    public Collider[] hitTargets;
    public LayerMask layermask;
    // Use this for initialization

    public override IEnumerator Attack()
    {
        print("COROTOU");
        RotateNCheck();
        if (targets.Count > 0 && hitTargets.Length > 0)
        {
            yield return new WaitForSeconds(1 / baseAttackSpeed);
            if (hitTargets.Length > 0)
            {
                for(int i = 0; i < hitTargets.Length; i++)
                {
                    print("Attacked");
                    hitTargets[i].GetComponent<Enemy>().health -= baseDamage;
                    hitTargets[i].GetComponent<Enemy>().CheckHealth();
                }
                StartCoroutine(Attack());
            }
        }
    }
    public void Update()
    {
        RotateNCheck();
    }
    void RotateNCheck()
    {
        if (targets.Count > 0)
        {
            Vector3 lookRotation = new Vector3(targets[0].transform.position.x, turret.transform.position.y, targets[0].transform.position.z);
            turret.transform.LookAt(lookRotation);
            hitTargets = Physics.OverlapBox(targets[0].transform.position, (rangeIndicator.GetComponent<BoxCollider>().size / 2), Quaternion.LookRotation(targets[0].transform.position - transform.position), layermask, QueryTriggerInteraction.Ignore);

        }
    }
}
