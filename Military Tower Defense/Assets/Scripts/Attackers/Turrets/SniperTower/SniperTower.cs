using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : Turret {
    public AudioSource[] audioSources;
    // Use this for initialization
    public override IEnumerator Attack()
    {
        bulletParticles.Play();
        if(targets.Count > 0)
        {
            audioSources[0].Play();
            audioSources[1].Play();
            targets[0].GetComponent<Enemy>().health -= baseDamage;
            targets[0].GetComponent<Enemy>().CheckHealth();
            if (targets.Count > 0)
            {
                yield return new WaitForSeconds(1 / baseAttackSpeed);
                if (targets.Count > 0)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }
    public void Update()
    {
        if (targets.Count > 0)
        {
            Vector3 lookRotation = new Vector3(targets[0].GetComponent<Enemy>().heart.transform.position.x, turretParts[1].transform.position.y, targets[0].GetComponent<Enemy>().heart.transform.position.z);
            turretParts[1].transform.LookAt(lookRotation);
        }
    }
}
