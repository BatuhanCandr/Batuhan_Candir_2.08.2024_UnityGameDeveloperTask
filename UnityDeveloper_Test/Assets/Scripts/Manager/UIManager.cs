using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] internal TextMeshProUGUI countdownText;
    [SerializeField] internal GameObject startBtn;

    [Header("Game State Panel")] 
    [SerializeField] internal GameObject failPanel;
    [SerializeField] internal GameObject winPanel;

    [SerializeField] internal TextMeshProUGUI failInfo;
    
    
    
    


    public void StartButton()
    {
        StartCoroutine(GameManager.Instance.StartCountdown(GameManager.Instance.startSeconds));
        GameManager.Instance.isStart = true;
        startBtn.SetActive(false);
    }
       
    

   
}