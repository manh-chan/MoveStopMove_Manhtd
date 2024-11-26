using Custom.Indicators;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LevelManager : Singleton<LevelManager>
{
    public int numberOfBots = 5;
    public float respawnDelay = 3f;
    public int totalRank = 20;
    public Transform spawnArea;
    public List<GameObject> listEnemies = new List<GameObject>();
    public OffscreenIndicators indicators;
    public Player player;

    private int totalScore = 0;
    private void Update()
    {
        WinGame();
    }
    public void StartSpawn()
    {
        SpawnEnemies();
        indicators.AddTarget(player);
    }
    public void SpawnEnemies()
    {
        for (int i = 0; i < numberOfBots; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = spawnArea.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
        Enemys enemy = SimplePool.Spawn<Enemys>(ObjectType.Enemy, spawnPosition, Quaternion.identity);
        listEnemies.Add(enemy.gameObject);
        enemy.OnInit();
        indicators.AddTarget(enemy);
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        totalRank --;
        SimplePool.Despawn(enemy.GetComponent<Enemys>());
        StartCoroutine(RespawnEnemyAfterDelay());
        indicators.RemoveTarget(enemy);
    }

    private IEnumerator RespawnEnemyAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnEnemy();
    }
    public void AddScore(int score)
    {
        totalScore += score;
    }
    public void ResetGame()
    {
        foreach (var enemy in listEnemies)
        {
            if (enemy != null)
            {
                indicators.RemoveTarget(enemy);
                SimplePool.Despawn(enemy.GetComponent<Enemys>());
            }
        }
        listEnemies.Clear();
        SimplePool.ReleaseAll();
        totalRank = numberOfBots + 1;
        totalScore = 0;
        totalRank = 20;
        indicators.RemoveTarget(player.gameObject);
        player.OnInit();
    }
    public void WinGame()
    {
        if (totalRank <= 1) 
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Victory);
        }
    }
}
