using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class PictureToWord : MonoBehaviour
{

    #region unity reference variables

    [Header("AUDIO---------------------------------------------------------")]
    [SerializeField] private AudioClip[] ACA_Words;
    [SerializeField] private AudioClip AC_Correct;
    [SerializeField] private AudioClip AC_Wrong;
    [SerializeField] private AudioClip AC_BubblyButtonClick;
    [SerializeField] private AudioClip AC_Proceed;

    [Space(10)]

    [Header("IMAGE---------------------------------------------------------")]
    [SerializeField] private Image[] IMGA_Lines;

    [Space(10)]

    [Header("ANIMATOR---------------------------------------------------------")]
    [SerializeField] private Animator ANIM_Counter;

    [Space(10)]

    [Header("TEXTMESHPRO---------------------------------------------------------")]
    [SerializeField] private TextMeshProUGUI TXT_Counter;

    [Space(10)]

    [Header("INPUTFIELD---------------------------------------------------------")]
    [SerializeField] private TMP_InputField[] IF_Q1;

    [Space(10)]

    [Header("GAME OBJECT---------------------------------------------------------")]
    [SerializeField] private GameObject[] GA_Images;
    [SerializeField] private GameObject[] GA_Words;
    [SerializeField] private GameObject[] GA_ImageDots;
    [SerializeField] private GameObject[] GA_WordDots;
    [SerializeField] private GameObject G_ImgTransparentScreen;
    [SerializeField] private GameObject G_WordTransparentScreen;
    [SerializeField] private GameObject G_TransparentScreen;


    [SerializeField] private GameObject G_Part1;
    [SerializeField] private GameObject G_Part2;
    [SerializeField] private GameObject G_Part3;
    [SerializeField] private GameObject G_ProceedButton1;
    [SerializeField] private GameObject G_ProceedButton2;
    [SerializeField] private GameObject G_CheckButton;
    [SerializeField] private GameObject[] GA_Questions;


    [Space(10)]


    [Header("TRANSFORM---------------------------------------------------------")]
    [SerializeField] private Transform T_QParent;


    //!end of region - unity reference variables
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion



    #region local variables
    private float _elapsedTime, _desiredDuration = 0.5f;
    private int I_ImageIndex;
    private int I_WordIndex;



    private int I_CurrentIndex;
    private int I_AnswerCount;
    private bool IF_1Answered, IF_2Answered;
    private GameObject instantiatedGameObject;



    //!end of region - local variables
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion


    #region QA

    private int qIndex;
    public GameObject questionGO;
    public GameObject[] optionsGO;
    public Dictionary<string, Component> additionalFields;
    Component question;
    Component[] options;
    Component[] answers;

    #endregion



    void Start()
    {
        I_ImageIndex = -1;
        I_WordIndex = -1;


        I_CurrentIndex = -1;
        I_AnswerCount = 0;
        IF_1Answered = false;
        IF_1Answered = false;

        #region DataSetter
        //Main_Blended.OBJ_main_blended.levelno = 3;
        QAManager.instance.UpdateActivityQuestion();
        qIndex = 0;
        // GetData(qIndex);
        GetAdditionalData();
        AssignData();
        #endregion

    }



    #region public functions


    public void BUT_Image(int index)
    {
        AudioManager.Instance.PlaySFX(AC_BubblyButtonClick);
        G_ImgTransparentScreen.SetActive(true);
        GA_Images[index].GetComponent<Animator>().SetTrigger("clicked");
        I_ImageIndex = index;
        G_WordTransparentScreen.SetActive(false);

        if (I_WordIndex != -1)
        {
            StartCoroutine(IENUM_MatchAnswerCheck());
        }
    }


    public void BUT_Word(int index)
    {
        G_WordTransparentScreen.SetActive(true);
        GA_Words[index].GetComponent<Animator>().SetTrigger("clicked");
        I_WordIndex = index;

        if (I_ImageIndex != -1)
        {
            StartCoroutine(IENUM_MatchAnswerCheck());
        }
    }


    public void BUT_Proceed1()
    {
        I_AnswerCount = 0;

        G_Part1.SetActive(false);
        G_Part2.SetActive(true);
        Invoke(nameof(BUT_Next), 0.5f);
    }


    public void BUT_Proceed2()
    {
        I_AnswerCount = 0;

        G_Part2.SetActive(false);
        G_Part3.SetActive(true);
    }


    //part 2 answer check
    public void BUT_Check()
    {
        switch (I_CurrentIndex)
        {
            case 0:
                if (IF_Q1[0].text.ToLower().Equals("s") && IF_Q1[1].text.ToLower().Equals("n"))
                {
                    AudioManager.Instance.PlayVoice(ACA_Words[0]);
                    PlayAnim("matched");
                }
                else
                {
                    AudioManager.Instance.PlayVoice(AC_Wrong);
                    PlayAnim("mismatched");
                }
                break;

            case 1:
                if (IF_Q1[0].text.ToLower().Equals("r") && IF_Q1[1].text.ToLower().Equals("e"))
                {
                    AudioManager.Instance.PlayVoice(ACA_Words[1]);
                    PlayAnim("matched");
                }
                else
                {
                    PlayAnim("mismatched");
                }
                break;

            case 2:
                if (IF_Q1[0].text.ToLower().Equals("b") && IF_Q1[1].text.ToLower().Equals("o"))
                {
                    AudioManager.Instance.PlayVoice(ACA_Words[2]);
                    PlayAnim("matched");
                }
                else
                {
                    PlayAnim("mismatched");
                }
                break;

            case 3:
                if (IF_Q1[0].text.ToLower().Equals("i") && IF_Q1[1].text.ToLower().Equals("r"))
                {
                    AudioManager.Instance.PlayVoice(ACA_Words[3]);
                    PlayAnim("matched");
                }
                else
                {
                    PlayAnim("mismatched");
                }
                break;

            case 4:
                if (IF_Q1[0].text.ToLower().Equals("i") && IF_Q1[1].text.ToLower().Equals("g"))
                {
                    AudioManager.Instance.PlayVoice(ACA_Words[4]);
                    PlayAnim("matched");
                }
                else
                {
                    PlayAnim("mismatched");
                }
                break;

            default:
                break;
        }


        void PlayAnim(string param)
        {
            instantiatedGameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger(param);
        }

    }


    public void BUT_Next()
    {
        foreach (Transform child in T_QParent) Destroy(child.gameObject);

        I_CurrentIndex++;

        if (I_CurrentIndex == GA_Questions.Length)
        {
            AudioManager.Instance.PlaySFX(AC_Proceed);
            return;
        }

        ShowQuestion();
    }


    public void BUT_Back()
    {
        foreach (Transform child in T_QParent) Destroy(child.gameObject);

        I_CurrentIndex--;
        ShowQuestion();
    }





    //!end of region - public functions
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion




    #region private functions


    private void Reset()
    {
        G_ImgTransparentScreen.SetActive(false);
        G_WordTransparentScreen.SetActive(false);
        I_ImageIndex = -1;
        I_WordIndex = -1;
    }


    private void ShowQuestion()
    {
        IF_1Answered = false;
        IF_2Answered = false;

        G_TransparentScreen.SetActive(false);

        instantiatedGameObject = Instantiate(GA_Questions[I_CurrentIndex], T_QParent);
        instantiatedGameObject.GetComponent<Animator>().SetTrigger("in");

        IF_Q1[0] = instantiatedGameObject.transform.GetChild(7).GetComponent<TMP_InputField>();
        IF_Q1[1] = instantiatedGameObject.transform.GetChild(8).GetComponent<TMP_InputField>();

        IF_Q1[0].onValueChanged.AddListener(delegate { InputField1Answered(); });
        IF_Q1[1].onValueChanged.AddListener(delegate { InputField2Answered(); });
    }


    private void InputField1Answered()
    {
        if (IF_Q1[0].text.Length > 0)
            IF_1Answered = true;
        else
            IF_1Answered = false;

        EnableDisableCheckButton();
    }


    private void InputField2Answered()
    {
        if (IF_Q1[1].text.Length > 0)
            IF_2Answered = true;
        else
            IF_2Answered = false;

        EnableDisableCheckButton();
    }


    private void EnableDisableCheckButton()
    {
        if (IF_1Answered && IF_2Answered)
            G_CheckButton.gameObject.SetActive(true);
        else
            G_CheckButton.gameObject.SetActive(false);
    }



    //!end of region - private functions
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion




    #region coroutines


    //part 1 answer check
    IEnumerator IENUM_MatchAnswerCheck()
    {
        if (I_ImageIndex == I_WordIndex)
        {
            //*correct answer
            ScoreManager.instance.RightAnswer(qIndex, questionID: question.id, answerID: GetOptionID(ACA_Words[I_WordIndex].name));

            if (qIndex < GA_Images.Length - 1)
                qIndex++;

            GetData(qIndex);



            StartCoroutine(IENUM_FillImage(IMGA_Lines[I_ImageIndex]));

            yield return new WaitForSeconds(1f);

            AudioManager.Instance.PlayVoice(ACA_Words[I_WordIndex]);
            ANIM_Counter.SetTrigger("active");
            TXT_Counter.text = (I_AnswerCount + 1).ToString();

            GA_WordDots[I_WordIndex].GetComponentInChildren<ParticleSystem>().Play();
            GA_Images[I_ImageIndex].GetComponent<Animator>().SetTrigger("matched");
            GA_Words[I_WordIndex].GetComponent<Animator>().SetTrigger("matched");

            GA_Images[I_ImageIndex].GetComponent<Button>().interactable = false;
            GA_Words[I_WordIndex].GetComponent<Button>().interactable = false;

            Invoke(nameof(Reset), 1f);
            I_AnswerCount++;

            //*checking if all the answers are correct
            if (I_AnswerCount == GA_Questions.Length)
            {
                G_ProceedButton1.SetActive(true);
                AudioManager.Instance.PlaySFX(AC_Proceed);
            }

        }
        else
        {
            //!wrong answer
            ScoreManager.instance.WrongAnswer(qIndex, questionID: question.id, answerID: GetOptionID(ACA_Words[I_WordIndex].name));

            AudioManager.Instance.PlaySFX(AC_Wrong);
            GA_Images[I_ImageIndex].GetComponent<Animator>().SetTrigger("mismatched");
            GA_Words[I_WordIndex].GetComponent<Animator>().SetTrigger("mismatched");

            Invoke(nameof(Reset), 1.2f);
        }

    }


    IEnumerator IENUM_FillImage(Image img)
    {
        // float fillIncrement = Time.deltaTime;

        while (img.fillAmount < 1f)
        {
            img.fillAmount += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
    }


    IEnumerator IENUM_LerpBoardColor(Image img, Color32 currentColor, Color32 targetColor)
    {
        //*slowly changing color for background
        while (_elapsedTime < _desiredDuration)
        {
            _elapsedTime += Time.deltaTime;
            float percentageComplete = _elapsedTime / _desiredDuration;

            img.color = Color.Lerp(currentColor, targetColor, percentageComplete);
            yield return null;
        }

        //resetting elapsed time back to zero
        _elapsedTime = 0f;
    }




    //!end of region - coroutines
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion


    void OnDisable()
    {
        AudioManager.Instance.StopVoice();
        AudioManager.Instance.StopSFX();
    }

    #region QA

    int GetOptionID(string selectedOption)
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i].text == selectedOption)
            {
                return options[i].id;
            }
        }
        return -1;
    }

    bool CheckOptionIsAns(Component option)
    {
        for (int i = 0; i < answers.Length; i++)
        {
            if (option.text == answers[i].text) { return true; }
        }
        return false;
    }

    void GetData(int questionIndex)
    {
        Debug.Log(">>>>>" + questionIndex);
        question = QAManager.instance.GetQuestionAt(0, questionIndex);
        //if(question != null){
        options = QAManager.instance.GetOption(0, questionIndex);
        answers = QAManager.instance.GetAnswer(0, questionIndex);
        // }
    }

    void GetAdditionalData()
    {
        additionalFields = QAManager.instance.GetAdditionalField(0);
    }

    void AssignData()
    {
        // Custom code
        for (int i = 0; i < optionsGO.Length; i++)
        {
            optionsGO[i].GetComponent<Image>().sprite = options[i]._sprite;
            optionsGO[i].tag = "Untagged";
            Debug.Log(optionsGO[i].name, optionsGO[i]);
            if (CheckOptionIsAns(options[i]))
            {
                optionsGO[i].tag = "answer";
            }
        }
        // answerCount.text = "/"+answers.Length;
    }

    #endregion


}
