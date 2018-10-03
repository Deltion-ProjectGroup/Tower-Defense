using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public GameObject healthbar;
    public GameObject healthbarHolder;
    public RaycastHit hitObj;
    public List <GameObject> targettedBy = new List<GameObject>();
    bool attacking = false;
    bool damaged;
    public int worthCurrency;
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
        if (damaged)
        {
            healthbarHolder.transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform);
        }
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
        if(health < maxHealth)
        {
            if (!damaged)
            {
                damaged = true;
                healthbarHolder.SetActive(true);
            }
            healthbar.GetComponent<Image>().fillAmount = (1/maxHealth) * health;
        }
        if(health <= 0)
        {
            for(int i = 0; i < targettedBy.Count; i++)
            {
                targettedBy[i].GetComponent<Turret>().CleanTarget(gameObject);
                LevelManager.levelManager.RemoveEnemy(gameObject);
            }
            LevelManager.levelManager.AddCurrency(worthCurrency);
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
