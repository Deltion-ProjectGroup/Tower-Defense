using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public GameObject transitionUI;
    public GameObject shopUI;
    public GameObject timerUI;
    public GameObject roundUI;
    public GameObject informationUI;
    public GameObject addMoneyInd;
    public GameObject moneyIndHolder;
    public GameObject moneyUI;
    public GameObject[] enemyInformation;
    public GameObject[] turretInformation;
    public GameObject[] obstacleInformation;
    public GameObject[] waveInformation;
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
            shopUI.GetComponent<RadialMenu>().SwitchShop();
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
    public IEnumerator UpdateCash(int addedAmt, int newCash)
    {
        string addedCash = "k";
        if(addedAmt < 0)
        {
            addedCash = "-";
        }
        else
        {
            addedCash = "+";
        }
        addedCash += addedAmt.ToString();
        moneyUI.GetComponent<Text>().text = "$" + newCash.ToString();
        GameObject g = Instantiate(addMoneyInd, Vector3.zero, Quaternion.identity, moneyIndHolder.transform);
        g.GetComponent<Text>().text = addedCash;
        yield return new WaitForSeconds(g.GetComponent<Animation>().clip.length);
        Destroy(g);
    }
    public void UpdateTimer(int time)
    {
        timerUI.GetComponent<Text>().text = time.ToString();
        if(time == 0)
        {
            timerUI.SetActive(false);
        }
    }
    public void ShowText(string textToShow)
    {
        roundUI.GetComponent<Text>().text = textToShow;
        roundUI.GetComponent<Animation>().Play();
    }
    public void ShowWaveInfo(LevelManager.Waves enemies)
    {
        /*
        waveInformation[0].SetActive(true);
        waveInformation[1].GetComponent<Text>().text = enemies.berserkers.ToString();
        waveInformation[2].GetComponent<Text>().text = enemies.melees.ToString();
        waveInformation[3].GetComponent<Text>().text = enemies.ranged.ToString();
        waveInformation[4].GetComponent<Text>().text = enemies.mortars.ToString();
        waveInformation[5].GetComponent<Text>().text = enemies.tanks.ToString();
        */
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PauseGame(bool resume = false)
    {
        if (resume)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public IEnumerator ChangeScene(string sceneName)
    {
        transitionUI.GetComponent<Animation>().Play("NAME");
        yield return new WaitForSeconds(transitionUI.GetComponent<Animation>().clip.length);
        SceneManager.LoadScene(sceneName);
    }
    public void RemoveWafeInfo()
    {
        waveInformation[0].SetActive(false);
    }
}
