using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlliedAI : MonoBehaviour
{
    [SerializeField] private Animator model;
    private Transform target;
    private float turnSpeed;
    private bool swapTarget;

    Vector3 difference;
    Quaternion rotGoal;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //target = enemies[0].transform;
    }

    // Update is called once per frame
    void Update()
    {
        difference = target.position - transform.position;
        direction = difference.normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
    }

    void UpdateTarget()
    {
        /*
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        if(swapTarget == true)
        {
            target = enemies;
            swapTarget = false;
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        if(other.tag == "Enemy")
        {
            swapTarget = true;
        }
        */
    }
}
