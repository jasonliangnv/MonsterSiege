using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int health = 10;
    public int money;

    public TextMeshProUGUI HPText;
    public GameObject loseTextObject;
    public AudioSource loseAudio;
    public AudioSource backgroundAudio;

    private bool endingLevel;

    void Start()
    {
        endingLevel = false;
    }

    void Update()
    {
        // Prints HP to the UI
        HPText.text = string.Format("{00}", health);

        // If player reaches 0 hp they lose the level
        if(health == 0 && endingLevel == false)
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
