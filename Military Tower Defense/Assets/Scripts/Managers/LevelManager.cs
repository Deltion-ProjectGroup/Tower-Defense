using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public Vector3 targetDestination;
    public Vector3[] spawnPositions;
    public static LevelManager levelManager;
    public Waves[] waves;
    public Enemies enemies;
    public List<GameObject> aliveEnemies = new List<GameObject>();
    public float spawnDelay;
    public int intermissionTime;
    int currentWave = 0;
    public int currency;
	// Use this for initialization
    private void Awake()
    {
        levelManager = this;
    }
    void Start () {
        StartCoroutine(SpawnNextWave());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public IEnumerator SpawnNextWave()
    {
        for(int i = 0; i < waves[currentWave].berserkers; i++)
        {
            aliveEnemies.Add(Instantiate(enemies.berserker, spawnPositions[Random.Range(0, waves.Length)], Quaternion.identity));
            yield return new WaitForSeconds(spawnDelay);
        }
        for (int i = 0; i < waves[currentWave].melees; i++)
        {
            aliveEnemies.Add(Instantiate(enemies.melee, spawnPositions[Random.Range(0, waves.Length)], Quaternion.identity));
            yield return new WaitForSeconds(spawnDelay);
        }
        for (int i = 0; i < waves[currentWave].ranged; i++)
        {
            aliveEnemies.Add(Instantiate(enemies.ranged, spawnPositions[Random.Range(0, waves.Length)], Quaternion.identity));
            yield return new WaitForSeconds(spawnDelay);
        }
        for (int i = 0; i < waves[currentWave].tanks; i++)
        {
            aliveEnemies.Add(Instantiate(enemies.tank, spawnPositions[Random.Range(0, waves.Length)], Quaternion.identity));
            yield return new WaitForSeconds(spawnDelay);
        }
        for (int i = 0; i < waves[currentWave].mortars; i++)
        {
            aliveEnemies.Add(Instantiate(enemies.mortar, spawnPositions[Random.Range(0, waves.Length)], Quaternion.identity));
            yield return new WaitForSeconds(spawnDelay);
        }
        currentWave++;
    }
    public IEnumerator Intermission()
    {
        int timer = intermissionTime;
        for(int i = 0; i < intermissionTime; i++)
        {
            yield return new WaitForSeconds(1);
            timer--;
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().UpdateTimer(timer);
            //Decrement with 1 at ui timer
        }
        SpawnNextWave();
    }
    public void AddCurrency(int amount)
    {
        currency += amount;
    }
    public void CheckWave()
    {
        if(aliveEnemies.Count == 0)
        {
            //Yeet
            if(currentWave == waves.Length - 1)
            {
                // U WON
            }
            else
            {
                StartCoroutine(Intermission());
            }
        }
    }
    public void RemoveEnemy(GameObject enemy)
    {
        aliveEnemies.Remove(enemy);
    }
    [System.Serializable]
    public struct Waves
    {
        public int berserkers;
        public int ranged;
        public int melees;
        public int tanks;
        public int mortars;
    }
    [System.Serializable]
    public struct Enemies
    {
        public GameObject berserker;
        public GameObject melee;
        public GameObject ranged;
        public GameObject tank;
        public GameObject mortar;
    }
}
