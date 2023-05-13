using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float damage;

    private Transform target;
    private float speed = 20f;

    public void Seek(Transform enemy)
    {
        target = enemy;
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    // Update is called once per frame
    void Update()
    {
        // Destroys fireball if enemy disappears or dies
        if(target == null)
        {
            Destroy(gameObject);
        }
        // Otherwise run standard movement and damage calculations
        else
        {
            Vector3 direction = target.position - transform.position;
            float distance = speed * Time.deltaTime;

            if(direction.magnitude <= distance)
            {
                Destroy(gameObject);
                target.GetComponent<EnemyAI>().TakeDamage(damage);
            }

            transform.Translate(direction.normalized * distance, Space.World);
        }
    }
}
