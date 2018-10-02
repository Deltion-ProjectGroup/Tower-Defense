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
    public string newRoundText;
    public int currency;
    bool doneSpawning;
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
        doneSpawning = false;
        UIManager uiManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
        string waveText = newRoundText + " " + (currentWave + 1).ToString();
        uiManager.ShowText(waveText);
        yield return new WaitForSeconds(uiManager.roundUI.GetComponent<Animation>().clip.length);
        List<GameObject> enemieList = new List<GameObject>();
        for(int i = 0; i < waves[currentWave].berserkers; i++)
        {
            enemieList.Add(enemies.berserker);
        }
        for (int i = 0; i < waves[currentWave].ranged; i++)
        {
            enemieList.Add(enemies.ranged);
        }
        for (int i = 0; i < waves[currentWave].melees; i++)
        {
            enemieList.Add(enemies.melee);
        }
        for (int i = 0; i < waves[currentWave].tanks; i++)
        {
            enemieList.Add(enemies.tank);
        }
        for (int i = 0; i < waves[currentWave].mortars; i++)
        {
            enemieList.Add(enemies.mortar);
        }
        int listLength = enemieList.Count;
        for(int i = 0; i < listLength; i++)
        {
            yield return new WaitForSeconds(spawnDelay);
            int randomNum = Random.Range(0, enemieList.Count);
            aliveEnemies.Add(Instantiate(enemieList[randomNum], spawnPositions[Random.Range(0, spawnPositions.Length)], Quaternion.identity));
            enemieList.RemoveAt(randomNum);
        }
        currentWave++;
        doneSpawning = true;
    }
    public IEnumerator Intermission()
    {
        UIManager uiManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
        uiManager.ShowText("Intermission");
        uiManager.timerUI.SetActive(true);
        int timer = intermissionTime;
        uiManager.UpdateTimer(timer);
        for (int i = 0; i < intermissionTime; i++)
        {
            yield return new WaitForSeconds(1);
            timer--;
            uiManager.UpdateTimer(timer);
        }
        StartCoroutine(SpawnNextWave());
    }
    public void AddCurrency(int amount)
    {
        currency += amount;
    }
    public void CheckWave()
    {
        if (doneSpawning)
        {
            if (aliveEnemies.Count == 0)
            {
                if (currentWave == waves.Length)
                {
                    // U WON
                }
                else
                {
                    StartCoroutine(Intermission());
                }
            }
        }
    }
    public void RemoveEnemy(GameObject enemy)
    {
        aliveEnemies.Remove(enemy);
        CheckWave();
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
