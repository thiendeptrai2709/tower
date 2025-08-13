using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemy1;
    public Transform enemy2;
    public Transform enemy3;
    public Transform enemy4;

    public Transform spawnPoint;

    public float timeBetweenEnemies = 0.2f;
    public float timeBetweenRounds = 5f;

    private int roundNumber = 0;
    private int totalRounds = 10;

    public static int aliveEnemies = 0;

    private int enemiesKilledThisRound = 0;
    private int enemiesToKillPerRound = 50;

    private bool roundActive = false;

    public static WaveSpawner instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!roundActive && roundNumber < totalRounds)
        {
            roundActive = true;
            enemiesKilledThisRound = 0;
            roundNumber++;
            StartCoroutine(SpawnRound(roundNumber));
        }
    }

    IEnumerator SpawnRound(int round)
    {
        Debug.Log("Bắt đầu Round " + round);

        List<Transform> possibleEnemies = new List<Transform>();

        if (round == 1)
            possibleEnemies.Add(enemy1);
        else if (round == 2)
        {
            possibleEnemies.Add(enemy1);
            possibleEnemies.Add(enemy2);
        }
        else if (round == 3)
        {
            possibleEnemies.Add(enemy2);
            possibleEnemies.Add(enemy3);
        }
        else if (round == 4)
        {
            possibleEnemies.Add(enemy3);
            possibleEnemies.Add(enemy4);
        }
        else
        {
            possibleEnemies.Add(enemy1);
            possibleEnemies.Add(enemy2);
            possibleEnemies.Add(enemy3);
            possibleEnemies.Add(enemy4);
        }

        int enemyCount = enemiesToKillPerRound;

        for (int i = 0; i < enemyCount; i++)
        {
            Transform chosenEnemy = possibleEnemies[Random.Range(0, possibleEnemies.Count)];
            Instantiate(chosenEnemy, spawnPoint.position, spawnPoint.rotation);
            aliveEnemies++;
            yield return new WaitForSeconds(timeBetweenEnemies);
        }

        // Sau khi spawn xong, chờ đủ số enemy bị tiêu diệt mới bắt đầu round mới
        while (enemiesKilledThisRound < enemiesToKillPerRound)
        {
            yield return null;
        }

        Debug.Log("Kết thúc Round " + round);

        // Chờ 5 giây trước khi round tiếp theo
        yield return new WaitForSeconds(timeBetweenRounds);

        roundActive = false;
    }

    public static void EnemyKilled()
    {
        if (instance != null)
            instance.OnEnemyKilled();
    }

    private void OnEnemyKilled()
    {
        enemiesKilledThisRound++;
        aliveEnemies--;

        Debug.Log($"Enemy bị tiêu diệt: {enemiesKilledThisRound}/{enemiesToKillPerRound}");
    }
}
