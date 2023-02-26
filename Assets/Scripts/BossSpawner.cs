using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossSpawner : MonoBehaviour
{
    public GameObject[] bosses;
    private int bossCount = 0;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if ((gameManager.waveCount % 5) == 0)
        {
            gameManager.nextWaveTime = 30;
            gameManager.isWaiting = true;
            StartCoroutine(BossSpawn(gameManager.nextWaveTime));
        }
    }

    IEnumerator BossSpawn(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameManager.isWaiting = false;
        
        int xPos = Random.Range(115, 127);
        int zPos = Random.Range(96, 106);
        int yPos = 15;
        
        Instantiate(bosses[bossCount], new Vector3(xPos, yPos, zPos), quaternion.identity);
        
        bossCount++;
    }
}
