using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    StarterAssetsInputs _input;

    public bool interactableNearby;
    public GameObject interactText;
    public Vector3 textOffset;


    public Quest quest = new Quest();

    // Start is called before the first frame update
    void Start()
    {
        instance = GetComponent<GameManager>();
        _input = player.GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactableNearby && interactText != null)
        {
            interactText.SetActive(true);
        } else 
        {
            interactText.SetActive(false);
        }

        if (_input.interact && interactableNearby)
        {
            _input.interact = false;
            Debug.Log("Interacted!");
        }
    }
}
