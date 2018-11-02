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
    public GameObject heart;
    public RaycastHit hitObj;
    public List <GameObject> targettedBy = new List<GameObject>();
    public bool attacking = false;
    public bool damaged;
    public bool dead;
    public int worthCurrency;
    public float[] attackTimeMarks; // NOTE: if 1st attack is after 1s and the 2nd after 2 s the 2nd one will also be yield return 1 s; 2-1 = 1
    public GameObject impactParticle;
    public GameObject deathParticles;
    public LayerMask targettable;
    //public ParticleSystem dust;
    public LevelManager.EnemyType enemyType;
    // Use this for initialization
    private void Awake()
    {
        attackSpeed = baseAttackSpeed;
        health = baseHealth;
        maxHealth = baseHealth;
        movementspeed = baseMovementspeed;
        damage = baseDamage;
        attackSpeed = baseAttackSpeed;
        GetComponent<NavMeshAgent>().speed = movementspeed;
        GetComponent<Animator>().SetFloat("AttackSpeed", attackSpeed);
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
        if (attacking && !dead)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position, Vector3.zero), 1.5f);
        }
	}
    public void OnTriggerEnter(Collider hit)
    {
        if (!attacking)
        {
            if (hit.tag == "Targettable")
            {
                //dust.Stop();
                attacking = true;
                target = hit.transform.gameObject;
                GetComponent<NavMeshAgent>().isStopped = true;
                target.GetComponent<Obstacle>().AddUnit(gameObject);
                GetComponent<Animator>().SetBool("canWalk", false);
                GetComponent<Animator>().SetBool("canAttack", true);
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
            StartCoroutine(Death());
        }
    }
    public override IEnumerator Attack()
    {
        return base.Attack();
    }
    public void Repath()
    {
        //dust.Play();
        GetComponent<Animator>().SetBool("canAttack", false);
        GetComponent<Animator>().SetBool("canWalk", true);
        StopAllCoroutines();
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        attacking = false;
    }
    public IEnumerator Death()
    {
        dead = true;
        //dust.Stop();
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
        LevelManager.levelManager.AddCurrency(worthCurrency, transform.position);
        Material fader = new Material(GetComponentInChildren<SkinnedMeshRenderer>().material);
        fader.SetFloat("_Mode", 2);
        fader.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        fader.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        fader.SetInt("_ZWrite", 0);
        fader.DisableKeyword("_ALPHATEST_ON");
        fader.EnableKeyword("_ALPHABLEND_ON");
        fader.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        fader.renderQueue = 3000;
        GetComponentInChildren<SkinnedMeshRenderer>().material = fader;
        gameObject.GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Animator>().SetBool("canAttack", false);
        GetComponent<Animator>().SetBool("canWalk", false);
        GetComponent<Animator>().SetBool("canDie", true);
        yield return new WaitForSeconds(1);
        Color fadeOut = fader.color;
        while (fader.color.a > 0)
        {
            fadeOut.a -= 0.02f;
            if(fadeOut.a < 0)
            {
                fadeOut.a = 0;
            }
            fader.color = fadeOut;
            print("KEK");
            yield return new WaitForSeconds(0.02f);
        }
        Vector3 spawn = heart.transform.position;
        spawn.y -= 1;
        GameObject g = Instantiate(deathParticles, spawn, deathParticles.transform.rotation);
        Destroy(g, 2);
        Destroy(gameObject);
    }
}
