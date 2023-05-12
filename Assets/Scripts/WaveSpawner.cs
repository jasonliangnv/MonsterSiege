using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class WaveSpawner : MonoBehaviour
{
    public int EnemiesAlive;

    //array of defined wave types to give to wavespawner
    public Wave[] waves;

    public Transform spawnPoint;

    public TMP_Text waveCountText;

    public Image waveUnitIcon;
    public TMP_Text waveUnitNumber;

    public GameObject fireworks1;
    public GameObject fireworks2;    
    public GameObject winTextObject;
    public GameObject treasureSelect;
    public AudioSource winAudio;
    public AudioSource backgroundAudio;

    public float timeBetweenWaves = 5f;

    private float countdown = 2f;

    private int waveNumber = 0;
    private bool endingLevel;

    void Start()
    {
        EnemiesAlive = 0;
        winTextObject.SetActive(false);
        treasureSelect.SetActive(false);
        endingLevel = false;
    }

    void Update()
    {

        // Debug function to immediately end level 1
        /*
        if(SceneManager.GetActiveScene().buildIndex != 2 && endingLevel == false)
        {
            endingLevel = true;
            winAudio.Play();
            backgroundAudio.Stop();
            winTextObject.SetActive(true);
            Invoke("SelectTreasure", 5);
        }
        */
        
        if(waveNumber <= (waves.Length - 1) && endingLevel == false)
        {
            if(waves[waveNumber] != null)
            {
                Wave upcomingWave = waves[waveNumber];
                waveUnitIcon.sprite = upcomingWave.icon;
                waveUnitNumber.text = upcomingWave.count.ToString();
            }

            waveCountText.text = "Wave: " + (waveNumber + 1).ToString();
            if(countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
            }

            countdown -= Time.deltaTime;
        }

        // Outputs win audio, win text, and treasure scene if it is not the last level
        if(waveNumber == waves.Length && EnemiesAlive == 0 && endingLevel == false)
        {
            endingLevel = true;
            winAudio.Play();
            backgroundAudio.Stop();
            winTextObject.SetActive(true);

            if(SceneManager.GetActiveScene().buildIndex != 3)
            {
                for(int i = 0; i <= 5; i++)
                {
                    StartCoroutine(LaunchFireworks());
                }

                Invoke("SelectTreasure", 6);
            }
            else
            {
                for(int i = 0; i <= 5; i++)
                {
                    StartCoroutine(LaunchFireworks());
                }

                Invoke("ReturnToMenu", 5);
            }
        }
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
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }

    void SelectTreasure()
    {
        winTextObject.SetActive(false);
        treasureSelect.SetActive(true);
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public IEnumerator LaunchFireworks()
    {
        fireworks1.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        fireworks2.SetActive(true);
    }
}
