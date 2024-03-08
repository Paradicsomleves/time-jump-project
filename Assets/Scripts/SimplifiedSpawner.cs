using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class SimplifiedSpawner : MonoBehaviour
{
    public GameObject[] item = new GameObject[1];
    public int numberOfItems;
    public float rangeOfSpawn;

    public float maxScale = 5;
    public float minScale = 2.5f;
    public float centerMaxSize = 15;
    float distanceFromCenter;

    Vector3 RandomSpawnGenerator(float _rangeOfSpawn)
    {
        Vector3 randomPlace = transform.position + new Vector3(UnityEngine.Random.Range(-_rangeOfSpawn,_rangeOfSpawn), 0, UnityEngine.Random.Range(-_rangeOfSpawn,_rangeOfSpawn));
        distanceFromCenter = (transform.position - randomPlace).magnitude / rangeOfSpawn;
        distanceFromCenter = Mathf.Lerp(centerMaxSize,minScale,distanceFromCenter);
        distanceFromCenter = Mathf.Clamp(distanceFromCenter, 1, centerMaxSize);
        return randomPlace;
    }
    Vector3 SizeGenerator(float _minScale,  float _maxScale, float _distanceFromCenter)
    {
        float randomSize;

        randomSize = UnityEngine.Random.Range(_minScale, _maxScale * _distanceFromCenter);

        Vector3 randomScale = new Vector3(UnityEngine.Random.Range(3,5),randomSize, UnityEngine.Random.Range(3, 5));
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
            GameObject newObject = Instantiate(item[UnityEngine.Random.Range(0,item.Length)], RandomSpawnGenerator(rangeOfSpawn), transform.rotation, transform) as GameObject;
            newObject.transform.localScale = new Vector3(1,1,1);
        }
    }

}
