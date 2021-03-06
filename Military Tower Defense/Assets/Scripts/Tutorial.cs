﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
    public Dialog.DialogText[] dialogTexts;
    int currentEvent;
    public Vector3[] positions;
    public Vector3[] scales;
    public GameObject tutorialIndicator;
    public static bool tester = true;
	// Use this for initialization
	void Start () {
        PerformEvent(currentEvent);
        LevelManager.canChangeSpeed = false;
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
                tester = false;
                EventManager.OnDialogComplete += NextEvent;
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[0].dialogSpeech); //INTRODUCES HIMSELF
                break;
            case 2:
                Time.timeScale = 1;
                tester = true;
                StartCoroutine(Delay(3.5f)); //LETS YOU WAIT TILL THE ENEMY IS IN THE FIELD
                break;
            case 3:
                Time.timeScale = 0;
                tester = false;;
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[1].dialogSpeech); //TELLS YOU TO CLICK AT THE ENEMY
                break;
            case 4:
                EventManager.onInteract += CheckIfEnemy; //ADDS THE EVENT TO CHECK IF IT IS AN ENEMY
                break;
            case 5:
                Time.timeScale = 1;
                tester = true;
                StartCoroutine(Delay(GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().informationUI.GetComponent<Animation>().GetClip("StatBarAppear").length)); //WAITS FOR THE INFOBAR TO GET UP
                break;
            case 6:
                Time.timeScale = 0;
                tester = false;
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[2].dialogSpeech, true); // TELLS YOU WHAT THE INFOBAR IS
                break;
            case 7:
                tutorialIndicator.SetActive(true);
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[3].dialogSpeech, true); //SHOWS THE  BASE ENEMY STATS
                break;
            case 8:
                tutorialIndicator.transform.localPosition = positions[0];
                tutorialIndicator.transform.localScale = scales[0];
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[4].dialogSpeech, true); //SHOWS THE DESCRIPTION AND STATEFFECTS BAR
                break;
            case 9:
                tutorialIndicator.SetActive(false);
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[5].dialogSpeech); // TELLS YOU TO SHUT DOWN THE INFOMENU
                break;
            case 10:
                EventManager.onInteract += CheckIfMisclick; //ADDS THE EVENT TRIGGER IF YOU SHUT DOWN THE MENU
                break;
            case 11:
                Time.timeScale = 1;
                StartCoroutine(Delay(GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().informationUI.GetComponent<Animation>().GetClip("StatbarRemove").length + 3));
                break;
            case 12:
                Time.timeScale = 0;
                EventManager.onInteract -= CheckIfMisclick;
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[9].dialogSpeech); //SAYS THINGS ABOUT SPEEDIN UP TIME
                break;
            case 13:
                EventManager.OnSpacebar += NextEvent;
                break;
            case 14:
                EventManager.OnSpacebar -= NextEvent;
                EventManager.OnObstacleTakeDamage += NextEvent; //RESUMES TIME AFTER YOU CLOSE THE WINDOW AND ADDS THE NEW EVENT FOR IF WALL GETS ATTACKED
                Time.timeScale = 1;
                tester = true;
                LevelManager.canChangeSpeed = true;
                break;
            case 15:
                Time.timeScale = 0;
                tester = false;
                LevelManager.canChangeSpeed = false;
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[6].dialogSpeech); // TEXT AFTER WALL GETS ATTACKED
                break;
            case 16:
                EventManager.OnRightClick += NextEvent; //ADDS IF YOU RIGHTCLICK EVENT
                break;
            case 17:
                LevelManager.levelManager.AddCurrency(1, Vector3.zero);
                EventManager.OnRightClick -= NextEvent; //REMOVES IT
                EventManager.OnTurretPlaced += NextEvent; //PLAYS EVENT IF YOU PLACE ANY TURRET
                break;
            case 18:
                EventManager.OnTurretPlaced -= NextEvent; //REMOVES IT
                tutorialIndicator.SetActive(true);//ENABLES INDICATOR ETC
                tutorialIndicator.transform.localPosition = positions[1];
                tutorialIndicator.transform.localScale = scales[1];
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[7].dialogSpeech, true); //EXPLAINS THE REVOLVER
                break;
            case 19:
                tutorialIndicator.SetActive(false);
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().DialogMethod(dialogTexts[8].dialogSpeech); //SAYS HE IS LEAVING
                break;
            case 20:
                Time.timeScale = 1;
                tester = true;
                LevelManager.canChangeSpeed = true;
                break;
        }
    }
    public IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        NextEvent();
    }
    public void CheckIfEnemy(GameObject gO)
    {
        if(gO != null)
        {
            if (gO.tag == "Enemy")
            {
                NextEvent();
            }
        }
    }
    public void CheckIfMisclick(GameObject gO)
    {
        if(gO == null)
        {
            NextEvent();
        }
        else
        {
            if (gO.tag != "Enemy" && gO.tag != "Turret" && gO.tag != "Interactable")
            {
                NextEvent();
            }
        }
    }
}
