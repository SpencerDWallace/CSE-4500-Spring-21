using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour
{
    [SerializeField] private GameObject flockUnitPrefab;
    [SerializeField] private GameObject enemy;
    public GameObject flocklingExplosion;
    [SerializeField] private int flockSize;
    [SerializeField] private Vector3 spawnBoundsMax;
    [SerializeField] private Vector3 spawnBoundsMin;

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
            float x = UnityEngine.Random.Range(spawnBoundsMin.x, spawnBoundsMax.x) * (((int)(UnityEngine.Random.Range(0f,1.99f))) * 2 - 1);
            float y = UnityEngine.Random.Range(spawnBoundsMin.y, spawnBoundsMax.y) * (((int)(UnityEngine.Random.Range(0f, 1.99f))) * 2 - 1);
            float z = UnityEngine.Random.Range(spawnBoundsMin.z, spawnBoundsMax.z) * (((int)(UnityEngine.Random.Range(0f, 1.99f))) * 2 - 1);
            var randomVector = new Vector3(x,y,z);
            var spawnPosition = transform.position + randomVector;
            var rotation = Quaternion.Euler(UnityEngine.Random.Range(-180F, 180F), UnityEngine.Random.Range(-180F, 180F), UnityEngine.Random.Range(-180F, 180F));
            allUnits[i] = Instantiate(flockUnitPrefab, spawnPosition, rotation);
            allUnits[i].transform.parent = gameObject.transform;
        }
    }

    public void attackPlayer()
    {
        //sometimes the flocklings crash or are destroyed, check that flockling being accessed wasn't already destroyed
        while (flockSize > 0 && allUnits[flockSize - 1] == null)
            flockSize--;
        if (flockSize > 0)
        {

            var newEnemy = allUnits[flockSize - 1].transform;
            Instantiate(enemy, newEnemy.position, newEnemy.rotation);
            allUnits[flockSize - 1] = null;
            flockSize--;
        }
    }
}
