using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static int health = 10;
    public static int money = 50;

    public int startingHealth;
    public int startingMoney;

    public TextMeshProUGUI HPText;
    public TextMeshProUGUI MoneyText;

    public GameObject loseTextObject;
    public AudioSource loseAudio;
    public AudioSource backgroundAudio;

    private bool endingLevel;

    void Start()
    {
        health = startingHealth;
        money = startingMoney;

        endingLevel = false;
    }

    void Update()
    {
        // Prints HP to the UI
        HPText.text = string.Format("{00}", health);
        MoneyText.text = string.Format("{00}", money);

        // If player reaches 0 hp they lose the level
        if (health == 0 && endingLevel == false)
        {
            endingLevel = true;
            loseAudio.Play();
            backgroundAudio.Stop();
            loseTextObject.SetActive(true);
            Invoke("PlayNextLevel", 5);            
        }
    }

    // Plays next level
    void PlayNextLevel()
    {
        SceneManager.LoadScene(0);
    }
}
