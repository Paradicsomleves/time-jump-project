using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class Interact : MonoBehaviour
{
    public bool interactable;

    TextPlacement textPlacement;
    StarterAssetsInputs _input;
    GameManager gameManager;
    public GameObject player;
    bool isPickUp = false;
        bool isInteract = false;

    private void Start()
    {
        _input = player.GetComponent<StarterAssetsInputs>();
        gameManager = FindObjectOfType<GameManager>();
        interactable = false;
    }

    private void Update()
    {
        if (_input.interact && interactable && isInteract)
        {
            _input.interact = false;
            Debug.Log("Interacted!");
        }
        if (_input.interact && interactable && isPickUp)
        {
            _input.interact = false;
            Debug.Log("Picked Up!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interact"))
        {
            textPlacement = other.GetComponent<TextPlacement>();
            textPlacement.MakeVisible(true);
            interactable = true;
            isInteract = true;

        } else if (other.CompareTag("PickUp"))
        {
            textPlacement = other.GetComponent<TextPlacement>();
            textPlacement.MakeVisible(true);
            interactable = true;
            isPickUp = true;

        } else
        {
            if (textPlacement != null) textPlacement.MakeVisible(false);
            interactable = false;
            isPickUp = false;
            isInteract = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickUp") || other.CompareTag("Interact"))
        {
            textPlacement.MakeVisible(false);
        }
            
        interactable = false;
        isPickUp = false;
        isInteract = false;
    }
}