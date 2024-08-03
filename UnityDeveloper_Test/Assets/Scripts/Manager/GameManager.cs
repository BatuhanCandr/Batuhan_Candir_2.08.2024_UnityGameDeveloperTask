using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    
    
    [Header("Class Refferences")]
    [SerializeField] internal Player player;
    
    private void Awake()
    {
        Instance = this;
    }
}
