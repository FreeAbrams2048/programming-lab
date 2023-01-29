using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float progress;

    [SerializeField] private float speed = 40f;

    void Start()
    {
        startPosition = transform.position.WithAxis(Axis.Z, -1);
    }


    void Update()
    {
        progress += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPosition, endPosition, progress);
    }

    public void SetTargetPosition(Vector3 targetposition)
    {
       endPosition = targetposition.WithAxis(Axis.Z, -1);
    }
}
