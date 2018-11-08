using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {
    string[] dialogs;
    int currentDialog;
    public Text text;
    bool isDone = true;
    public float textDelay;
    // Use this for initialization
    public void Update()
    {
        if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Interact"))
        {
            DialogInteract();
        }
    }
    void DialogInteract()
    {
        if (isDone)
        {
            if(currentDialog < dialogs.Length)
            {
                StartCoroutine(DialogWriter());
            }
            else
            {
                if(EventManager.OnDialogComplete != null)
                {
                    EventManager.OnDialogComplete();
                }
                gameObject.SetActive(false);
            }
        }
        else
        {
            StopAllCoroutines();
            isDone = true;
            text.text = dialogs[currentDialog];
            currentDialog++;
        }
    }
    public void Initializer(string[] dialogText)
    {
        dialogs = dialogText;
        currentDialog = 0;
        DialogInteract();
    }
    IEnumerator DialogWriter()
    {
        isDone = false;
        text.text = null;
        for(int i = 0; i < dialogs[currentDialog].Length; i++)
        {
            text.text += dialogs[currentDialog][i];
            yield return new WaitForSecondsRealtime(textDelay);
        }
        isDone = true;
        currentDialog++;
    }
    [System.Serializable]
    public struct DialogText
    {
        public string[] dialogSpeech;
    }
}
