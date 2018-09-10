using System.Collections;
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
                    placingObject.transform.position = hit.point;
                }
            }
            if (Input.GetButtonDown("Fire1"))
            {
                placingObject.GetComponent<CapsuleCollider>().enabled = true;
                placingObject.GetComponent<SphereCollider>().enabled = true;
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
}
