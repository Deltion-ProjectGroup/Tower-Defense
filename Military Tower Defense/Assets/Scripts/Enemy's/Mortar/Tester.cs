using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour {
    public Vector3 spawnPosition;
    public GameObject ball;
    public GameObject target;
    public Vector3 force;
    public Vector3 hi;
    float radians;
	// Use this for initialization
	void Start () {
        Time.timeScale = 0.2f;
        spawnPosition = transform.position;
        spawnPosition.y += 1.5f;
        hi = Vector3.RotateTowards(transform.position, target.transform.position, 100, 10);
        float distance = Vector3.Distance(target.transform.position, transform.position);
        distance *= 0.287f;
        print(distance);
        force.z = distance;
        GameObject shot = Instantiate(ball, spawnPosition, Quaternion.identity);
        shot.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(hi);
    }
}
