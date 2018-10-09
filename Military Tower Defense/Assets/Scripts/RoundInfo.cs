using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoundInfo : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler{
    bool isLooking;
    public int[] aliveEnemies;

    // Use this for initialization
    public void Update()
    {
        if (isLooking)
        {
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().UpdateWaveInfo(aliveEnemies);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(aliveEnemies.Length > 0)
        {
            isLooking = true;
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().ShowWaveInfo(aliveEnemies);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isLooking = false;
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().RemoveWafeInfo();
    }
    int[] CalculateEnemies(LevelManager.Waves newWave)
    {
        int[] enemies = new int[5];
        for(int i = 0; i < newWave.enemies.Length; i++)
        {
            switch (newWave.enemies[i].enemy)
            {
                case LevelManager.EnemyType.Berserker:
                    enemies[0] += newWave.enemies[i].spawnAmount;
                    break;
                case LevelManager.EnemyType.Melee:
                    enemies[1] += newWave.enemies[i].spawnAmount;
                    break;
                case LevelManager.EnemyType.Ranged:
                    enemies[2] += newWave.enemies[i].spawnAmount;
                    break;
                case LevelManager.EnemyType.Mortar:
                    enemies[3] += newWave.enemies[i].spawnAmount;
                    break;
                case LevelManager.EnemyType.Tank:
                    enemies[4] += newWave.enemies[i].spawnAmount;
                    break;
            }
        }
        return enemies;
    }
    public void RemoveEnemies(LevelManager.EnemyType enemyType)
    {
        switch (enemyType)
        {
            case LevelManager.EnemyType.Berserker:
                aliveEnemies[0]--;
                break;
            case LevelManager.EnemyType.Melee:
                aliveEnemies[1]--;
                break;
            case LevelManager.EnemyType.Ranged:
                aliveEnemies[2]--;
                break;
            case LevelManager.EnemyType.Mortar:
                aliveEnemies[3]--;
                break;
            case LevelManager.EnemyType.Tank:
                aliveEnemies[4]--;
                break;
        }
    }
    public void NewWaveImport(LevelManager.Waves newEnemies)
    {
        aliveEnemies = CalculateEnemies(newEnemies);
    }
}
