using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public float health;
	// Use this for initialization
	void Start () {
        GetComponent<NavMeshAgent>().SetDestination(LevelManager.levelManager.targetDestination);
	}
	
	// Update is called once per frame
	void Update () {

	}
    public void Death()
    {
        Destroy(gameObject);
        print("OOF");
    }
}
