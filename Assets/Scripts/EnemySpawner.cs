using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public EnemyType EnemyTypes;

    public GameObject enemy;
    private int enemyIndex;
    private int xPos;
    private int yPos = 15;
    private int zPos;
    public int enemyCount;
    private int enemySpawned;

    public int whenStart = 3; //wave number


    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (whenStart == 0)
            EnemyDrop();
    }

    private void Update()
    {
        if (gameManager.nextWave && gameManager.waveCount >= whenStart)
        {
            enemySpawned = 0;
            if ((gameManager.waveCount % 5) != 0)
            {
                gameManager.nextWaveTime = 20;
                NextWave();
            }
        }
    }

    void EnemyDrop()
    {
        gameManager.isWaiting = true;
        StartCoroutine(EnemyCoroutine(gameManager.nextWaveTime));
    }


    public void NextWave()
    {
        enemyCount++;
        EnemyDrop();
    }

    IEnumerator EnemyCoroutine(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameManager.isWaiting = false;

        int randomAmount = Random.Range(enemyCount - 1, enemyCount + 1);
        if (randomAmount == 0)
        {
            randomAmount = 1;
        }

        while (enemySpawned < randomAmount)
        {
            xPos = Random.Range(115, 127);
            zPos = Random.Range(96, 106);

            Instantiate(enemy, new Vector3(xPos, yPos, zPos), quaternion.identity);

            enemySpawned++;
        }
    }
}