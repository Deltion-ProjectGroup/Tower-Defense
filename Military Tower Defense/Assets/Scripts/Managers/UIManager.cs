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
    public GameObject moneyUI;
    public GameObject[] healthBars;
    public GameObject[] enemyInformation;
    public GameObject[] turretInformation;
    public GameObject[] obstacleInformation;
    public GameObject[] waveInformation;
    public GameObject victory;
    public GameObject loss;
    public GameObject options;
    bool isInOpt;
    public bool canReset = true;
    public bool isTracking;
    bool canInfoToggle = true;
    public GameObject trackingObj;
    public GameObject dialogUI;
    public AudioClip[] audioclips;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //print(1.0f / Time.deltaTime); //FPS 
        if (Input.GetButtonDown("Cancel"))
        {
            if (isInOpt)
            {
                if (canReset)
                {
                    Time.timeScale = 1;
                }
                isInOpt = false;
                options.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                isInOpt = true;
                options.SetActive(true);
            }
        }
        if(isTracking && trackingObj != null)
        {
            UpdateStats();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            shopUI.GetComponent<RadialMenu>().SwitchShop();
        }
	}
    public void DialogMethod(string[] dialog, bool nextIsAlsoDialog = false)
    {
        dialogUI.SetActive(true);
        dialogUI.GetComponent<Dialog>().Initializer(dialog, nextIsAlsoDialog);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public IEnumerator ShowStats(GameObject target)
    {
        if (EventManager.onInteract != null)
        {
            EventManager.onInteract(target);
        }
        if (target == null)
        {
            StartCoroutine(StatBarDissapear());
        }
        else
        {
            if (target.tag == "Enemy" || target.tag == "Turret" || target.tag == "Targettable")
            {
                trackingObj = target;
                enemyInformation[0].SetActive(false);
                turretInformation[0].SetActive(false);
                obstacleInformation[0].SetActive(false);
                switch (trackingObj.tag)
                {
                    case "Enemy":
                        enemyInformation[0].SetActive(true);
                        break;
                    case "Turret":
                        turretInformation[0].SetActive(true);
                        break;
                    case "Targettable":
                        obstacleInformation[0].SetActive(true);
                        break;
                }
                UpdateStats();
                if (!isTracking && canInfoToggle)
                {
                    informationUI.GetComponent<AudioSource>().clip = audioclips[0];
                    informationUI.GetComponent<AudioSource>().Play();
                    canInfoToggle = false;
                    isTracking = true;
                    informationUI.GetComponent<Animation>().Play();
                    yield return new WaitForSeconds(informationUI.GetComponent<Animation>().clip.length);
                    canInfoToggle = true;

                }
            }
            else
            {
                StartCoroutine(StatBarDissapear());
            }
        }
    }
    public void UpdateStats()
    {
        switch (trackingObj.tag)
        {
            case "Enemy":
                enemyInformation[1].GetComponent<Image>().sprite = trackingObj.GetComponent<Attacker>().icon;
                enemyInformation[2].GetComponent<Text>().text = trackingObj.GetComponent<Enemy>().objName;
                enemyInformation[3].GetComponent<Text>().text = trackingObj.GetComponent<Enemy>().health.ToString() + "/" + trackingObj.GetComponent<Enemy>().maxHealth;
                enemyInformation[4].GetComponent<Text>().text = trackingObj.GetComponent<Enemy>().attackSpeed.ToString();
                enemyInformation[5].GetComponent<Text>().text = trackingObj.GetComponent<Enemy>().movementspeed.ToString();
                enemyInformation[6].GetComponent<Text>().text = trackingObj.GetComponent<Enemy>().damage.ToString();
                enemyInformation[7].GetComponent<Text>().text = trackingObj.GetComponent<Enemy>().description;
                enemyInformation[8].GetComponent<Image>().fillAmount = trackingObj.GetComponent<Enemy>().healthbar.GetComponent<Image>().fillAmount;
                enemyInformation[9].SetActive(false);
                if(trackingObj.GetComponent<DOT>() != null)
                {
                    switch (trackingObj.GetComponent<DOT>().dotType)
                    {
                        case DOT.DOTType.Fire:
                            enemyInformation[9].SetActive(true);
                            enemyInformation[9].GetComponentInChildren<Text>().text = Mathf.RoundToInt((trackingObj.GetComponent<DOT>().remainingTicks / trackingObj.GetComponent<DOT>().ticksPerSecond)).ToString();
                            break;
                        case DOT.DOTType.Poison:

                            break;
                    }
                }
                break;
            case "Turret":
                turretInformation[2].GetComponent<Text>().text = trackingObj.GetComponent<Turret>().objName;
                turretInformation[3].GetComponent<Text>().text = trackingObj.GetComponent<Turret>().baseAttackSpeed.ToString();
                turretInformation[4].GetComponent<Text>().text = trackingObj.GetComponent<Turret>().baseDamage.ToString();
                turretInformation[5].GetComponent<Text>().text = trackingObj.GetComponent<Turret>().description;
                turretInformation[6].GetComponent<Text>().text = "+" + trackingObj.GetComponent<Turret>().sellValue.ToString();
                break;
            case "Targettable":
                obstacleInformation[2].GetComponent<Text>().text = trackingObj.GetComponent<Obstacle>().objName;
                obstacleInformation[3].GetComponent<Text>().text = trackingObj.GetComponent<Obstacle>().description;
                obstacleInformation[4].GetComponent<Text>().text = trackingObj.GetComponent<Obstacle>().health.ToString() + "/" + trackingObj.GetComponent<Obstacle>().maxHealth.ToString();
                obstacleInformation[5].GetComponent<Image>().fillAmount = trackingObj.GetComponent<Obstacle>().healthbar.GetComponent<Image>().fillAmount;
                break;
        }
    }
    public void CheckIfTracked(GameObject target)
    {
        if(target == trackingObj)
        {
            StartCoroutine(StatBarDissapear());
        }
    }
    IEnumerator StatBarDissapear()
    {
        if (canInfoToggle && isTracking)
        {
            informationUI.GetComponent<AudioSource>().clip = audioclips[1];
            informationUI.GetComponent<AudioSource>().Play();
            canInfoToggle = false;
            informationUI.GetComponent<Animation>().Play("StatbarRemove");
            yield return new WaitForSeconds(informationUI.GetComponent<Animation>().GetClip("StatbarRemove").length);
            isTracking = false;
            trackingObj = null;
            enemyInformation[0].SetActive(false);
            turretInformation[0].SetActive(false);
            obstacleInformation[0].SetActive(false);
            canInfoToggle = true;
        }
    }
    public IEnumerator UpdateCash(int addedAmt, int newCash, Vector3 moneyPos)
    {
        string addedCash = "";
        GameObject g = Instantiate(addMoneyInd, moneyPos, Quaternion.identity, GameObject.FindGameObjectWithTag("WorldspaceCanvas").transform);
        if (addedAmt < 0)
        {
            g.GetComponent<Text>().color = Color.red;
        }
        else
        {
            g.GetComponent<Text>().color = Color.green;
            addedCash = "+";
        }
        addedCash += addedAmt.ToString();
        moneyUI.GetComponent<Text>().text = "$" + newCash.ToString();
        g.GetComponent<Text>().text = addedCash;
        for(int i = 0; i < 40; i++)
        {
            g.transform.Translate(new Vector3(0, 0.05f, 0));
            yield return new WaitForSeconds(0.03f);
        }
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
    public void ShowWaveInfo(int[] enemyAmts)
    {
        waveInformation[0].SetActive(true);
        for(int i = 1; i < waveInformation.Length; i++)
        {
            waveInformation[i].GetComponent<Text>().text = enemyAmts[(i - 1)].ToString();
        }
    }
    public void UpdateWaveInfo(int[] enemyAmts)
    {
        for (int i = 1; i < waveInformation.Length; i++)
        {
            waveInformation[i].GetComponent<Text>().text = enemyAmts[(i - 1)].ToString();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Sell()
    {
        GetComponent<AudioSource>().Play();
        trackingObj.GetComponent<BoxCollider>().enabled = false;
        LevelManager.levelManager.AddCurrency(trackingObj.GetComponent<Turret>().sellValue, trackingObj.transform.position);
        CheckIfTracked(trackingObj);
        Destroy(trackingObj);
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
