using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerDamageRange : MonoBehaviour {
    public Flamethrower flamethrower;
    public ParticleSystem flames;
    private void Start()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Enemy")
        {
            flamethrower.AddTarget(hit.gameObject);
            if(flamethrower.targets.Count == 1)
            {
                flames.Play();
            }
        }
    }
    private void OnTriggerExit(Collider hit)
    {
        if(hit.tag == "Enemy")
        {
            flamethrower.CleanTarget(hit.gameObject);
            if (flamethrower.targets.Count == 0)
            {
                flames.Stop();
            }
        }
    }
}
