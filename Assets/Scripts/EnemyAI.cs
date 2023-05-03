using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float tempo;

    // HP BAR DISPLAY WORK IN PROGRESS
    //[HideInInspector]
    
    public float health;
    public int money;

    //[Header("HP Bar")]
    //public Image healthBar;

    [SerializeField] private Animator model;
    private int index = 0;
    private float turnSpeed = 0.05f;
    private float delay = 1.3f;
    private float timer = 0f;
    private bool waveStarted = false;
    private bool walking = false;
    private bool dead = false;

    Vector3 difference;
    Quaternion rotGoal;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.points[0];
        tempo = tempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        // Prevents movement until user enters an input (may not be needed if we use a spawn system after user hits start wave)
        if(!waveStarted)
        {
            waveStarted = true;
            // Place a start condition here such as when the user hits start wave
            /*
            if()
            {
                waveStarted = true;
            }
            */

            // If the model is not walking, start walking
            if (!walking)
            {
                model.SetTrigger("triggerRun");
                walking = true;
            }
        }

        // Rotates the unit towards the target and begin walking towards the target if the unit is not dead
        if(!dead)
        {
            difference = target.position - transform.position;
            direction = difference.normalized;
            rotGoal = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
            transform.Translate(direction * tempo * Time.deltaTime, Space.World);
        }
        else
        {
            if(timer >= 0.3f)
            {
                // Plays death animation if dead
                model.SetTrigger("triggerDeath");
            }

            // Delay for death animation before destroying game object
            if(timer <= delay)
            {
                // Slows motion so it is a less sudden stop
                difference = target.position - transform.position;
                direction = difference.normalized;
                rotGoal = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
                transform.Translate(direction * tempo/2 * Time.deltaTime, Space.World);

                timer += Time.deltaTime;
            }

            if(timer >= delay)
            {
                // Subtracts one from the enemy count
                GameObject waveController = GameObject.Find("GameManager");
                WaveSpawner levelTracker = waveController.GetComponent<WaveSpawner>();

                PlayerStats.money += money;
                levelTracker.EnemiesAlive--;

                Destroy(gameObject);
            }
        }

        // If we reach the target update to the next target
        if(Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }       

    }

    // Updates waypoint and if the unit reaches the end of the map it will deal damage to the player health
    void GetNextWaypoint()
    {
        if(index >= Waypoints.points.Length - 1)
        {
            // Subtracts one from the enemy count
            GameObject waveController = GameObject.Find("GameManager");
            WaveSpawner levelTracker = waveController.GetComponent<WaveSpawner>();

            levelTracker.EnemiesAlive--;

            Destroy(gameObject);

            PlayerStats.health--;            

            return;
        }

        index++;
        target = Waypoints.points[index];
    }

    // Runs damage calculation if hit by a unit
    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            dead = true;
            gameObject.tag = "Untagged";
            walking = false;
        }
    }

    // Checks if the unit is dead
    public bool IsDead()
    {
        if(dead)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
