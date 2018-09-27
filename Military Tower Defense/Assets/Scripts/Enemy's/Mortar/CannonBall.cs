using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {
    public GameObject explosionParticles;
    public float damage;
    public bool isFromEnemy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter(Collider hit)
    {
        if (!hit.isTrigger)
        {
            if (isFromEnemy)
            {
                if (hit.gameObject.transform.tag == "Targettable")
                {
                    GameObject explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity);
                    hit.gameObject.GetComponent<Obstacle>().health -= damage;
                    hit.gameObject.GetComponent<Obstacle>().CheckHealth();
                    Destroy(explosion, 3);
                }
            }
            else
            {
                if (hit.gameObject.transform.tag == "Enemy")
                {
                    GameObject explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity);
                    hit.gameObject.GetComponent<Enemy>().health -= damage;
                    hit.gameObject.GetComponent<Enemy>().CheckHealth();
                    Destroy(explosion, 3);
                }
            }
            Destroy(gameObject);
        }
    }
}
