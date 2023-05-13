using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class AlliedAI : MonoBehaviour
{   
    public Transform target;
    public float range;
    public float damage;
    public int cost;
    public bool ranged;
    public GameObject fireball;
    public Transform firePoint;
    public Sprite icon;

    [SerializeField] private Animator model;
    private float turnSpeed = 0.1f;
    private float timer = 0f;
    private float delay;

    Vector3 difference;
    Quaternion rotGoal;
    Vector3 direction;
    GameObject hitAudio;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.25f);

        if(ranged)
            delay = 1.33f;
        else
            delay = 1.33f;

        damage += PlayerStats.allyModifiers["attack"];
        range += PlayerStats.allyModifiers["range"];
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
        if(target.GetComponent<EnemyAI>().IsDead() == false && timer == 0 && ranged == false)
        {
            model.SetTrigger("triggerHit");
            
            hitAudio = Instantiate(GameObject.Find("HitSound"));
            AudioSource hit = hitAudio.GetComponent<AudioSource>();
            hit.Play();
            StartCoroutine(DeleteSound());

            target.GetComponent<EnemyAI>().TakeDamage(damage);

            timer += Time.deltaTime;
        }
        else if (target.GetComponent<EnemyAI>().IsDead() == false && timer == 0 && ranged == true)
        {
            model.SetTrigger("triggerHit");

            Invoke("FireProjectile", 0.60f);

            hitAudio = Instantiate(GameObject.Find("FireballSound"));
            AudioSource hit = hitAudio.GetComponent<AudioSource>();
            hit.Play();
            StartCoroutine(DeleteSound());

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

    void FireProjectile()
    {
        GameObject fireballObject = (GameObject)Instantiate(fireball, firePoint.position, firePoint.rotation);
        Fireball projectile = fireballObject.GetComponent<Fireball>();

        projectile.SetDamage(damage);

        if (projectile != null)
            projectile.Seek(target);
    }

    public IEnumerator DeleteSound()
    {
        if(ranged)
            yield return new WaitForSeconds(0.862f);
        else
            yield return new WaitForSeconds(1.33f);
        
        Destroy(hitAudio);
    }
}
