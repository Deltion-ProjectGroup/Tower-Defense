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
            RaycastHit[] hits;
            hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 10000);
            for(int i = 0; i < hits.Length; i++)
            {
                if(hits[i].transform.tag == "TowerTerrain")
                {
                    if (CanPlace())
                    {
                        placingObject.transform.position = hits[i].point;
                        break;
                    }
                    else
                    {
                        print("NO");
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
            if (Input.GetButtonDown("Fire2"))
            {
                placing = false;
                Destroy(placingObject);
                placingObject = null;
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
        Collider[] colls = Physics.OverlapBox(placingObject.GetComponent<BoxCollider>().center, placingObject.GetComponent<BoxCollider>().size, Quaternion.identity, 11, QueryTriggerInteraction.Ignore);
        for(int i = 0; i < colls.Length; i++)
        {
            if(colls[i].tag != "TowerTerrain" && !colls[i].isTrigger)
            {
                return false;
            }
        }
        return true;
    }
}
