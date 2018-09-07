using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public Vector3 targetDestination;
    public static LevelManager levelManager;
	// Use this for initialization
    private void Awake()
    {
        levelManager = this;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
