using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOT : Effect {
    public int damage;
    public float ticksPerSecond;
    public int maxTicks;
    int ticks;
    public override void Start()
    {
        StartCoroutine(DamageDealer());
    }
    IEnumerator DamageDealer()
    {
        ticks = 0;
        for(int i = 0; ticks < maxTicks; ticks++)
        {
            yield return new WaitForSeconds(1 / ticksPerSecond);
            gameObject.GetComponent<Enemy>().health -= damage;
            gameObject.GetComponent<Enemy>().CheckHealth();
        }
        Destroy(this);
    }
    public override void Refresh()
    {
        ticks = 0;
    }
}
