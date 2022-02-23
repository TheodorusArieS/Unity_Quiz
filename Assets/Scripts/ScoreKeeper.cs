using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int seenQuestions = 0;

    public int GetCorrectAnswer()
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswer()
    {
        correctAnswers++;
    }

    public int GetSeenQuestions()
    {
        return seenQuestions;
    }

    public void IncrementSeenQuestions()
    {
        seenQuestions++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)seenQuestions * 100);
    }
}
