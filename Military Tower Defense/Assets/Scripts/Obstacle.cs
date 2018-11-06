using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    public Sprite icon;
    public float health;
    public float maxHealth;
    public string objName;
    public string description;
    public GameObject healthbarHolder;
    public GameObject healthbar;
    bool damaged;
    public List<GameObject> targettedBy = new List<GameObject>();
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CheckHealth()
    {
        if (health < maxHealth)
        {
            if (!damaged)
            {
                damaged = true;
                healthbarHolder.SetActive(true);
            }
            healthbar.GetComponent<Image>().fillAmount = (1 / maxHealth) * health;
            if (health <= 0)
            {
                Death();
            }
        }
    }
    public void RemoveUnit(GameObject unit)
    {
        targettedBy.Remove(unit);
    }
    public void AddUnit(GameObject unit)
    {
        targettedBy.Add(unit);
    }
    public virtual void Death()
    {
        GetComponent<Collider>().enabled = false;
        for (int i = 0; i < targettedBy.Count; i++)
        {
            targettedBy[i].GetComponent<Enemy>().Repath();
        }
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().CheckIfTracked(gameObject);
        Destroy(gameObject);
    }
}
