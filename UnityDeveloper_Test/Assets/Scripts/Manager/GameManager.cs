using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [SerializeField] internal List<CollectableController> collectableList;
    
    [Header("Class Refferences")]
    [SerializeField] internal Player player;
    [SerializeField] internal UIManager uiManager;
    

    [Header("Game State")]
    [SerializeField] internal bool isStart;
    [SerializeField] internal bool isWin;
    [SerializeField] internal bool isFail;
    
    [Space]
    [SerializeField] internal int collectCount;
    [SerializeField] internal int startSeconds; 

    
    
    private void Awake()
    {
        Instance = this;
    }

    internal void Win()
    {
        if (collectCount == collectableList.Count)
        {
            isWin = true;
            uiManager.winPanel.SetActive(true);
        }
    }

    internal void Fail()
    {
        isFail = true;
        uiManager.failPanel.SetActive(true);
        
    }
    
    internal IEnumerator StartCountdown(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
          
            int minutes = counter / 60;
            int secondsRemaining = counter % 60;
            uiManager.countdownText.text = string.Format("{0:D2}:{1:D2}", minutes, secondsRemaining);

            yield return new WaitForSeconds(1);
            counter--;
        }

    
        uiManager.countdownText.text = "00:00";
        OnCountdownFinished();
    }

    private void OnCountdownFinished()
    {
        uiManager.failInfo.text = "You couldn't collect it all in time";
        Fail();
    }

    internal void RestartScene()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        
    }
}
