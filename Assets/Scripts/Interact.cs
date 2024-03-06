using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public bool interactable;

    private static GameManager instance;

    private void Start()
    {
        instance = FindObjectOfType<GameManager>();
        Interactable(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interact"))
        {
            Interactable(true);
            instance.interactText.transform.position = other.transform.position + instance.textOffset;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable(false);
    }

    private void Interactable(bool isInteractable)
    {
        interactable = isInteractable;
        instance.interactableNearby = isInteractable;
    }
}