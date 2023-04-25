using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlliedAI : MonoBehaviour
{   
    public Transform target;
    public float range;
    public float hitRate;
    
    [SerializeField] private Animator model;
    private float turnSpeed;
    private float hitCD;

    Vector3 difference;
    Quaternion rotGoal;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        //model.SetTrigger("triggerRun");
        turnSpeed = 0.05f;
        hitRate = 5f;
        hitCD = 0f;
        range = 7f;
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        // Skips movement and attacks if there is no target
        if(target == null)
        {
            return;
        }

        // Rotates towards the target
        difference = target.position - transform.position;
        direction = difference.normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);

        // Unit attempts to hit nearest unit at hit rate
        if (hitCD <= 0f && target != null)
        {
            model.SetTrigger("triggerHit");
            hitCD = 0.25f / hitRate;
        }

        hitCD -= Time.deltaTime;
    }

    // Checks for the nearest enemy target
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortest = Mathf.Infinity;
        GameObject nearest = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortest)
            {
                shortest = distanceToEnemy;
                nearest = enemy;
            }
        }

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
