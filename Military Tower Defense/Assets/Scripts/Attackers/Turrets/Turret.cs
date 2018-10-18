using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Attacker {
    public GameObject[] turretParts; //This is for placing the turret to make it transparent etc.
    public List <GameObject> targets = new List<GameObject>(); //All the enemies it can target

    private void OnTriggerEnter(Collider hit)
    {
        OnEnterEffect(hit);
    }
    private void OnTriggerExit(Collider hit)
    {
        OnExitEffect(hit);
    }
    public virtual void CleanTarget(GameObject unit) //virtual because of flamethrower needing to remove the damagetarget instead of targets;
    {
        targets.Remove(unit);
        unit.GetComponent<Enemy>().targettedBy.Remove(gameObject);
        if(targets.Count <= 0)
        {
            StopAllCoroutines();
        }
    }
    public virtual void AddTarget(GameObject unit)
    {
        if(unit != null)
        {
            targets.Add(unit);
            unit.GetComponent<Enemy>().targettedBy.Add(gameObject);
        }
    }
    public virtual void OnEnterEffect(Collider hit)
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
    public virtual void OnExitEffect(Collider hit)
    {
        if (hit.gameObject.tag == "Enemy" && !hit.isTrigger)
        {
            CleanTarget(hit.gameObject);
        }
    }
}
