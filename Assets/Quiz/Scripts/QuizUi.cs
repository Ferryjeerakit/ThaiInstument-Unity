using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class QuizUi : MonoBehaviour
{
    //เก็บตัวแปรต่างๆ
    [SerializeField]private Server server;
    [SerializeField]private QuizManager quizManager;
    [SerializeField]private Text questionText,scoreText,timerText,userScore;
    [SerializeField]private List<Image> lifeImageList;
    [SerializeField]private GameObject gameOverPanel,mainMenuPanel,gameMenuPanel,infoPanel;
    [SerializeField]private Image questionImage;
    [SerializeField]private UnityEngine.Video.VideoPlayer questionVideo;
    [SerializeField]private AudioSource questionAudio;
    [SerializeField]private List<Button> options,uiButtons;
    [SerializeField]private Color correctCol,wrongCol,normalCol;

    [Space]

    [SerializeField]private GameObject fI,sI,tI,ChingI,HanuI,PosIn,PraI,Ra2I,RaI,TaI;

    private Question question;
    private bool answerd;
    private float audioLength;

    

    

  //ตัวแปรเก็บคะแนน
    public Text ScoreText{get{return scoreText;}}
    public Text UserScore{get{return userScore;}}
    public Text TimerText{get{return timerText;}}
    public GameObject GameOverPanel {get{return gameOverPanel;}}


 

    // ปุ่ม start
    void Awake()
    {
      


       
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }

         for (int i = 0; i < uiButtons.Count; i++)
        {
            Button localBtn = uiButtons[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
        
        
    }

    //ui พวก question สร้างตัวแปรเก็บคำถามเป็น type ต่างๆ
    public void SetQuestion(Question question)
    {
        this.question = question;

        switch(question.questionType)
        {
            case QuestionType.Text: 
                questionImage.transform.parent.gameObject.SetActive(false);

                break;
            case QuestionType.IMAGE:
                ImageHolder();
                questionImage.transform.gameObject.SetActive(true);

                questionImage.sprite = question.questionImg;

                break;
            case QuestionType.VIDEO:
                ImageHolder();
                questionVideo.transform.gameObject.SetActive(true);

                questionVideo.clip = question.questionVideo;
                questionVideo.Play();

                break;
            case QuestionType.AUDIO:
                ImageHolder();
                questionAudio.transform.gameObject.SetActive(true);
                audioLength = question.questionClip.length;
                StartCoroutine(PlayAudio());

                break;
            
        }

        
        questionText.text = question.questionInfo;
        
        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);

        for (int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<Text>().text = answerList[i];
            options[i].name = answerList[i];
            options[i].image.color = normalCol;
        
        }

        answerd = false;

    }

    //Audio
    IEnumerator PlayAudio()
    {
        if(question.questionType == QuestionType.AUDIO)
        {
            questionAudio.PlayOneShot(question.questionClip);

            yield return new WaitForSeconds(audioLength + 0.5f);

            StartCoroutine(PlayAudio());
        }
        else{
            StopCoroutine(PlayAudio());
            yield return null;
        }
    }

    //ui image
    void ImageHolder()
    {
        questionImage.transform.parent.gameObject.SetActive(true);
        questionImage.transform.gameObject.SetActive(false);
        questionAudio.transform.gameObject.SetActive(false);
        questionVideo.transform.gameObject.SetActive(false);
    }


    //ปุ่มถูกผิดเวลากด
    private void OnClick(Button btn)
    {

        if(quizManager.GameStatus == GameStatus.Playing)
        { 
            if(!answerd)
            {
                answerd = true;
                bool val = quizManager.Answer(btn.name);

                if(val)
                {
                    btn.image.color = correctCol;
                }
                else{
                    btn.image.color = wrongCol;
                }

            }
        }


        //ปุ่มแต่ละปุ่ม
        switch(btn.name)
        {
            case "StartBtn":
                quizManager.StartGame(0);
                mainMenuPanel.SetActive(false);
                gameMenuPanel.SetActive(true);
                
                break;

            case "InfoBtn":
                mainMenuPanel.SetActive(false);
                gameMenuPanel.SetActive(false);
                infoPanel.SetActive(true);
                
                break;
           
        }
       
    }

    //ปุ่มออก
    public void ExitButton(){
        print("exit");
        Application.Quit();
        // UnityEditor.EditorApplication.isPlaying = false;
    }
    
    //ปุ่ม retry
    public void RetryButton()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    //แสดงหน้า info หลัก
    public void onel()
    { 
        fI.SetActive(false);
        tI.SetActive(true);
    }
     public void oner()
    { 
        fI.SetActive(false);
        sI.SetActive(true);
    }
    public void twol()
    { 
        sI.SetActive(false);
        fI.SetActive(true);
    }
    public void twor()
    { 
        sI.SetActive(false);
        tI.SetActive(true);
    }
    public void threel()
    { 
        tI.SetActive(false);
        sI.SetActive(true);
    }
    public void threer()
    { 
        tI.SetActive(false);
        fI.SetActive(true);
    }


     public void Infomation()
    { 
        fI.SetActive(true);
    }


    public void ching()
    { 
        ChingI.SetActive(true);
    }

    public void hanuman()
    { 
        HanuI.SetActive(true);
    }

    public void Poster()
    { 
        PosIn.SetActive(true);
    }

    public void Pra()
    { 
        PraI.SetActive(true);
    }

    public void Ranard()
    { 
        RaI.SetActive(true);
    }

    public void RanardT()
    { 
        Ra2I.SetActive(true);
    }

    public void Tapon()
    { 
        TaI.SetActive(true);
    }


    //ปุ่มปิด
    public void closeBt()
    {
        ChingI.SetActive(false);
        HanuI.SetActive(false);
        PosIn.SetActive(false);
        PraI.SetActive(false);
        RaI.SetActive(false);
        Ra2I.SetActive(false);
        TaI.SetActive(false);
        fI.SetActive(false);
    }






    //รีเซ็ต
    public void Reset(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        //SceneManager.LoadSceneAsync("Scene", LoadSceneMode.Additive);
    }
    

    //ลดเลือด
    public void ReduceLife(int index)
    {
        lifeImageList[index].color = wrongCol;
    }

    
}
