using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BeaconSpawner : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject healingBeacon;
    public GameObject armorBeacon;
    public GameObject armorHealingBeacon;

    private GameManager gameManager;

    private bool isOccupied = false;
    [HideInInspector] public bool isSpawned;

    private int i = 0;
    
    void Start()
    {
        isSpawned = false;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.nextWave && isSpawned == false)
        {
            Spawn();
            isSpawned = true;
        }
    }

    void Spawn()
    {
        Instantiate(healingBeacon, spawnPoints[(i) % 8].transform.position,
            quaternion.identity);
        Instantiate(armorBeacon, spawnPoints[(i + 1) % 8].transform.position,
            quaternion.identity);
        Instantiate(armorHealingBeacon, spawnPoints[(i + 2) % 8].transform.position,
            quaternion.identity);
        i++;
    }
}