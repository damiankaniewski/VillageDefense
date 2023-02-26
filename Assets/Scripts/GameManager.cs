using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    Goblin,
    Zombie,
    BossWejmar
};

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI enemiesAmountText;
    public TextMeshProUGUI coinScoreText;
    public TextMeshProUGUI wavesCountdownText;
    [HideInInspector] public bool nextWave = false;
    public int waveCount = 0;
    public int nextWaveTime = 20;
    private int counter;
    public int enemiesAmount;

    private BeaconSpawner beaconSpawner;

    [HideInInspector] public int coinScore = 0;
    [HideInInspector] public bool isWaiting;
    //cursor
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    /*void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }*/
    
    private void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        
        beaconSpawner = FindObjectOfType<BeaconSpawner>();
        enemiesAmount = EnemiesAmount();
        waveText.SetText("Wave: " + waveCount);
        enemiesAmountText.SetText("Enemies: " + enemiesAmount);

        StartCoroutine(WavesCountdown());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            coinScore += 100;
        }
        
        
        nextWave = false;
        enemiesAmount = EnemiesAmount();
        if (enemiesAmount != 0)
        {
            enemiesAmountText.SetText("Enemies: " + enemiesAmount);
        }
        else
        {
            enemiesAmountText.SetText("Enemies incoming...");
        }
        

        coinScoreText.SetText("x" + coinScore);

        if (counter > 0)
        {
            if ((waveCount % 5) != 0)
            {
                wavesCountdownText.SetText("New wave in " + counter + " seconds!");
            }
            else
            {
                wavesCountdownText.SetText("Boss incoming in " + counter + " seconds!");
            }
        }
        else
        {
            wavesCountdownText.SetText("");
        }

        if (enemiesAmount == 0 && isWaiting == false)
        {
            nextWave = true;
            NextWave();
        }

        if (nextWave)
        {
            if ((waveCount % 5) == 0)
            {
                waveText.SetText("Wave: " + waveCount + " - boss");
            }
            else
            {
                waveText.SetText("Wave: " + waveCount);
            }
        }
    }

    public void NextWave()
    {
        beaconSpawner.isSpawned = false;
        nextWave = true;
        waveCount++;
        StartCoroutine(WavesCountdown());
    }


    public int EnemiesAmount()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    IEnumerator WavesCountdown()
    {
        if ((waveCount % 5) == 0)
        {
            counter = 30;
        }
        else
        {
            counter = 20;
        }
        
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
    }
}