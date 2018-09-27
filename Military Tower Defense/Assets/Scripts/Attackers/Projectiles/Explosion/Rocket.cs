using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Explosive {
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}
    private void OnTriggerEnter(Collider hit)
    {
        if(!hit.isTrigger && hit.tag != "Turret")
        {
            print(hit);
            Explosion();
        }
    }
    public override void Explosion()
    {
        //Instantiate(explosionParticles);
        base.Explosion();
    }
}
