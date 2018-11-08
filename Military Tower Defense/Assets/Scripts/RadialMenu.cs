using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour {
    public GameObject[] radialParts;
    public GameObject selectingObj;
    bool shopping;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ResetScale()
    {
        for(int i = 0; i < radialParts.Length; i++)
        {
            radialParts[i].transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void SwitchShop()
    {
        if (shopping)
        {
            shopping = false;
            for(int i = 0; i < radialParts.Length; i++)
            {
                radialParts[i].SetActive(false);
            }
            EventManager.OnLeftClick -= MissClick;
        }
        else
        {
            shopping = true;
            ResetScale();
            for (int i = 0; i < radialParts.Length; i++)
            {
                radialParts[i].SetActive(true);
            }
            transform.position = Input.mousePosition;
            EventManager.OnLeftClick += MissClick;
        }
    }
    public void MissClick()
    {
        if(selectingObj == null)
        {
            SwitchShop();
        }
    }
}
