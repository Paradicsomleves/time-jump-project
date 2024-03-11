using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    DestroyedChange destoryedChange;
    public static GameManager instance;

    public bool isPast;

    public delegate void TimeJump();
    public static event TimeJump Jumping;

    //public Quest quest;
    //public QuestGiver questGiver;
    //public bool questIsActive;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        destoryedChange = FindObjectOfType<DestroyedChange>();
        isPast = false;
    }

    private void Update()
    {
        isPast = !destoryedChange.IsPast;

        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            if (Jumping != null)
            {
                Jumping();
            }
        }
    }

}
