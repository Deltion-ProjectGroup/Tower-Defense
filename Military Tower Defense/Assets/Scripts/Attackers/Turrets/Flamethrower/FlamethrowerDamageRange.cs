using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerDamageRange : MonoBehaviour {
    public Flamethrower flamethrower;
    private void Start()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Enemy" && !hit.isTrigger)
        {
            print("Added");
            flamethrower.AddTarget(hit.gameObject);
        }
    }
    private void OnTriggerExit(Collider hit)
    {
        if(hit.tag == "Enemy" && !hit.isTrigger)
        {
            flamethrower.CleanTarget(hit.gameObject);
        }
    }
}
