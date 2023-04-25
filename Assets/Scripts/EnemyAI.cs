using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float tempo;

    [SerializeField] private Animator model;
    private int index;
    private float turnSpeed;
    private bool waveStarted;
    private bool walking;

    Vector3 difference;
    Quaternion rotGoal;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        target = Waypoints.points[0];
        turnSpeed = 0.05f;
        waveStarted = false;
        walking = false;
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

            if (!walking)
            {
                model.SetTrigger("triggerRun");
                walking = true;
            }
        }

        difference = target.position - transform.position;
        direction = difference.normalized;
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
        transform.Translate(direction * tempo * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }       

    }

    void GetNextWaypoint()
    {
        if(index >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            // Reduce player HP here
            return;
        }

        index++;
        target = Waypoints.points[index];
    }
}
