using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public GameObject mainMenu;
    public GameObject optionMenu;
    bool inOpt;
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
        if (Input.GetButtonDown("Cancel"))
        {
            if (inOpt)
            {
                inOpt = false;
                optionMenu.SetActive(false);
                mainMenu.SetActive(true);
            }
            else
            {
                inOpt = true;
                optionMenu.SetActive(true);
                mainMenu.SetActive(false);
            }
        }
	}
    public void Play(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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
