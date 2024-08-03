using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] internal TextMeshProUGUI countdownText;
    [SerializeField] internal GameObject startBtn;
    [SerializeField] internal int startSeconds; 


    public void StartButton()
    {
        StartCoroutine(StartCountdown(startSeconds));
        GameManager.Instance.isStart = true;
        startBtn.SetActive(false);
    }
       
    

    private IEnumerator StartCountdown(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
          
            int minutes = counter / 60;
            int secondsRemaining = counter % 60;
            countdownText.text = string.Format("{0:D2}:{1:D2}", minutes, secondsRemaining);

            yield return new WaitForSeconds(1);
            counter--;
        }

    
        countdownText.text = "00:00";
        OnCountdownFinished();
    }

    private void OnCountdownFinished()
    {
       
        Debug.Log("Countdown finished!");
    }
}