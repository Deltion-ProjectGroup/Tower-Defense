using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatetowards : MonoBehaviour {
    public GameObject target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion hi = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, hi, 0.2f);
	}
}
