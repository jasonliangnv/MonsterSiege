using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int health;
    public int startHealth;

    public static int money;
    public int startMoney;

    private void Start()
    {
        money = startMoney;
        health = startHealth;
    }

    public void reduceHealth(int damage)
    {
        health = -damage;
    }
}
