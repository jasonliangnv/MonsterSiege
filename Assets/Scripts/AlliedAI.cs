using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlliedAI : MonoBehaviour
{   
    public Transform target;
    public float range;
    public float damage;
    
    [SerializeField] private Animator model;
    private float turnSpeed = 0.05f;
    private float timer = 0f;
    private float delay = 1.33f;

    Vector3 difference;
    Quaternion rotGoal;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        // Skips movement and attacks if there is no target
        if(target == null)
        {
            model.ResetTrigger("triggerHit");
            return;
        }

        // Rotates towards the target
        difference = target.position - transform.position;
        direction = difference.normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);

        // Unit attempts to hit nearest unit at animation hit rate
        if(target.GetComponent<EnemyAI>().IsDead() == false && timer == 0)
        {
            model.SetTrigger("triggerHit");
            target.GetComponent<EnemyAI>().TakeDamage(damage);

            timer += Time.deltaTime;
        }
        else
        {
            timer += Time.deltaTime;
        }
        
        if(timer >= delay)
        {
            timer = 0f;
        }
    }

    // Checks for the nearest enemy target
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortest = Mathf.Infinity;
        GameObject nearest = null;

        // Runs distance calculations
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortest)
            {
                shortest = distanceToEnemy;
                nearest = enemy;
            }
        }

        // Retargets based on nearest distance
        if (nearest != null && shortest <= range)
        {
            target = nearest.transform;
        }
        else
        {
            target = null;
        }
    }

    // Draws range of unit when selected
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
