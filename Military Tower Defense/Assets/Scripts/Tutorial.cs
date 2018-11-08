using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
    public Dialog.DialogText[] dialogTexts;
    int currentEvent;
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
                StartCoroutine(Delay(5.5f));
                break;
            case 3:
                Time.timeScale = 0;
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[1].dialogSpeech);
                break;
            case 4:
                EventManager.OnDialogComplete -= NextEvent;
                EventManager.onInteract += CheckIfEnemy;
                break;
            case 5:
                print("DUN");
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
}
