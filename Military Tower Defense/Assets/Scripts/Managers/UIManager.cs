using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public GameObject shopUI;
    public bool shopping;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2"))
        {
            if (shopping)
            {
                shopping = false;
                shopUI.SetActive(false);
            }
            else
            {
                shopping = true;
                shopUI.SetActive(true);
            }
        }
	}
}
