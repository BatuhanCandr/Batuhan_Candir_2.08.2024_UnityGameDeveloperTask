using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [SerializeField] internal List<CollectableController> collectableList;
    
    [Header("Class Refferences")]
    [SerializeField] internal Player player;
    [SerializeField] internal UIManager uıManager;
    

    [Header("Game State")]
    [SerializeField] internal bool isStart;
    [SerializeField] internal bool isWin;
    [SerializeField] internal int collectCount;

    
    
    private void Awake()
    {
        Instance = this;
    }

    internal void Win()
    {
        if (collectCount == collectableList.Count)
        {
            isWin = true;
        }
    }
}
