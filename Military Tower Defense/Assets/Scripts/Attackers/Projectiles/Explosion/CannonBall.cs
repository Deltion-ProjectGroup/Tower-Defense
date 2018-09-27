using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : Explosive {
    public bool isFromEnemy;
    // Use this for initialization

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
                    Destroy(gameObject);
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
