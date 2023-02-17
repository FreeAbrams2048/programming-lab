using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoving : MonoBehaviour
{
    public float playerHPMax = 20;
    public float playerHPCurrent = 20;

    Transform trans;
    bool cooldown = false;
    [SerializeField] Rigidbody2D rig;

    [SerializeField] private float speed;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject _trail;
    [SerializeField] private float weaponRange = 10f;
    //[SerializeField] private Animator muzzleFlashAnimator;

    void Start()
    {
        trans = GetComponent<Transform>();
    }

    void Update()
    {
        walk();
        LookAtMouse();
        if (Input.GetMouseButtonDown(0) && cooldown == false)
        {
            shoot();
            //StartCoroutine(waiting());
        }
    }

    void shoot()
    {
        //muzzleFlashAnimator.SetTrigger("Shoot");

        var hit = Physics2D.Raycast(firePoint.position, transform.up, weaponRange);
        var trail = Instantiate(_trail, firePoint.position, transform.rotation);
        var trailScript = trail.GetComponent<BulletTrail>();

        if (hit.collider != null)
        {
            trailScript.SetTargetPosition(hit.point);

            var charger = hit.collider.GetComponent<Charger>();
            if (charger != null)
            {
                charger.TakeDamage(5);
                Debug.Log("Hit");
            }
        }

        else
        {
            var endPosition = firePoint.position + transform.up * weaponRange;
            trailScript.SetTargetPosition(endPosition);
        }
    }

    void walk()
    {
        // if (Input.GetKey(KeyCode.W))
        // {
        //     trans.position += transform.up * Time.deltaTime * speed;
        //  }
        //  if (Input.GetKey(KeyCode.S))
        // {
        //      trans.position += transform.up * Time.deltaTime * -speed;
        //  }
        // if (Input.GetKey(KeyCode.D))
        // {
        //     trans.position += transform.right * Time.deltaTime * speed;
        // }
        // if (Input.GetKey(KeyCode.A))
        // {
        //    trans.position += transform.right * Time.deltaTime * -speed;
        //}

        rig.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
    }

    void LookAtMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = mousePos - new Vector2(transform.position.x, transform.position.y);
    }

    IEnumerator waiting()
    {
        cooldown = true;
        yield return new WaitForSeconds(0.4f);
        cooldown = false;
    }
}