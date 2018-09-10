using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BuyItem : MonoBehaviour, IPointerClickHandler {
    public GameObject turret;
    public int cost;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TurretPlacer>().placing)
        {
            if(LevelManager.levelManager.currency >= cost)
            {
                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TurretPlacer>().PlaceTurret(turret);
            }
        }
    }
}
