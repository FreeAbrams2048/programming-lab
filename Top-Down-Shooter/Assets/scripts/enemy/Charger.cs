using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    private Transform target;

    public float HitPoints = 10;
    public float MaxHitPoints = 10;
    public HealthBarBehaviour HealthBar;

    float minDist = 20;
    float cDist;
    bool isCharging;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        HealthBar.SetHealth(HitPoints, MaxHitPoints);
    }

    void Update()
    {

        cDist = Vector3.Distance(target.position, transform.position);

        if (isCharging == false)
        {
            transform.up = target.position - transform.position;
            transform.position += transform.up * Time.deltaTime * 4;
        }

        if (cDist < minDist)
        {
            StartCoroutine(ChargeHandler());
        }
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

    IEnumerator ChargeHandler()         //handles timings for the enemy to dash, and has the enemy wait after dashing to act as a cooldown
    {
        isCharging = true;
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreLayerCollision(3, 6, true);
        transform.position += transform.up * Time.deltaTime * 20;
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreLayerCollision(3, 6, false);
        yield return new WaitForSeconds(1f);
        isCharging = false;
        yield break;
    }
}
