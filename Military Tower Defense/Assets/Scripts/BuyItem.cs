using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BuyItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    public GameObject turret;
    public Vector3 scaler;
    public int cost;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInParent<RadialMenu>().selectingObj = gameObject;
        transform.localScale += scaler;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInParent<RadialMenu>().selectingObj = null;
        transform.localScale -= scaler;
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TurretPlacer>().placing == false)
        {
            if(LevelManager.levelManager.currency >= cost)
            {
                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TurretPlacer>().PlaceTurret(turret);
            }
        }
        GetComponentInParent<RadialMenu>().SwitchShop();
    }
}
