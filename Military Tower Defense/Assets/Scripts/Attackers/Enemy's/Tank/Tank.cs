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
            yield return new WaitForSeconds(attackTimeMarks[0] / baseAttackSpeed);
            if (Physics.Raycast(transform.position, transform.forward, out hitObj, attackRange))
            {
                if(hitObj.transform.gameObject.tag == "Targettable")
                {
                    target.GetComponent<Obstacle>().health -= damage;
                    target.GetComponent<Obstacle>().CheckHealth();
                    GameObject explosion = Instantiate(explosionParticles, hitObj.point, Quaternion.identity);
                    Destroy(explosion, 2);
                }
            }
            yield return new WaitForSeconds((attackTimeMarks[1] - attackTimeMarks[0]) / baseAttackSpeed);
        }
    }
}
