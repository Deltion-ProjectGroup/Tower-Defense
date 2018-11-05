using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour {
    public GameObject explosionParticles;
    public float damage;
    public float explosionRange;
    public Collider[] targets;
    public LayerMask targettableEnemies;
    public LayerMask targettableObstacles;
    // Use this for initialization

    public void Explosion(bool fromEnemy)
    {
        GetComponent<AudioSource>().Play();
        if (fromEnemy)
        {
            Destroy(Instantiate(explosionParticles, transform.position, explosionParticles.transform.rotation), 2);
            targets = Physics.OverlapSphere(transform.position, explosionRange, targettableObstacles, QueryTriggerInteraction.Ignore);
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].GetComponent<Obstacle>().health -= damage;
                targets[i].GetComponent<Obstacle>().CheckHealth();
            }
        }
        else
        {
            Destroy(Instantiate(explosionParticles, transform.position, explosionParticles.transform.rotation), 2);
            targets = Physics.OverlapSphere(transform.position, explosionRange, targettableEnemies, QueryTriggerInteraction.Ignore);
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].GetComponent<Enemy>().health -= damage;
                targets[i].GetComponent<Enemy>().CheckHealth();
            }
        }
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        Destroy(gameObject, GetComponent<AudioSource>().clip.length);
    }
}
