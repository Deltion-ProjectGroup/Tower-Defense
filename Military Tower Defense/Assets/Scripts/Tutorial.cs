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
                StartCoroutine(Delay(7));
                break;
            case 3:
                print("HI");
                EventManager.OnDialogComplete -= NextEvent;
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[1].dialogSpeech);
                break;
        }
    }
    public IEnumerator Delay(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        NextEvent();
    }
}
