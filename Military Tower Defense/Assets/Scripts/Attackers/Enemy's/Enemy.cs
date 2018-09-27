﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Attacker {
    [Header("Health")]
    public float baseHealth;
    public float health;
    [HideInInspector]
    public float maxHealth;
    [Header("MovementSpeed")]
    public float baseMovementspeed;
    [HideInInspector]
    public float movementspeed;
    [Header("AttackRange")]
    public float attackRange;
    [Header("AttackStuff")]
    public GameObject target;
    public RaycastHit hitObj;
    public List <GameObject> targettedBy = new List<GameObject>();
    bool attacking = false;
    // Use this for initialization
    private void Awake()
    {
        health = baseHealth;
        maxHealth = baseHealth;
        movementspeed = baseMovementspeed;
        damage = baseDamage;
        attackSpeed = baseAttackSpeed;
        GetComponent<NavMeshAgent>().speed = movementspeed;
    }
    void Start () {
        GetComponent<NavMeshAgent>().SetDestination(LevelManager.levelManager.targetDestination);
	}
	
	// Update is called once per frame
	public virtual void Update ()
    {
        CheckAttack();
	}
    public void CheckAttack()
    {
        if (!attacking)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hitObj, attackRange))
            {
                if (hitObj.transform.gameObject.tag == "Targettable")
                {
                    attacking = true;
                    target = hitObj.transform.gameObject;
                    GetComponent<NavMeshAgent>().isStopped = true;
                    target.GetComponent<Obstacle>().targettedBy.Add(gameObject);
                    StartCoroutine(Attack());
                }
            }
        }
    }
    public virtual void CheckHealth()
    {
        if(health <= 0)
        {
            for(int i = 0; i < targettedBy.Count; i++)
            {
                targettedBy[i].GetComponent<Turret>().CleanTarget(gameObject);
            }
            Destroy(gameObject);
        }
    }
    public void Repath()
    {
        StopAllCoroutines();
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        attacking = false;
    }
}