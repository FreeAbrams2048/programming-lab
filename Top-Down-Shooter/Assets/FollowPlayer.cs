using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Transform target;
    Vector3 differance;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        differance = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + differance;
    }
}
