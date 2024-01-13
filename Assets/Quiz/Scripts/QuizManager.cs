using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private Server server;
    [SerializeField] private QuizUi quizUi;
    [SerializeField] private List<QuizDataScriptable> quizData;
    [SerializeField] private float timeLimit = 30f;
    
    private List<Question> questions;
    private Question selectedQuestion;
    public static int scoreCount = 0;
    private float currentTime;
    private int lifeRemaining = 3;

    private GameStatus gameStatus = GameStatus.Next;

    public GameStatus GameStatus  { get { return gameStatus; } }

    private Server serverInstance;


    //เริ่ม quiz
    public void StartGame(int index)
    {
        scoreCount = 0;
        currentTime = timeLimit;
        lifeRemaining = 3;
        questions = new List<Question>();
        for (int i = 0; i < quizData[index].questions.Count; i++)
        {
            questions.Add(quizData[index].questions[i]);
        }

        

        selectQuestion();
        gameStatus = GameStatus.Playing;
        

      
    }

    //เลือกคำถาม
    void selectQuestion()
    {
        int val = UnityEngine.Random.Range(0,questions.Count);
        selectedQuestion = questions[val];

        quizUi.SetQuestion(selectedQuestion);

        questions.RemoveAt(val);

    }

    //set เวลาให้ลด
    private void Update(){

        //print(Server.namesign);

        if(gameStatus == GameStatus.Playing){
            currentTime -= Time.deltaTime;
            SetTimer(currentTime);
        }

        //quizUi.NameU.text = Server.namesign.text;
    }


    //เวลาหมดแพ้
    private void SetTimer(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        quizUi.TimerText.text = "Time:" + time.ToString("mm':'ss");

        if(currentTime <=0 ){
            
            quizUi.GameOverPanel.SetActive(true);
            gameStatus = GameStatus.Next;
            
        }
    }
    
    //เช็คคำถามคำตอบ
    public bool Answer(string answerd)
    {
        bool correctAns = false;

        if(answerd == selectedQuestion.correctAns){
            //YEs
            correctAns = true;
            scoreCount += 50; //addscore
            quizUi.ScoreText.text = "Score:" + scoreCount;
            quizUi.UserScore.text = "" + scoreCount;
          
            
        }
        else{
            lifeRemaining--;
            quizUi.ReduceLife(lifeRemaining);

            if(lifeRemaining <= 0)
            {
                quizUi.GameOverPanel.SetActive(true);
                gameStatus = GameStatus.Next;
                
            }
            //wrong
        }

        if(gameStatus == GameStatus.Playing)
        {
            if(questions.Count > 0)
            {
                Invoke("selectQuestion",0.4f);//time
            }
            else{
                quizUi.GameOverPanel.SetActive(true);
                gameStatus = GameStatus.Next;
                
            }
            
        }
        
        return correctAns;
    }
}

[System.Serializable]

public class Question{
    public string questionInfo;
    public QuestionType questionType;
    public Sprite questionImg;
    public AudioClip questionClip;
    public UnityEngine.Video.VideoClip questionVideo;
    public List<string> options;
    public string correctAns;
}

[System.Serializable]

public enum QuestionType{
    Text,
    IMAGE,
    VIDEO,
    AUDIO

}

[System.Serializable]
public enum GameStatus{
    Next,
    Playing
}