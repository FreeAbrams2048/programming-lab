using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] private GameObject charger;
    [SerializeField] private Transform spawnPoint;
    void Start()
    {
        Instantiate(charger, transform.localPosition,transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
