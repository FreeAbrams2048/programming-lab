using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class playerMoving : MonoBehaviour
{
    public float playerHPMax = 20;
    public float playerHPCurrent = 20;

    Transform trans;
    bool cooldown = false;
    [SerializeField] Rigidbody2D rig;
    [SerializeField] private FeildOfView fieldOfView;
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
        //LookAtMouse();
        Vector3 mousePosition = GetMousePosition();//this bit is for fov

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        firePoint.eulerAngles = new Vector3(0, 0, angle);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //this bit is to turn the character
        transform.up = mousePos - new Vector2(transform.position.x, transform.position.y);

        fieldOfView.SetAimDirection(aimDirection);
        fieldOfView.SetOrigin(transform.position);
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

    }

    IEnumerator waiting()
    {
        cooldown = true;
        yield return new WaitForSeconds(0.4f);
        cooldown = false;
    }

    static Vector3 GetMousePosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0;
        return vec;
    }
    static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}