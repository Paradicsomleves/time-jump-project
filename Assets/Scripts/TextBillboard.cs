using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TextBillboard : MonoBehaviour
{
    Transform mainCamera;

    void Awake()
    {
        mainCamera = Camera.main.transform;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCamera);
        transform.Rotate(Vector3.up, 180);
    }
}
