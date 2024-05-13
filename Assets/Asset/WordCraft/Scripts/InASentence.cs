using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InASentence : MonoBehaviour
{


    [SerializeField] private AudioClip AC_Click;
    [SerializeField] private AudioClip AC_Correct;
    [SerializeField] private AudioClip AC_Wrong;

    [Space(10)]

    [SerializeField] private Animator ANIM_Counter;

    [Space(10)]

    [SerializeField] private TextMeshProUGUI TXT_Counter;

    [Space(10)]

    [SerializeField] private GameObject[] GA_Questions;
    [SerializeField] private GameObject[] GA_Options;
    [SerializeField] private GameObject[] GA_Answers;
    [SerializeField] private GameObject G_Next;
    [SerializeField] private GameObject G_ActivityCompleted;


    private int I_CurrentIndex;


    private List<string> qList = new List<string>();


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
        I_CurrentIndex = -1;
        BUT_Next();

        #region DataSetter
        //Main_Blended.OBJ_main_blended.levelno = 3;
        QAManager.instance.UpdateActivityQuestion();
        qIndex = 0;
        GetData(qIndex);
        GetAdditionalData();
        AssignData();
        #endregion

    }


    public void BUT_Next()
    {
        AudioManager.Instance.PlaySFX(AC_Click);
        StartCoroutine(IENUM_ShowNextQuestion());
    }


    IEnumerator IENUM_ShowNextQuestion()
    {
        yield return null;
        G_Next.SetActive(false);

        if (I_CurrentIndex > -1)
        {

            GA_Options[I_CurrentIndex].SetActive(false);
            GA_Questions[I_CurrentIndex].SetActive(false);
        }

        I_CurrentIndex++;

        if (I_CurrentIndex >= GA_Questions.Length)
        {
            ShowActivityCompleted();
            yield break;
        }

        TXT_Counter.text = (I_CurrentIndex + 1).ToString();
        ANIM_Counter.SetTrigger("active");
        GA_Options[I_CurrentIndex].SetActive(true);
        GA_Questions[I_CurrentIndex].SetActive(true);
    }


    public void BUT_CorrectAnswer(string ans)
    {
        //*correct answer
        ScoreManager.instance.RightAnswer(qIndex, questionID: question.id, answerID: GetOptionID(ans));

        if (qIndex < GA_Questions.Length - 1)
            qIndex++;

        GetData(qIndex);


        //audio feedback
        AudioManager.Instance.PlaySFX(AC_Correct);

        //visual feedback
        //TODO : particles
        GA_Answers[I_CurrentIndex].SetActive(true);

        //show next button
        G_Next.SetActive(true);
    }


    public void BUT_WrongAnswer(string ans)
    {
        //!wrong answer
        ScoreManager.instance.WrongAnswer(qIndex, questionID: question.id, answerID: GetOptionID(ans));

        AudioManager.Instance.PlaySFX(AC_Wrong);
    }



    private void ShowActivityCompleted()
    {
        BlendedOperations.instance.NotifyActivityCompleted();
        G_ActivityCompleted.SetActive(true);
    }


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