using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject item;
    public int numberOfItems = 20;
    public float rangeOfSpawn = 20f;

    Vector3 RandomSpawnGenerator(float spawnRange)
    {
        Vector3 randomPlace = transform.position + new Vector3(Random.Range(-spawnRange,spawnRange), 0, Random.Range(-spawnRange,spawnRange));
        return randomPlace;
    }
    public void SpawnItems()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            DestroyImmediate(this.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < numberOfItems; i++)
        {
            Instantiate(item, RandomSpawnGenerator(rangeOfSpawn), transform.rotation, transform);
        }
    }

}
