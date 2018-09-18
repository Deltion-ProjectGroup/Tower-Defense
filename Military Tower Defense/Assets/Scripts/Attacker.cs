using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour {
    public string objName;
    public string description;
    [Header("Damage")]
    public float baseDamage;
    [HideInInspector]
    public float damage;
    [Header("AttackSpeed")]
    [Tooltip("Attacks per second")]
    public float baseAttackSpeed;
    [HideInInspector]
    public float attackSpeed;
    public Animation attackAnim;
    public virtual IEnumerator Attack()
    {
        yield return new WaitForEndOfFrame();
    }
}
