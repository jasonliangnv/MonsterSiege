using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static int health;
    public static int money;

    public int startingHealth;
    public int startingMoney;

    public TextMeshProUGUI HPText;
    public TextMeshProUGUI MoneyText;
    public GameObject treasuresGrid;

    public static List<Sprite> treasures = new List<Sprite>();

    public static Dictionary<string, int> allyModifiers = new Dictionary<string, int>()
    {
        {"rangedAttack", 0},
        {"rangedRange", 0 },
        {"meleeAttack", 0},
        {"meleeRange", 0},
    };

    public static Dictionary<string, int> enemyModifiers = new Dictionary<string, int>()
    {
        {"speed", 0}
    };

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
        treasuresGrid = GameObject.Find("TreasuresGrid");

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
        //health = startingHealth;
        
        // Gives the player money depending on the level to make up for last levels unit costs
        // We are removing this line since we implemented a refund system
        /*
        if(SceneManager.GetActiveScene().buildIndex == 2)
            money += startingMoney;
        else if(SceneManager.GetActiveScene().buildIndex == 3)
            money += startingMoney * 2;
        */
    
        HPText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        MoneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        treasuresGrid = GameObject.Find("TreasuresGrid");

        //loads sprites into grid
        foreach (Sprite treasureSprite in treasures)
        {
            GameObject spriteObject = new GameObject("SpriteObject");
            spriteObject.transform.SetParent(treasuresGrid.transform);
            Image image = spriteObject.AddComponent<Image>();
            image.sprite = treasureSprite;
            spriteObject.layer = 5;
        }
    }

    public static void AddTreasure(GameObject treasure)
    {
        //saves sprite from treasure
        Transform trans = treasure.transform;
        Transform childTrans = trans.Find("Portrait");
        if (childTrans != null)
        {
            treasures.Add(childTrans.gameObject.GetComponent<Image>().sprite);
        }
    }
}
