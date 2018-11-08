using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
    public Dialog.DialogText[] dialogTexts;
    int currentEvent;
    public Vector3[] positions;
    public GameObject tutorialIndicator;
	// Use this for initialization
	void Start () {
        PerformEvent(currentEvent);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void NextEvent()
    {
        currentEvent++;
        PerformEvent(currentEvent);
    }
    public void PerformEvent(int performInt)
    {
        switch (performInt)
        {
            case 0:
                StartCoroutine(Delay(5));
                break;
            case 1:
                Time.timeScale = 0;
                EventManager.OnDialogComplete += NextEvent;
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[0].dialogSpeech);
                break;
            case 2:
                Time.timeScale = 1;
                StartCoroutine(Delay(3.5f));
                break;
            case 3:
                Time.timeScale = 0;
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[1].dialogSpeech);
                break;
            case 4:
                EventManager.onInteract += CheckIfEnemy;
                break;
            case 5:
                Time.timeScale = 1;
                StartCoroutine(Delay(GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().informationUI.GetComponent<Animation>().GetClip("StatBarAppear").length));
                break;
            case 6:
                Time.timeScale = 0;
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[2].dialogSpeech, true);
                break;
            case 7:
                tutorialIndicator.SetActive(true);
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[3].dialogSpeech, true);
                break;
            case 8:
                print("KEK");
                tutorialIndicator.transform.localPosition = positions[0];
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[4].dialogSpeech, true);
                break;
            case 9:
                tutorialIndicator.SetActive(false);
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[4].dialogSpeech);
                break;
            case 10:
                EventManager.onInteract += CheckIfMisclick;
                break;
            case 11:
                Time.timeScale = 1;
                EventManager.onInteract -= CheckIfMisclick;
                break;
        }
    }
    public IEnumerator Delay(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        NextEvent();
    }
    public void CheckIfEnemy(GameObject gO)
    {
        if(gO.tag == "Enemy")
        {
            NextEvent();
        }
    }
    public void CheckIfMisclick(GameObject gO)
    {
        if(gO.tag != "Enemy" && gO.tag != "Turret" && gO.tag != "Interactable")
        {
            NextEvent();
        }
    }
}
