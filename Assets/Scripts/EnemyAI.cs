using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public Transform target3;
    public float tempo;

    [SerializeField] private Animator model;
    private float turnSpeed;
    private bool waveStarted;
    private bool walking;
    private bool swapTarget;

    Vector3 difference;
    Transform activeTarget;
    Quaternion rotGoal;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        turnSpeed = 0.1f;
        waveStarted = false;
        walking = false;
        swapTarget = false;
        tempo = tempo / 60f;
        activeTarget = target1;
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

            if (!walking)
            {
                model.SetTrigger("triggerRun");
                walking = true;
            }
        }

        // If the enemy unit is not at the first turn, keep walking towards the target
        if(activeTarget == target1)
        {
                if(swapTarget == false)
                {
                    difference = activeTarget.position - transform.position;
                    direction = difference.normalized;
                    rotGoal = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
                    transform.position += new Vector3(tempo * Time.deltaTime, 0, 0);                    
                }
                else
                {
                    activeTarget = target2;
                    swapTarget = false;
                }
        }
        // Else if the enemy unit is not at the second turn, keep walking towards the target
        else if(activeTarget == target2)
        {
            if(swapTarget == false)
            {
                difference = activeTarget.position - transform.position;
                direction = difference.normalized;
                rotGoal = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
                transform.position += new Vector3(0, 0, tempo * Time.deltaTime);                      
            }
            else
            {
                activeTarget = target3;
                swapTarget = false;
            }
        }
        // Else if the enemy unit is not at the player loss zone, keep walking towards the target
        else if(activeTarget == target3)
        {
            if(swapTarget == false)
            {
                difference = activeTarget.position - transform.position;
                direction = difference.normalized;
                rotGoal = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
                transform.position += new Vector3(tempo * Time.deltaTime, 0, 0);                  
            }
            else
            {
                // Place player loses health here
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyPath")
        {
            swapTarget = true;
        }
    }
}
