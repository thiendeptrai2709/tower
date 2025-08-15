using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

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

    [Header("UI")]
    public TMP_Text waveText;

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

            if (waveText != null)
                waveText.text = $"Wave {roundNumber} / {totalRounds}";

            StartCoroutine(SpawnRound(roundNumber));
        }
    }

    IEnumerator SpawnRound(int round)
    {
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

        // Tính bonus HP: cứ mỗi 5 round cộng thêm 50 HP
        int bonusHP = (round - 1) / 5 * 50;

        for (int i = 0; i < enemyCount; i++)
        {
            Transform chosenEnemy = possibleEnemies[Random.Range(0, possibleEnemies.Count)];
            GameObject enemyObj = Instantiate(chosenEnemy, spawnPoint.position, spawnPoint.rotation).gameObject;

            // Áp dụng bonus HP cho enemy
            Enemy enemyScript = enemyObj.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.SetupHP(bonusHP);
            }

            aliveEnemies++;
            yield return new WaitForSeconds(timeBetweenEnemies);
        }

        while (enemiesKilledThisRound < enemiesToKillPerRound)
        {
            yield return null;
        }

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
    }
}
