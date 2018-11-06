using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherTower : Turret {
    public GameObject missile;
    public Transform[] spawnPoints;
    [HideInInspector]
    public List<Rocket> currentMissiles = new List<Rocket>();
    public override IEnumerator Attack()
    {
        if(targets.Count > 0)
        {
            for(int i = 0; i < spawnPoints.Length; i++)
            {
                GetComponent<AudioSource>().Play();
                GameObject rocket = Instantiate(missile, spawnPoints[i].position, Quaternion.identity);
                rocket.transform.LookAt(targets[0].transform);
                currentMissiles.Add(rocket.GetComponent<Rocket>());
                currentMissiles[currentMissiles.Count - 1].target = targets[0];
            }
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
    public override void CleanTarget(GameObject unit)
    {
        if(unit.GetComponent<Enemy>().health <= 0)
        {
            while (currentMissiles.Count > 0)
            {
                currentMissiles[0].target = null;
                currentMissiles.RemoveAt(0);
            }
        }
        base.CleanTarget(unit);
    }
    public void Update()
    {
        if (targets.Count > 0)
        {
            Vector3 lookRotation = new Vector3(targets[0].GetComponent<Enemy>().heart.transform.position.x, turretParts[1].transform.position.y, targets[0].GetComponent<Enemy>().heart.transform.position.z);
            turretParts[1].transform.LookAt(lookRotation);
            turretParts[2].transform.LookAt(targets[0].GetComponent<Enemy>().heart.transform);
        }
    }
}
