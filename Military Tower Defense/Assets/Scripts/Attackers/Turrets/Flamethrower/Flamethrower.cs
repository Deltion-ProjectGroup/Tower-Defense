using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Turret {
    public Collider[] newTargets;
    public Vector3 centerOfBox;
    public Vector3 halfSize;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (targets.Count > 0)
        {
            newTargets = Physics.OverlapBox(centerOfBox, halfSize, Quaternion.FromToRotation(transform.position, targets[0].transform.position), 9, QueryTriggerInteraction.Ignore);
            Vector3 lookRotation = new Vector3(targets[0].transform.position.x, transform.position.y, targets[0].transform.position.z);
            transform.LookAt(lookRotation);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(centerOfBox, halfSize);
    }
}
