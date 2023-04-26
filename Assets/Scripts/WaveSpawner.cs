using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    //array of defined wave types to give to wavespawner
    public Wave[] waves;

    public Transform spawnPoint;

    public TMP_Text waveCountText;

    public Image waveUnitIcon;
    public TMP_Text waveUnitNumber;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    private int waveNumber = 0;
    void Update()
    {
        if(waves[waveNumber] != null)
        {
            Wave upcomingWave = waves[waveNumber];
            waveUnitIcon.sprite = upcomingWave.icon;
            waveUnitNumber.text = upcomingWave.count.ToString();
        }

        waveCountText.text = "Wave: " + (waveNumber + 1).ToString();

        if(EnemiesAlive > 0)
        {
            return;
        }

        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        //takes wave in wave array, spawns enemies according to parameters
        Wave wave = waves[waveNumber];

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveNumber++;

        if(waveNumber == waves.Length)
        {
            //end level
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
