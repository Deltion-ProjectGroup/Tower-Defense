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
    public bool attacking = false;
    public bool damaged;
    public bool dead;
    public int worthCurrency;
    public LevelManager.EnemyType enemyType;
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
	}
    public void OnTriggerEnter(Collider hit)
    {
        if (!attacking)
        {
            if (hit.tag == "Targettable")
            {
                attacking = true;
                target = hit.transform.gameObject;
                GetComponent<NavMeshAgent>().isStopped = true;
                target.GetComponent<Obstacle>().AddUnit(gameObject);
                StartCoroutine(Attack());
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
        if (health <= 0 && !dead)
        {
            StopAllCoroutines();
            dead = true;
            healthbarHolder.SetActive(false);
            while (targettedBy.Count != 0)
            {
                targettedBy[0].GetComponent<Turret>().CleanTarget(gameObject);
            }
            LevelManager.levelManager.RemoveEnemy(gameObject);
            if (attacking)
            {
                target.GetComponent<Obstacle>().RemoveUnit(gameObject);
            }
            LevelManager.levelManager.AddCurrency(worthCurrency);
            gameObject.GetComponent<Collider>().enabled = false;
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
