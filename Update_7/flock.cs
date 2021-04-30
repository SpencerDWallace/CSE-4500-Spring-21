using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour
{
    [SerializeField] private GameObject flockUnitPrefab;
    [SerializeField] private int flockSize;
    [SerializeField] private Vector3 spawnBounds;

    public GameObject[] allUnits { get; set; }

    private void Start()
    {
        GenerateUnits();
    }

    private void GenerateUnits()
    {
        allUnits = new GameObject[flockSize];
        for (int i = 0; i < flockSize; i++)
        {
            var randomVector = UnityEngine.Random.insideUnitSphere;
            randomVector = new Vector3(randomVector.x * spawnBounds.x, randomVector.y * spawnBounds.y, randomVector.z * spawnBounds.z);
            var spawnPosition = transform.position + randomVector;
            var rotation = Quaternion.Euler(UnityEngine.Random.Range(-180F, 180F), UnityEngine.Random.Range(-180F, 180F), UnityEngine.Random.Range(-180F, 180F));
            allUnits[i] = Instantiate(flockUnitPrefab, spawnPosition, rotation);
        }
    }
}
