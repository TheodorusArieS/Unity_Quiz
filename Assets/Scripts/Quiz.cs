using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    QuestionSO currentQuestion;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnswerQuestionEarly = true;

    [Header("Button Colors")]
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite defaultAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    [Header("Complete")]
    public bool isComplete;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;

    }

    void Update()
    {
        timerImage.fillAmount = timer.fillerFraction;
        if (timer.loadNextQuestion)
        {

            hasAnswerQuestionEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnswerQuestionEarly && !timer.isTimeToAnswerQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    void DisplayAnswer(int index)
    {
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            Image buttonSprite = answerButtons[index].GetComponent<Image>();
            buttonSprite.sprite = correctAnswerSprite;
            questionText.text = "CORRECT!";
            scoreKeeper.IncrementCorrectAnswer();

        }
        else
        {

            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            Image buttonSprite = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonSprite.sprite = correctAnswerSprite;
            questionText.text = "Sorry the correct answer was: \n" + correctAnswer;
        }
    }

    public void OnClickAnswer(int index)
    {
        hasAnswerQuestionEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score:" + scoreKeeper.CalculateScore() + "%";


    }

    void GetNextQuestion()
    {

        if (progressBar.value == progressBar.maxValue) isComplete = true;
        if (questions.Count < 1) return;
        progressBar.value++;
        SetButtonState(true);
        GetRandomQuestion();
        DisplayQuestion();
        SetDefaultButtonSprite();
        scoreKeeper.IncrementSeenQuestions();
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion)) questions.Remove(currentQuestion);
    }
    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI button = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            button.text = currentQuestion.GetAnswer(i);
        }

    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprite()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image button = answerButtons[i].GetComponent<Image>();
            button.sprite = defaultAnswerSprite;
        }
    }
}
