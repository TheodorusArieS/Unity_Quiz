using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class EndScreen : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    [SerializeField] TextMeshProUGUI scoreText;

    void Awake() {
         scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    

    public void ShowFinalScore()
    {
        scoreText.text = "Congratulations! \n" + "Your Score is:" + scoreKeeper.CalculateScore() +"%";
    }
}
