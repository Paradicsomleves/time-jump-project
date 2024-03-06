using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public static GameManager instance;

    private void Start()
    {
        instance = FindObjectOfType<GameManager>();
    }
}
