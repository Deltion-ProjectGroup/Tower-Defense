using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    public float health;
    public List<GameObject> targettedBy = new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void CheckHealth()
    {
        if(health <= 0)
        {
            for(int i = 0; i < targettedBy.Count; i++)
            {
                targettedBy[i].GetComponent<Enemy>().Repath();
            }
            Destroy(gameObject);
        }
    }
}
