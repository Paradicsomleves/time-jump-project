using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public GameObject[] item = new GameObject[1];
    public int numberOfItems;
    public float rangeOfSpawn;

    public float maxScale = 5;
    public float minScale = 2.5f;
    public float centerMaxMultiplier = 15;
    float distanceFromCenter;
    float sizeCenter;

    [Range(0f, 1f)]
    public float highriseRadian;

    Vector3 RandomSpawnGenerator(float _rangeOfSpawn)
    {
        Vector3 randomPlace = transform.position + new Vector3(UnityEngine.Random.Range(-_rangeOfSpawn, _rangeOfSpawn), 0, UnityEngine.Random.Range(-_rangeOfSpawn, _rangeOfSpawn));
        distanceFromCenter = (transform.position - randomPlace).magnitude / rangeOfSpawn;

        sizeCenter = Mathf.Lerp(centerMaxMultiplier, minScale, distanceFromCenter);
        sizeCenter = Mathf.Clamp(sizeCenter, 1, centerMaxMultiplier);
        return randomPlace;
    }
    Vector3 SizeGenerator(float _minScale, float _maxScale, float _sizeCenter)
    {
        float randomSize;

        if (distanceFromCenter > highriseRadian)
        {
            randomSize = UnityEngine.Random.Range(_minScale, _maxScale);
        }
        else
        {
            randomSize = UnityEngine.Random.Range(_minScale, _maxScale) * _sizeCenter;
        }
        Vector3 randomScale = new Vector3(UnityEngine.Random.Range(6, 9), randomSize, UnityEngine.Random.Range(6, 9));
        return randomScale;
    }

    quaternion RandomRotation(int _random03)
    {
        return quaternion.AxisAngle(Vector3.up, _random03 * 90f); ;
    }

    public void SpawnItems()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            DestroyImmediate(this.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < numberOfItems; i++)
        {
            RandomSpawnGenerator(rangeOfSpawn);
            GameObject newObject = Instantiate(item[UnityEngine.Random.Range(0, item.Length)], RandomSpawnGenerator(rangeOfSpawn), RandomRotation(UnityEngine.Random.Range(0, 4)), transform) as GameObject;
            newObject.transform.localScale = SizeGenerator(minScale, maxScale, sizeCenter);
        }
    }

}
