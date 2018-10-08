using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Turret {
    public Collider[] newTargets;
    public List<GameObject> damagingTargets = new List<GameObject>();
    public float detectionRange;
    public BurnStats burnstats;
    public ParticleSystem fireParticles;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        newTargets = Physics.OverlapSphere(transform.position, detectionRange, 9, QueryTriggerInteraction.Ignore);
        targets = new List<GameObject>();
        if (newTargets.Length > 0)
        {
            for (int i = 0; i < newTargets.Length; i++)
            {
                targets.Add(newTargets[i].transform.gameObject);
            }
            transform.LookAt(new Vector3(targets[0].transform.position.x, transform.position.y, targets[0].transform.position.z));
        }
    }
    public override void OnEnterEffect(Collider hit)
    {
        if(hit.tag == "Enemy" && !hit.isTrigger)
        {
            AddTarget(hit.transform.gameObject);
            if (damagingTargets.Count == 1)
            {
                fireParticles.Play();
                StartCoroutine(Attack());
            }
        }
    }
    public override void OnExitEffect(Collider hit)
    {
        if(hit.tag == "Enemy" && !hit.isTrigger)
        {
            CleanTarget(hit.transform.gameObject);
        }
    }
    public override IEnumerator Attack()
    {
        print("Attack");
        if(damagingTargets.Count > 0)
        {
            for (int i = 0; i < damagingTargets.Count; i++)
            {
                AddEffect(damagingTargets[i]);
                Vector3 lookRotation = new Vector3(targets[0].transform.position.x, transform.position.y, targets[0].transform.position.z);
                transform.LookAt(lookRotation);
                damagingTargets[i].GetComponent<Enemy>().health -= baseDamage;
                damagingTargets[i].GetComponent<Enemy>().CheckHealth();
            }
        }
        if (damagingTargets.Count > 0)
        {
            yield return new WaitForSeconds(1 / baseAttackSpeed);
            if(damagingTargets.Count > 0)
            {
                StartCoroutine(Attack());
            }
        }
        else
        {
            fireParticles.Stop();
        }
    }
    public override void CleanTarget(GameObject unit)
    {
        unit.GetComponent<Enemy>().targettedBy.Remove(gameObject);
        damagingTargets.Remove(unit);
        if (damagingTargets.Count <= 0)
        {
            StopAllCoroutines();
            fireParticles.Stop();
        }
    }
    public override void AddTarget(GameObject unit)
    {
        unit.GetComponent<Enemy>().targettedBy.Add(gameObject);
        damagingTargets.Add(unit);
    }
    [System.Serializable]
    public struct BurnStats
    {
        public int tickDamage;
        public int maxTicks;
        public float ticksPerSecond;
    }
    public void AddEffect(GameObject target)
    {
        if(target != null)
        {
            if (target.GetComponent<DOT>() != null)
            {
                target.GetComponent<DOT>().Refresh();
            }
            else
            {
                DOT burn = target.AddComponent<DOT>();
                burn.ticksPerSecond = burnstats.ticksPerSecond;
                burn.maxTicks = burnstats.maxTicks;
                burn.damage = burnstats.tickDamage;
            }
        }
    }
}
