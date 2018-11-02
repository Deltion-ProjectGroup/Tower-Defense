using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : Explosive {
    public bool isFromEnemy;
    // Use this for initialization

    public void OnTriggerEnter(Collider hit)
    {
        if (!hit.isTrigger && hit.gameObject.transform.tag != "Explodable")
        {
            if (isFromEnemy)
            {
                if(hit.transform.tag != "Enemy")
                {
                    GameObject explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity);
                    if (hit.gameObject.transform.tag == "Targettable")
                    {
                        hit.gameObject.GetComponent<Obstacle>().health -= damage;
                        hit.gameObject.GetComponent<Obstacle>().CheckHealth();
                        Destroy(gameObject);
                    }
                    Destroy(explosion, 3);
                }
            }
            else
            {
                if (hit.gameObject.transform.tag != "Turret")
                {
                    GameObject explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity);
                    Explosion();
                    Destroy(explosion, 3);
                }
            }
        }
    }
}
