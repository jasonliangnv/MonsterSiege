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

    private static PlayerStats instance;

    void Start()
    {

        DontDestroyOnLoad(gameObject);

        if(instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;

        health = startingHealth;
        money = startingMoney;

        HPText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        MoneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        // Prints HP to the UI
        HPText.text = string.Format("{00}", health);
        MoneyText.text = string.Format("{00}", money);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        health = startingHealth;
        money = startingMoney;
        HPText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        MoneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
    }
}
