using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    DestroyedChange destoryedChange;

    public bool isPast;

    //public Quest quest;
    //public QuestGiver questGiver;
    //public bool questIsActive;

    void Start()
    {
        destoryedChange = FindObjectOfType<DestroyedChange>();
    }

    private void Update()
    {
        isPast = destoryedChange.IsPast;
    }

}
