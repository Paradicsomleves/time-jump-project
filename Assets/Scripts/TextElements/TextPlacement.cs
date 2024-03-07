using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPlacement : MonoBehaviour
{
    public GameObject text;
    public Vector3 textOffset;

    private void Start()
    {
        text.SetActive(false);
    }

    public void MakeVisible(bool isVisible)
    {
        if (isVisible)
        {
            text.transform.position = transform.position + textOffset;
            text.SetActive(true);

        }
        else
        {
            text.SetActive(false);
        }
    }
}
