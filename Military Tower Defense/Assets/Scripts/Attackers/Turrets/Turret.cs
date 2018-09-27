using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Attacker {
    public List <GameObject> targets = new List<GameObject>(); //All the enemies it can target

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Enemy" && !hit.isTrigger)
        {
            AddTarget(hit.transform.gameObject);
            if (targets.Count == 1)
            {
                StartCoroutine(Attack());
            }
        }
    }
    private void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.tag == "Enemy" && !hit.isTrigger)
        {
            CleanTarget(hit.gameObject);
            if (targets.Count == 0)
            {
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
