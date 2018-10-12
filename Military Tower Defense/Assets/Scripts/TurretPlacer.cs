﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacer : MonoBehaviour {
    public bool placing;
    public GameObject placingObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (placing)
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10000))
            {
                if(hit.transform.gameObject.tag == "TowerTerrain")
                {
                    if (CanPlace())
                    {
                        placingObject.transform.position = hit.point;
                    }
                }
            }
            if (Input.GetButtonDown("Fire1"))
            {
                Collider[] turretColliders = placingObject.GetComponents<Collider>();
                placingObject.GetComponent<Turret>().enabled = true;
                for(int i = 0; i < turretColliders.Length; i++)
                {
                    turretColliders[i].enabled = true;
                }
                placingObject = null;
                placing = false;
            }
        }
	}
    public void PlaceTurret(GameObject turret)
    {
        placing = true;
        placingObject = Instantiate(turret, Vector3.zero, Quaternion.identity);
    }
    bool CanPlace()
    {
        Collider[] colls = Physics.OverlapBox(placingObject.transform.position, placingObject.GetComponent<BoxCollider>().size, Quaternion.identity, 11, QueryTriggerInteraction.Ignore);
        for(int i = 0; i < colls.Length; i++)
        {
            if(colls[i].tag != "TowerTerrain")
            {
                return false;
            }
        }
        return true;
    }
}
