using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour {
    public AudioMixer audioMixer;
    public void ChangeSFX(Slider slider)
    {
        print(slider.value);
        audioMixer.SetFloat("SFXvol", slider.value);
        PlayerPrefs.SetFloat("SFXvol", slider.value);
        PlayerPrefs.Save();
    }
    public void ChangeMusic(Slider slider)
    {
        audioMixer.SetFloat("Musicvol", slider.value);
        PlayerPrefs.SetFloat("Musicvol", slider.value);
        PlayerPrefs.Save();
    }
    public void ChangeMaster(Slider slider)
    {
        audioMixer.SetFloat("Mastervol", slider.value);
        PlayerPrefs.SetFloat("Mastervol", slider.value);
        PlayerPrefs.Save();
    }
    public void GoToMenu(GameObject menuScreen)
    {
        menuScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
