using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureController : MonoBehaviour
{
    public void BolsterDefenses()
    {
        PlayerStats.health += 15;
        PlayerStats.AddTreasure(gameObject);
    }

    public void PoisonEnemy()
    {
        PlayerStats.enemyModifiers["speed"] += 10;
        PlayerStats.AddTreasure(gameObject);
    }

    public void SharperSwords()
    {
        PlayerStats.allyModifiers["meleeAttack"] += 5;
        PlayerStats.AddTreasure(gameObject);
    }

    public void BetterFirepower()
    {
        PlayerStats.allyModifiers["rangedAttack"] += 5;
        PlayerStats.AddTreasure(gameObject);        
    }
}
