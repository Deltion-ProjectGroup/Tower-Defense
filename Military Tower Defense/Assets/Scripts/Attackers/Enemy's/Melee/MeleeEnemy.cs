using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy {

    public override IEnumerator Attack()
    {
        base.Attack();
        while (true)
        {
            //yield return new WaitForSeconds(attackAnim.clip.length);
            target.GetComponent<Obstacle>().health -= damage;
            target.GetComponent<Obstacle>().CheckHealth();
            yield return new WaitForSeconds(1 / attackSpeed);
        }
    }
}
