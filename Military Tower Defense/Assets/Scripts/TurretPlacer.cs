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
    public GameObject dustParticles;
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
                    Vector3 newPos = hits[i].point;
                    newPos.y = 0;
                    placingObject.transform.position = newPos;
                    CanPlace(newPos);
                }
            }
            if (Input.GetButtonDown("Fire1"))
            {
                if (canPlace)
                {
                    GameObject dust = Instantiate(dustParticles, placingObject.transform.position, Quaternion.identity);
                    Destroy(dust, 2);
                    Collider[] turretColliders = placingObject.GetComponents<Collider>();
                    placingObject.GetComponent<Turret>().enabled = true;
                    for (int i = 0; i < turretColliders.Length; i++)
                    {
                        turretColliders[i].enabled = true;
                    }
                    for (int i = 0; i < placingObject.GetComponent<Turret>().turretParts.Length; i++)
                    {
                        placingObject.GetComponent<Turret>().turretParts[i].GetComponent<Renderer>().material = ogMaterials[i];
                    }
                    placingObject = null;
                    placing = false;
                }
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
    void CanPlace(Vector3 placePos)
    {
        colls = Physics.OverlapBox(placePos, (placingObject.GetComponent<BoxCollider>().size / 2), Quaternion.identity, detectable, QueryTriggerInteraction.Ignore);
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
