using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour {
    public GameObject explosionParticles;
    public float damage;
    public float explosionRange;
    public Collider[] enemies;
    public LayerMask targettable;
    // Use this for initialization

    public virtual void Explosion()
    {
        enemies = Physics.OverlapSphere(transform.position, explosionRange, targettable, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().health -= damage;
            enemies[i].GetComponent<Enemy>().CheckHealth();
        }
        Destroy(gameObject);
    }
}
