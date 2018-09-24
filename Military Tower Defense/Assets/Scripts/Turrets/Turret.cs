using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Attacker {
    public List <GameObject> targets = new List<GameObject>(); //All the enemies it can target

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Enemy" && !hit.isTrigger)
        {
            targets.Add(hit.gameObject);
            if (targets.Count == 1)
            {
                AddTarget(hit.gameObject);
                StartCoroutine(Attack());
            }
        }
    }
    private void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.tag == "Enemy" && !hit.isTrigger)
        {
            if (targets.Count == 1)
            {
                CleanTarget(hit.gameObject);
                StopAllCoroutines();
            }
        }
    }
    public void CleanTarget(GameObject unit)
    {
        targets.Remove(unit);
        unit.GetComponent<Enemy>().targettedBy.Remove(gameObject);
    }
    public void AddTarget(GameObject unit)
    {
        targets.Add(unit);
        unit.GetComponent<Enemy>().targettedBy.Add(gameObject);
    }
}
