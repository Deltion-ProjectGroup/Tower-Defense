using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Turret {
    public GameObject turret;
    public GameObject gun;
    public int maxTicks;
    public int ticksPerSec;
    public LayerMask targettable;
    // Use this for initialization
    private void Start()
    {
        turretParts[2].GetComponent<FlamethrowerDamageRange>().enabled = true;
    }
    public void Update()
    {
        RotateTurret();
        if(targets.Count > 0)
        {
            for(int i = 0; i < targets.Count; i++)
            {
                AddEffect(targets[i]);
            }
        }
    }
    void RotateTurret()
    {
        Collider[] lookTargets = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius, targettable, QueryTriggerInteraction.Ignore);
        if(lookTargets.Length > 0)
        {
            Vector3 lookRotation = new Vector3(lookTargets[0].transform.position.x, turret.transform.position.y, lookTargets[0].transform.position.z);
            turretParts[1].transform.LookAt(lookRotation);
        }

    }
    public override void OnEnterEffect(Collider hit)
    { 
        //CLEARS THE EFFECT OF DA COLLIDER
    }
    public override void OnExitEffect(Collider hit)
    {
        //^^^^^^^
    }
    void AddEffect(GameObject target)
    {
        if(target.GetComponent<DOT>() != null)
        {
            if(target.GetComponent<DOT>().dotType == DOT.DOTType.Fire)
            {
                target.GetComponent<DOT>().Refresh();
            }
            else
            {
                DOT dot = target.AddComponent<DOT>();
                dot.maxTicks = maxTicks;
                dot.ticksPerSecond = ticksPerSec;
                dot.damage = (int)baseDamage;
            }
        }
        else
        {
            DOT dot = target.AddComponent<DOT>();
            dot.maxTicks = maxTicks;
            dot.ticksPerSecond = ticksPerSec;
            dot.damage = (int)baseDamage;
        }
    }
}
