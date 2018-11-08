﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventManager : MonoBehaviour {
    public delegate void EventMethod();
    public static event EventMethod OnClick;
    public static EventMethod OnDialogComplete;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            if(OnClick != null)
            {
                OnClick();
            }
        }
	}
}
