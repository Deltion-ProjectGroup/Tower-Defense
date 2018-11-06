using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public enum EnemyType { Berserker, Melee, Ranged, Mortar, Tank}
    public Vector3 targetDestination;
    public Vector3[] spawnPositions;
    public static LevelManager levelManager;
    public Waves[] waves;
    public Enemies enemies;
    public List<GameObject> aliveEnemies = new List<GameObject>();
    public List<GameObject> roundBullets;
    public int currentBullet;
    public float spawnDelay;
    public int intermissionTime;
    int nextWave = 0;
    public string newRoundText;
    public int currency;
    bool doneSpawning;
    public Vector3 bulletScaler;
	// Use this for initialization
    private void Awake()
    {
        if(waves.Length < roundBullets.Count - 1)
        {
            throw new System.Exception("NotEnoughWavesException");
        }
        else
        {
            for (int i = 0; i < waves.Length; i++)
            {
                if (waves[i].enemies.Length <= 0)
                {
                    throw new System.Exception("ZeroWaveEnemyException");
                }
                else
                {
                    for (int q = 0; q < waves[i].enemies.Length; q++)
                    {
                        if (waves[i].enemies[q].spawnAmount <= 0)
                        {
                            throw new System.Exception("ZeroSpawnException");
                        }
                    }
                }
            }
        }
        levelManager = this;
    }
    void Start () {
        Initialize();
        StartCoroutine(SpawnNextWave());
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Level 1");
        }
	}
    public IEnumerator SpawnNextWave()
    {
        int waveBackup = nextWave;
        StartCoroutine(SpinBarrel(nextWave));
        nextWave++;
        doneSpawning = false;
        UIManager uiManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
        string waveText = newRoundText + " " + (nextWave).ToString();
        uiManager.ShowText(waveText);
        yield return new WaitForSeconds(uiManager.roundUI.GetComponent<Animation>().clip.length);
        for(int i = 0; i < waves[waveBackup].enemies.Length; i++)
        {
            for(int q = 0; q < waves[waveBackup].enemies[i].spawnAmount; q++)
            {
                yield return new WaitForSeconds(spawnDelay);
                switch (waves[waveBackup].enemies[i].enemy)
                {
                    case EnemyType.Berserker:
                        aliveEnemies.Add(Instantiate(enemies.berserker, spawnPositions[Random.Range(0, spawnPositions.Length)], Quaternion.identity));
                        break;
                    case EnemyType.Melee:
                        aliveEnemies.Add(Instantiate(enemies.melee, spawnPositions[Random.Range(0, spawnPositions.Length)], Quaternion.identity));
                        break;
                    case EnemyType.Ranged:
                        aliveEnemies.Add(Instantiate(enemies.ranged, spawnPositions[Random.Range(0, spawnPositions.Length)], Quaternion.identity));
                        break;
                    case EnemyType.Mortar:
                        aliveEnemies.Add(Instantiate(enemies.mortar, spawnPositions[Random.Range(0, spawnPositions.Length)], Quaternion.identity));
                        break;
                    case EnemyType.Tank:
                        aliveEnemies.Add(Instantiate(enemies.tank, spawnPositions[Random.Range(0, spawnPositions.Length)], Quaternion.identity));
                        break;
                }
            }
        }
        doneSpawning = true;
    }
    public IEnumerator SpinBarrel(int currentWave)
    {
        //Rotate by 60;
        //Put S always first
        GetComponent<AudioSource>().clip = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().audioclips[2];
        GetComponent<AudioSource>().Play();
        while (roundBullets[currentBullet].transform.localScale.x > 0)
        {
            roundBullets[currentBullet].transform.localScale -= bulletScaler;
            yield return new WaitForEndOfFrame();
        }
        roundBullets[currentBullet].transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(1);
        GetComponent<AudioSource>().clip = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().audioclips[3];
        GetComponent<AudioSource>().Play();

        for (int i = 0; i < 60; i++)
        {
            transform.Rotate(new Vector3(0, 0, -1));
            yield return new WaitForEndOfFrame();
        }
        if ((currentWave + roundBullets.Count) < waves.Length)
        {
            roundBullets[currentBullet].GetComponent<RoundInfo>().NewWaveImport(waves[currentWave + roundBullets.Count]);
            roundBullets[currentBullet].GetComponentInChildren<Text>().text = (currentWave + roundBullets.Count).ToString();
            roundBullets[currentBullet].transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            roundBullets[currentBullet].GetComponentInChildren<Text>().text = "E";
            print("There is no more waves for this bullet");
            // WIN
        }
        currentBullet++;
        if (currentBullet >= roundBullets.Count)
        {
            currentBullet = 0;
        }
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
    public void AddCurrency(int amount, Vector3 moneyIndSpawn)
    {
        currency += amount;
        StartCoroutine(GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().UpdateCash(amount, currency, moneyIndSpawn));
    }
    public void CheckWave()
    {
        if (doneSpawning)
        {
            if (aliveEnemies.Count == 0)
            {
                if (nextWave == waves.Length)
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
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().CheckIfTracked(enemy);
        aliveEnemies.Remove(enemy);
        roundBullets[currentBullet].GetComponent<RoundInfo>().RemoveEnemies(enemy.GetComponent<Enemy>().enemyType);
        CheckWave();
    }
    [System.Serializable]
    public struct Waves
    {
        public Enemeh[] enemies;
        /*
        public int berserkers;
        public int ranged;
        public int melees;
        public int tanks;
        public int mortars;
        */
    }
    [System.Serializable]
    public struct Enemeh
    {
        public EnemyType enemy;
        public int spawnAmount;
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
    private void Initialize()
    {
        for(int i = 1; i < roundBullets.Count; i++)
        {
            roundBullets[i].GetComponent<RoundInfo>().NewWaveImport(waves[i - 1]);
        }
    }
}
