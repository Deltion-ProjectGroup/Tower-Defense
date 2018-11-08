using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    public GameObject playMenu;
    public GameObject mainMenu;
    public GameObject optionMenu;
    // Use this for initialization
    void Start () {
        if (PlayerPrefs.HasKey("SFXvol"))
        {
            print(PlayerPrefs.GetFloat("SFXvol"));
            optionMenu.GetComponent<Options>().audioMixer.SetFloat("SFXvol", PlayerPrefs.GetFloat("SFXvol"));
            optionMenu.GetComponent<Options>().audioMixer.SetFloat("Musicvol", PlayerPrefs.GetFloat("Musicvol"));
            optionMenu.GetComponent<Options>().audioMixer.SetFloat("Mastervol", PlayerPrefs.GetFloat("Mastervol"));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Play()
    {
        playMenu.SetActive(true);
    }
    public void Resume()
    {
        gameObject.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Options()
    {
        optionMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
}
