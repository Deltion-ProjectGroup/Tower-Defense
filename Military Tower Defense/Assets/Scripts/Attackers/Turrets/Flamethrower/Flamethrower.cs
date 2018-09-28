﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Turret {
    public Collider[] newTargets;
    public List<GameObject> damagingTargets = new List<GameObject>();
    public float detectionRange;
    public BurnStats burnstats;
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
        yield return new WaitForEndOfFrame();
        if(damagingTargets.Count > 0)
        {
            for (int i = 0; i < damagingTargets.Count; i++)
            {
                AddEffect(damagingTargets[i]);
                damagingTargets[i].GetComponent<Enemy>().health -= baseDamage;
                damagingTargets[i].GetComponent<Enemy>().CheckHealth();
            }
            yield return new WaitForSeconds(1 / baseAttackSpeed);
            StartCoroutine(Attack());
        }
    }
    public override void CleanTarget(GameObject unit)
    {
        unit.GetComponent<Enemy>().targettedBy.Remove(gameObject);
        damagingTargets.Remove(unit);
    }
    public override void AddTarget(GameObject unit)
    {
        damagingTargets.Add(unit);
        unit.GetComponent<Enemy>().targettedBy.Add(gameObject);
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
        if(target.GetComponent<DOT>() != null)
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
