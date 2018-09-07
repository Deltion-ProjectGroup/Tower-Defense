using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour {
    public float damage;
    public float shotCooldown;
    List<GameObject> targets = new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider hit)
    {
        if(hit.gameObject.tag == "Enemy")
        {
            targets.Add(hit.gameObject);
            if (targets.Count == 1)
            {
                StartCoroutine(Turret());
            }
        }
    }
    private void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            targets.Remove(hit.gameObject);
            if(targets.Count <= 0)
            {
                StopCoroutine("Turret");
            }
        }
    }
    private IEnumerator Turret()
    {
        RaycastHit hitObject;
        transform.LookAt(targets[0].transform);
        if(Physics.Raycast(transform.position, transform.forward, out hitObject, 20))
        {
            if(hitObject.transform.gameObject.tag == "Enemy")
            {
                hitObject.transform.gameObject.GetComponent<Enemy>().health -= damage;
                if(hitObject.transform.gameObject.GetComponent<Enemy>().health <= 0)
                {
                    targets.RemoveAt(0);
                    hitObject.transform.gameObject.GetComponent<Enemy>().Death();
                }
            }
        }
        yield return new WaitForSeconds(shotCooldown);
        StartCoroutine(Turret());
    }
}
