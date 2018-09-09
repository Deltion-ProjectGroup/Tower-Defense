using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Attacker {
    public float baseAttackSpeed;
    public float baseHealth;
    public float health;
    public float maxHealth;
    public float baseMovementspeed;
    public float movementspeed;
    public float attackRange;
    bool attacking = false;
    public GameObject target;
    public RaycastHit hitObj;
    // Use this for initialization
    private void Awake()
    {
        GetComponent<NavMeshAgent>().speed = movementspeed;
    }
    void Start () {
        GetComponent<NavMeshAgent>().SetDestination(LevelManager.levelManager.targetDestination);
	}
	
	// Update is called once per frame
	public virtual void Update ()
    {
        if (!attacking)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hitObj, attackRange))
            {
                if(hitObj.transform.gameObject.tag == "Targettable")
                {
                    target = hitObj.transform.gameObject;
                    GetComponent<NavMeshAgent>().isStopped = true;
                    StartCoroutine(Attack());
                }
            }
        }
	}
    public virtual void HealthCheck()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
