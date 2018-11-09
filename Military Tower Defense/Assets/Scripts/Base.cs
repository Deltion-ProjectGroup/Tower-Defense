using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : Obstacle {

    public override void Death()
    {
        Time.timeScale = 0;
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().loss.SetActive(true);
        base.Death();
    }
}
