using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy {
    public GameObject explosionParticles;
    // Use this for initialization
    public override IEnumerator Attack()
    {
        while (true)
        {
            //yield return new WaitForSeconds(attackAnim.clip.length);
            target.GetComponent<Obstacle>().health -= damage;
            if(Physics.Raycast(transform.position, transform.forward, out hitObj, attackRange))
            {
                if(hitObj.transform.gameObject.tag == "Targettable")
                {
                    GameObject explosion = Instantiate(explosionParticles, hitObj.point, Quaternion.identity);
                    Destroy(explosion, 2);
                }
            }
            target.GetComponent<Obstacle>().CheckHealth();
            yield return new WaitForSeconds(1 / attackSpeed);
        }
    }
}
