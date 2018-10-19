using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCam : MonoBehaviour {
    public float rotateSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(transform.position - GameObject.FindGameObjectWithTag("MainCamera").transform.position), rotateSpeed);
	}
}
