using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool paused;

    public GameObject loseTextObject;
    public AudioSource loseAudio;
    public AudioSource backgroundAudio;

    private bool gameEnded = false;

    void Update()
    {
        if (gameEnded)
        {
            return;
        }

        if(PlayerStats.health <= 0 && gameEnded == false)
        {
            loseAudio.Play();
            backgroundAudio.Stop();
            loseTextObject.SetActive(true);
            Invoke("EndGame", 5);
            gameEnded = true;
        }
    }

    void EndGame()
    {
        SceneManager.LoadScene(0);
    }
}
