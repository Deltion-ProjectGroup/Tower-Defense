﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour {
    public float baseDamage;
    public float damage;
    [Tooltip("Attacks per second")]
    public float attackSpeed;
    public virtual IEnumerator Attack()
    {
        yield return new WaitForEndOfFrame();
    }
}
