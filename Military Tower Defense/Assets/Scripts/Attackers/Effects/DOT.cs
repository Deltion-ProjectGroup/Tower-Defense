using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOT : Effect {
    public int damage;
    public float ticksPerSecond;
    public int maxTicks;
    public enum DOTType { Fire, Poison}
    public DOTType dotType;
    public int remainingTicks;
    public override void Start()
    {
        StartCoroutine(DamageDealer());
    }
    IEnumerator DamageDealer()
    {
        remainingTicks = maxTicks;
        while(remainingTicks > 0)
        {
            yield return new WaitForSeconds(1 / ticksPerSecond);
            remainingTicks--;
            gameObject.GetComponent<Enemy>().health -= damage;
            gameObject.GetComponent<Enemy>().CheckHealth();
        }
        Destroy(this);
    }
    public override void Refresh()
    {
        remainingTicks = maxTicks;
    }
}
