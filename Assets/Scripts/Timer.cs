using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToAnswerQuestion = 30f;
    [SerializeField] float timeToShowAnswer = 10f;
    [SerializeField] Quiz quiz;

    public float fillerFraction;
    float timerValue;
    public bool isTimeToAnswerQuestion;
    public bool loadNextQuestion;
    void Update()
    {
        UpdateTime();
    }

    public void CancelTimer()
    {
        
        timerValue = 0;
    }
    void UpdateTime()
    {
        timerValue -= Time.deltaTime;
        if (isTimeToAnswerQuestion)
        {
            if (timerValue > 0)
            {
                fillerFraction = timerValue / timeToAnswerQuestion;
            }
            else
            {
                isTimeToAnswerQuestion = false;
                timerValue = timeToShowAnswer;
            }
        }
        else
        {
            if (timerValue > 0)
            {
                fillerFraction = timerValue / timeToShowAnswer;
            }
            else
            {
                isTimeToAnswerQuestion = true;
                timerValue = timeToAnswerQuestion;
                loadNextQuestion = true;

            }
        }
    }

    public void check()
    {
        Debug.Log("ada di timer");
    }
}
