using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public GameObject shopUI;
    public GameObject timerUI;
    public GameObject informationUI;
    public GameObject[] enemyInformation;
    public GameObject[] turretInformation;
    public GameObject[] obstacleInformation;
    public bool shopping;
    bool isTracking;
    public GameObject trackingObj;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isTracking)
        {
            UpdateStats();
        }
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
    public void ShowStats(GameObject target)
    {
        trackingObj = target;
        enemyInformation[0].SetActive(false);
        turretInformation[0].SetActive(false);
        obstacleInformation[0].SetActive(false);
        if(target.tag == "Enemy" || target.tag == "Turret" || target.tag == "Targettable")
        {
            informationUI.SetActive(true);
            isTracking = true;
        }
        else
        {
            isTracking = false;
            informationUI.SetActive(false);
        }
    }
    public void UpdateStats()
    {
        if(trackingObj != null)
        {
            switch (trackingObj.tag)
            {
                case "Enemy":
                    enemyInformation[0].SetActive(true);
                    enemyInformation[2].GetComponent<Text>().text = trackingObj.GetComponent<Enemy>().objName;
                    enemyInformation[3].GetComponent<Text>().text = trackingObj.GetComponent<Enemy>().health.ToString();
                    enemyInformation[4].GetComponent<Text>().text = trackingObj.GetComponent<Enemy>().attackSpeed.ToString();
                    enemyInformation[5].GetComponent<Text>().text = trackingObj.GetComponent<Enemy>().movementspeed.ToString();
                    enemyInformation[6].GetComponent<Text>().text = trackingObj.GetComponent<Enemy>().damage.ToString();
                    enemyInformation[7].GetComponent<Text>().text = trackingObj.GetComponent<Enemy>().description;
                    break;
                case "Turret":
                    turretInformation[0].SetActive(true);
                    turretInformation[2].GetComponent<Text>().text = trackingObj.GetComponent<Turret>().objName;
                    turretInformation[3].GetComponent<Text>().text = trackingObj.GetComponent<Turret>().attackSpeed.ToString();
                    turretInformation[4].GetComponent<Text>().text = trackingObj.GetComponent<Turret>().damage.ToString();
                    turretInformation[5].GetComponent<Text>().text = trackingObj.GetComponent<Turret>().description;
                    break;
                case "Targettable":
                    obstacleInformation[0].SetActive(true);
                    obstacleInformation[2].GetComponent<Text>().text = trackingObj.GetComponent<Obstacle>().objName;
                    obstacleInformation[3].GetComponent<Text>().text = trackingObj.GetComponent<Obstacle>().description;
                    break;
            }
        }
        else
        {
            StatBarDissapear();
        }
    }
    void StatBarDissapear()
    {
        isTracking = false;
        informationUI.SetActive(false);
    }
    public void UpdateTimer(int time)
    {
        timerUI.GetComponent<Text>().text = time.ToString();
        if(time == 0)
        {
            timerUI.SetActive(false);
        }
    }
}
