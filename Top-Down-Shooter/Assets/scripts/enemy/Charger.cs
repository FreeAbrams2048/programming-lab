using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    private Transform target;

    public float HitPoints = 10;
    public float MaxHitPoints = 10;
    public HealthBarBehaviour HealthBar;
    
    void Start()
    {
        target = GameObject.Find("Player").transform;
        HealthBar.SetHealth(HitPoints, MaxHitPoints);
    }

    void Update()
    {
        transform.up = target.position - transform.position;
    }

    public void TakeDamage(float damage)
    {
        HitPoints -= damage;
        HealthBar.SetHealth(HitPoints, MaxHitPoints);

        if (HitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
