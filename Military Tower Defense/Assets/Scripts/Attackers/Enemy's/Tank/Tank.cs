using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy {
    public GameObject explosionParticles;
    public GameObject[] tires;
    public Vector3 rotateAmt;
    // Use this for initialization
    public override IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackSpeed);
            if (Physics.Raycast(heart.transform.position, transform.forward, out hitObj, 10, targettable, QueryTriggerInteraction.Ignore))
            {
                if(hitObj.transform.gameObject.tag == "Targettable")
                {
                    audioSources[0].Play();
                    audioSources[1].Play();
                    target.GetComponent<Obstacle>().health -= damage;
                    target.GetComponent<Obstacle>().CheckHealth();
                    GameObject explosion = Instantiate(explosionParticles, hitObj.point, Quaternion.identity);
                    Destroy(explosion, 2);
                }
            }
        }
    }
    public override void Update()
    {
        /*if(!attacking && !dead)
        {
            for(int i = 0; i < tires.Length; i++)
            {
                tires[i].transform.Rotate(rotateAmt * Time.deltaTime);
            }
        }*/
        base.Update();
    }
}
