using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Attacker {
    public List <GameObject> targets = new List<GameObject>(); //All the enemies it can target

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            targets.Add(hit.gameObject);
            if (targets.Count == 1)
            {
                StartCoroutine(Attack());
            }
        }
    }
    private void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            if (targets.Count == 1)
            {
                StopAllCoroutines();
            }
            targets.Remove(hit.gameObject);
        }
    }
}
