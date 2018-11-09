using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventManager : MonoBehaviour {
    public delegate void EventMethod();
    public static EventMethod OnLeftClick;
    public static EventMethod OnRightClick;
    public static EventMethod OnDialogComplete;
    public static EventMethod OnObstacleTakeDamage;
    public static EventMethod OnTurretPlaced;
    public delegate void EventGOMethod(GameObject gO);
    public static EventGOMethod onInteract;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            if(OnLeftClick != null)
            {
                OnLeftClick();
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if(OnRightClick != null)
            {
                OnRightClick();
            }
        }
	}
}
