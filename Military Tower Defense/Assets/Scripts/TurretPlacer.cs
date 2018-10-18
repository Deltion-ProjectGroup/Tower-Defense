using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacer : MonoBehaviour {
    public bool placing;
    public LayerMask detectable;
    public Color[] placementColors;
    public GameObject placingObject;
    public Material[] ogMaterials;
    public Material[] fakeMaterials;
    public Collider[] colls;
    bool canPlace;
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
                    placingObject.transform.position = hits[i].point;
                    CanPlace();
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
                for(int i = 0; i < placingObject.GetComponent<Turret>().turretParts.Length; i++)
                {
                    placingObject.GetComponent<Turret>().turretParts[i].GetComponent<Renderer>().material = ogMaterials[i];
                }
                placingObject = null;
                placing = false;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                if (canPlace)
                {
                    placing = false;
                    Destroy(placingObject);
                    placingObject = null;
                }
            }
        }
	}
    public void PlaceTurret(GameObject turret)
    {
        placing = true;
        placingObject = Instantiate(turret, Vector3.zero, Quaternion.identity);
        ogMaterials = new Material[placingObject.GetComponent<Turret>().turretParts.Length];
        for(int i = 0; i < ogMaterials.Length; i++)
        {
            ogMaterials[i] = placingObject.GetComponent<Turret>().turretParts[i].GetComponent<Renderer>().material;
        }
        for(int i = 0; i < placingObject.GetComponent<Turret>().turretParts.Length; i++)
        {
            fakeMaterials[i].CopyPropertiesFromMaterial(ogMaterials[i]);
        }
        for (int i = 0; i < placingObject.GetComponent<Turret>().turretParts.Length; i++)
        {
            placingObject.GetComponent<Turret>().turretParts[i].GetComponent<Renderer>().material = fakeMaterials[i];
        }
    }
    void CanPlace()
    {
        colls = Physics.OverlapBox(placingObject.transform.position, placingObject.GetComponent<BoxCollider>().size, Quaternion.identity, detectable, QueryTriggerInteraction.Ignore);
        for(int i = 0; i < colls.Length; i++)
        {
            if(colls[i].tag != "TowerTerrain" && !colls[i].isTrigger)
            {
                for (int q = 0; q < placingObject.GetComponent<Turret>().turretParts.Length; q++)
                {
                    fakeMaterials[q].color = placementColors[0];
                }
                canPlace = false;
                return;
            }
        }
        for(int i = 0; i < placingObject.GetComponent<Turret>().turretParts.Length; i++)
        {
            fakeMaterials[i].color = placementColors[1];
        }
        canPlace = true;
        return;
    }
}
