using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoundInfo : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler{
    public LevelManager.Waves roundEnemies;
	// Use this for initialization
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().ShowWaveInfo(roundEnemies);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().RemoveWafeInfo();
    }
}
