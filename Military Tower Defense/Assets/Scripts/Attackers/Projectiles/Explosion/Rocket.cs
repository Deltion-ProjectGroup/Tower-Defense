using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Explosive {
    public float speed;
    public float rotateSpeed;
    [HideInInspector]
    public GameObject target;
    public RocketLauncherTower owner;
    bool destructing = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(target != null)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up), rotateSpeed * Time.deltaTime);
        }
        else
        {
            if (!destructing)
            {
                destructing = true;
                Destroy(gameObject, 3);
            }
        }
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
