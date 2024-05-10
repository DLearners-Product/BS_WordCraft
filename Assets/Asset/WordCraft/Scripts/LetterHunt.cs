using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LetterHunt : MonoBehaviour
{

    public Color32 CLR_Correct;
    public Color32 CLR_Wrong;

    [SerializeField] private AudioClip[] ACA_Words;
    [SerializeField] private AudioClip AC_LetterClick;
    [SerializeField] private AudioClip AC_Clear;
    [SerializeField] private AudioClip AC_Correct;
    [SerializeField] private AudioClip AC_Wrong;

    [Space(10)]

    [Header("ANIMATOR---------------------------------------------------------")]
    [SerializeField] private Animator ANIM_SpeakerEffect;
    [SerializeField] private Animator ANIM_Counter;

    [Space(10)]

    [Header("TEXTMESHPRO---------------------------------------------------------")]
    [SerializeField] private TextMeshProUGUI TXT_Counter;

    [Space(10)]

    [SerializeField] private Button BTN_Clear;

    [Space(10)]

    [SerializeField] private TextMeshProUGUI TXT_FormedWord;

    [Space(10)]

    [SerializeField] private GameObject G_TransparentScreen;
    [SerializeField] private GameObject G_ActivityCompleted;

    [Space(10)]

    [SerializeField] private Transform TA_Letters;

    [Space(10)]

    [SerializeField] private ParticleSystem PS_CorrectEffect;



    private string[] STRA_Letters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    private string[] STRA_Words = new string[] { "and", "big", "it", "is", "the", "pot", "not", "we", "us", "boy", "girl", "sun" };
    private List<Image> letterImageIndexList;
    private int I_CurrentIndex;


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
        letterImageIndexList = new List<Image>();
        GotoNextWord();

        #region DataSetter
        //Main_Blended.OBJ_main_blended.levelno = 3;
        QAManager.instance.UpdateActivityQuestion();
        qIndex = 0;
        // GetData(qIndex);
        GetAdditionalData();
        AssignData();
        #endregion

    }


    public void BUT_Speaker()
    {
        AudioManager.Instance.PlayVoice(ACA_Words[I_CurrentIndex]);
        ANIM_SpeakerEffect.SetTrigger("active");
    }


    public void BUT_LetterClick(int index)
    {
        AudioManager.Instance.PlayVoice(AC_LetterClick);

        if (TXT_FormedWord.text.Length >= 4)
        {
            return;
        }
        BTN_Clear.interactable = true;
        letterImageIndexList.Add(TA_Letters.GetChild(index).GetComponent<Image>());

        TXT_FormedWord.text += STRA_Letters[index];

        if (TXT_FormedWord.text.Length == STRA_Words[I_CurrentIndex].Length)
        {
            if (TXT_FormedWord.text.ToString().Equals(STRA_Words[I_CurrentIndex]))
            {
                //*correct answer
                // ScoreManager.instance.RightAnswer(qIndex, questionID: question.id, answerID: GetOptionID(TXT_FormedWord.text.ToString()));
                ScoreManager.instance.RightAnswer(qIndex, questionID: question.id, answer: TXT_FormedWord.text);

                if (qIndex < ACA_Words.Length - 1)
                    qIndex++;

                GetData(qIndex);


                for (int i = 0; i < letterImageIndexList.Count; i++) { letterImageIndexList[i].color = CLR_Correct; }

                PS_CorrectEffect.Play();
                AudioManager.Instance.PlaySFX(AC_Correct);
                G_TransparentScreen.SetActive(true);
                Invoke(nameof(GotoNextWord), 2.5f);
            }
            else
            {
                //!wrong answer
                ScoreManager.instance.WrongAnswer(qIndex, questionID: question.id, answer: TXT_FormedWord.text);

                for (int i = 0; i < letterImageIndexList.Count; i++) { letterImageIndexList[i].color = CLR_Wrong; }

                AudioManager.Instance.PlaySFX(AC_Wrong);
                Invoke(nameof(BUT_Clear), 1f);
            }
        }
    }


    private void GotoNextWord()
    {
        BUT_Clear();
        if (letterImageIndexList.Count > 0) ColorReset();

        G_TransparentScreen.SetActive(false);
        I_CurrentIndex++;

        if (I_CurrentIndex >= STRA_Words.Length)
        {
            Invoke(nameof(ShowActivityCompleted), 2f);
            return;
        }

        ANIM_Counter.SetTrigger("active");
        TXT_Counter.text = (I_CurrentIndex + 1).ToString();

        TXT_FormedWord.text = "";
        PlayWordSound();
    }


    private void PlayWordSound()
    {
        AudioManager.Instance.PlayVoice(ACA_Words[I_CurrentIndex]);
        ANIM_SpeakerEffect.SetTrigger("active");

    }


    public void BUT_Clear()
    {
        if (letterImageIndexList.Count > 0) ColorReset();

        AudioManager.Instance.PlaySFX(AC_Clear);
        TXT_FormedWord.text = "";
        BTN_Clear.interactable = false;
    }


    public void ColorReset()
    {
        for (int i = 0; i < letterImageIndexList.Count; i++) { letterImageIndexList[i].color = new Color32(255, 255, 255, 255); }
        letterImageIndexList.Clear();
    }


    private void ShowActivityCompleted()
    {
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
