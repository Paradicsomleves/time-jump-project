using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public StarterAssetsInputs _input;
    DestoryedChange destoryedChange;

    public bool isPast;

    //public Quest quest;
    //public QuestGiver questGiver;
    //public bool questIsActive;

    void Start()
    {
        _input = player.GetComponent<StarterAssetsInputs>();
        destoryedChange = FindObjectOfType<DestoryedChange>();
    }

    private void Update()
    {
        isPast = destoryedChange.IsPast;
    }

}
