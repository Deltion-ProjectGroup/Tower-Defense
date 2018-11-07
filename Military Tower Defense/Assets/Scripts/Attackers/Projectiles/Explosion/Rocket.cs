using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Explosive {
    public float speed;
    public float rotateSpeed;
    public GameObject target;
    public ParticleSystem[] particles;
    bool destructing = false;
    public AudioClip explosionSound;
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
                Explosion(false);
                Destroy(gameObject, 3);
            }
        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}
    private void OnTriggerEnter(Collider hit)
    {
        if(!hit.isTrigger && hit.tag != "Turret" && hit.tag != "Explodable")
        {
            GetComponent<AudioSource>().clip = explosionSound;
            GetComponent<AudioSource>().loop = false;
            for(int i = 0; i < particles.Length; i++)
            {
                particles[i].Stop();
            }
            Explosion(false);
        }
    }
}
