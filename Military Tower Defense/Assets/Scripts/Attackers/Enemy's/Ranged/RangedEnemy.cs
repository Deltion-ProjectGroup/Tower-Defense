using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy {

    public override IEnumerator Attack()
    {
        base.Attack();
        while (true)
        {
            yield return new WaitForSeconds(attackTimeMarks[0] / baseAttackSpeed);
            target.GetComponent<Obstacle>().health -= damage;
            target.GetComponent<Obstacle>().CheckHealth();
            yield return new WaitForSeconds((attackTimeMarks[1] - attackTimeMarks[0]) / baseAttackSpeed);
        }
    }
}
