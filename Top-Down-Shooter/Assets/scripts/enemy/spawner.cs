using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] private GameObject charger;
    [SerializeField] private Transform spawnPoint;
    void Start()
    {
        Instantiate(charger);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
