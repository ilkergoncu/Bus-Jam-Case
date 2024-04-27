using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using DG.Tweening;

//<summary>
//This class handles the timer.
//<summary>

public class ManagerTimer : MonoBehaviour
{
    public float timeRemaining = 100; 
    public TMP_Text timerText; 
    public GameObject TimeOutPanel;
    bool isTimerBlinking;
    [SerializeField]
    RectTransform timerIntroducePanel;
    bool isTimerShown;
    

    private void Start()
    {
        isTimerShown = (PlayerPrefs.GetInt("TimerShown") != 0);
        if (!isTimerShown)
        {
            timerIntroducePanel.anchoredPosition = new Vector2(0, -timerIntroducePanel.rect.height);
            timerIntroducePanel.DOAnchorPosY(250, 0.5f).SetEase(Ease.OutQuad);
            isTimerShown = true;
            PlayerPrefs.SetInt("TimerShown", (isTimerShown ? 1 : 0));
        

        }
    }


    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();

            if(timeRemaining <= 10.0f && !isTimerBlinking)
            {
              
                StartCoroutine(BlinkText());
                isTimerBlinking = true;
            }
        }
        else
        {
            EndTime();
            StopCoroutine(BlinkText());
            timerText.color = Color.red; 
        }
    }

    void UpdateTimerDisplay()
    {
       
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void EndTime()
    {
        timerText.text = "00:00";
        timeRemaining = 0;
        TimeOutPanel.SetActive(true); 
        TimeOutPanel.transform.localScale = Vector3.zero; 
        TimeOutPanel.transform.DOScale(1, 1f).SetEase(Ease.OutBack); 

    }

    public void TurnOffTimerIntroduceManager()
    {
        timerIntroducePanel.gameObject.SetActive(false);
    }
    

    IEnumerator BlinkText()
    {
        while (true)
        {
            timerText.color = Color.red; 
            yield return new WaitForSeconds(0.5f); 
            timerText.color = Color.white; 
            yield return new WaitForSeconds(0.5f); 
        }
    }



}