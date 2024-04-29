using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



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


    [Header("ANIMATOR---------------------------------------------------------")]
    [SerializeField] private Animator ANIM_CheckButton;


    [Space(10)]


    [Header("IMAGE---------------------------------------------------------")]
    [SerializeField] private Image[] IMGA_Lines;


    [Space(10)]


    [Header("INPUTFIELD---------------------------------------------------------")]
    [SerializeField] private TMP_InputField[] IF_Q1;
    [SerializeField] private TMP_InputField[] IF_Q2;
    [SerializeField] private TMP_InputField[] IF_Q3;
    [SerializeField] private TMP_InputField[] IF_Q4;
    [SerializeField] private TMP_InputField[] IF_Q5;

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


    [Space(10)]


    [Header("TRANSFORM---------------------------------------------------------")]
    [SerializeField] private Transform[] TA_Questions;


    //!end of region - unity reference variables
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion



    #region local variables
    private float _elapsedTime, _desiredDuration = 0.5f;
    private int I_ImageIndex;
    private int I_WordIndex;



    private int I_CurrentIndex;
    private int I_AnswerCount;


    //!end of region - local variables
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion


    void Start()
    {
        I_ImageIndex = -1;
        I_WordIndex = -1;


        I_CurrentIndex = -1;
        I_AnswerCount = 0;

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
            StartCoroutine(IENUM_AnswerCheck());
        }
    }


    public void BUT_Word(int index)
    {
        G_WordTransparentScreen.SetActive(true);
        GA_Words[index].GetComponent<Animator>().SetTrigger("clicked");
        I_WordIndex = index;

        if (I_ImageIndex != -1)
        {
            StartCoroutine(IENUM_AnswerCheck());
        }
    }


    public void BUT_Proceed1()
    {
        I_AnswerCount = 0;

        G_Part1.SetActive(false);
        G_Part2.SetActive(true);
        Invoke(nameof(ShowQuestion), 2.85f);
    }


    public void BUT_Proceed2()
    {
        I_AnswerCount = 0;

        G_Part2.SetActive(false);
        G_Part3.SetActive(true);
    }


    public void BUT_Check()
    {
        switch (I_CurrentIndex)
        {
            case 0:
                if (IF_Q1[0].text.ToLower().Equals("s") && IF_Q1[1].text.ToLower().Equals("n"))
                {
                    AudioManager.Instance.PlayVoice(ACA_Words[0]);
                    PlayAnim(0, "matched");
                    I_AnswerCount = 0;
                    Invoke(nameof(ShowQuestion), 2.5f);
                }
                else
                {
                    PlayAnim(0, "mismatched");
                }
                break;

            case 1:
                if (IF_Q2[0].text.ToLower().Equals("i") && IF_Q2[1].text.ToLower().Equals("g"))
                {
                    AudioManager.Instance.PlayVoice(ACA_Words[1]);
                    PlayAnim(1, "matched");
                    I_AnswerCount = 0;
                }
                else
                {
                    PlayAnim(1, "mismatched");
                }
                break;

            case 2:
                if (IF_Q3[0].text.ToLower().Equals("o") && IF_Q3[1].text.ToLower().Equals("y"))
                {
                    AudioManager.Instance.PlayVoice(ACA_Words[2]);
                    PlayAnim(2, "matched");
                    I_AnswerCount = 0;
                }
                else
                {
                    PlayAnim(2, "mismatched");
                }
                break;

            case 3:
                if (IF_Q4[0].text.ToLower().Equals("i") && IF_Q4[1].text.ToLower().Equals("r"))
                {
                    AudioManager.Instance.PlayVoice(ACA_Words[3]);
                    PlayAnim(3, "matched");
                    I_AnswerCount = 0;
                }
                else
                {
                    PlayAnim(3, "mismatched");
                }
                break;

            case 4:
                if (IF_Q5[0].text.ToLower().Equals("e") && IF_Q5[1].text.ToLower().Equals("d"))
                {
                    AudioManager.Instance.PlayVoice(ACA_Words[4]);
                    PlayAnim(4, "matched");
                    I_AnswerCount = 0;
                }
                else
                {
                    PlayAnim(4, "mismatched");
                }
                break;

            default:
                break;
        }


        void PlayAnim(int index, string param)
        {
            TA_Questions[index].GetChild(0).GetComponent<Animator>().SetTrigger(param);
        }

    }



    public void IncrementAnswerCount()
    {
        I_AnswerCount++;

        if (I_AnswerCount == 2)
        {
            CheckInputFields();
        }

        Debug.Log("answer count incremented - " + I_AnswerCount);
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
        G_TransparentScreen.SetActive(false);

        I_CurrentIndex++;

        if (I_CurrentIndex > 0) TA_Questions[I_CurrentIndex - 1].GetComponent<Animator>().SetTrigger("out");

        TA_Questions[I_CurrentIndex].GetComponent<Animator>().SetTrigger("in");
        // TA_Questions[I_CurrentIndex].GetComponent<Animator>().keepAnimatorControllerStateOnDisable = true;
        // Invoke(nameof(DisableAnim), 0.65f);



        void DisableAnim()
        {
            TA_Questions[I_CurrentIndex].GetComponent<Animator>().enabled = false;
        }
    }


    private void CheckInputFields()
    {

        if (I_CurrentIndex == 0)
        {
            if (IF_Q1[0].text.ToLower().Equals("s") && IF_Q1[1].text.ToLower().Equals("n"))
            {
                PlayAnim(0, "matched");
                I_AnswerCount = 0;
                G_TransparentScreen.SetActive(true);
                TA_Questions[I_CurrentIndex].GetComponent<Animator>().enabled = true;
                Invoke(nameof(ShowQuestion), 2.5f);
                return;
            }
            else
            {
                PlayAnim(0, "mismatched");
                I_AnswerCount = 1;
            }
        }
        else if (I_CurrentIndex == 1)
        {
            if (IF_Q2[0].text.ToLower().Equals("i") && IF_Q2[1].text.ToLower().Equals("g"))
            {
                PlayAnim(0, "matched");
                I_AnswerCount = 0;
                G_TransparentScreen.SetActive(true);
                TA_Questions[I_CurrentIndex].GetComponent<Animator>().enabled = true;
                Invoke(nameof(ShowQuestion), 2.5f);
                return;
            }
            else
            {
                PlayAnim(0, "mismatched");
                I_AnswerCount = 1;
            }
        }
        else if (I_CurrentIndex == 2)
        {
            if (IF_Q2[0].text.ToLower().Equals("o") && IF_Q2[1].text.ToLower().Equals("y"))
            {
                PlayAnim(0, "matched");
                I_AnswerCount = 0;
                G_TransparentScreen.SetActive(true);
                TA_Questions[I_CurrentIndex].GetComponent<Animator>().enabled = true;
                Invoke(nameof(ShowQuestion), 2.5f);
                return;
            }
            else
            {
                PlayAnim(0, "mismatched");
                I_AnswerCount = 1;
            }
        }
        else if (I_CurrentIndex == 3)
        {
            if (IF_Q3[0].text.ToLower().Equals("i") && IF_Q3[1].text.ToLower().Equals("r"))
            {
                PlayAnim(0, "matched");
                I_AnswerCount = 0;
                G_TransparentScreen.SetActive(true);
                TA_Questions[I_CurrentIndex].GetComponent<Animator>().enabled = true;
                Invoke(nameof(ShowQuestion), 2.5f);
                return;
            }
            else
            {
                PlayAnim(0, "mismatched");
                I_AnswerCount = 1;
            }
        }
        else if (I_CurrentIndex == 4)
        {
            if (IF_Q4[0].text.ToLower().Equals("b") && IF_Q4[1].text.ToLower().Equals("e"))
            {
                PlayAnim(0, "matched");
                I_AnswerCount = 0;
                G_TransparentScreen.SetActive(true);
                TA_Questions[I_CurrentIndex].GetComponent<Animator>().enabled = true;
                Invoke(nameof(ShowQuestion), 2.5f);
                return;
            }
            else
            {
                PlayAnim(0, "mismatched");
                I_AnswerCount = 1;
            }
        }

        //------

        if (IF_Q1[1].text.ToLower().Equals("n"))
        {
            TA_Questions[0].GetChild(0).GetComponent<Animator>().SetTrigger("matched");
        }
        else
        {
            TA_Questions[0].GetChild(0).GetComponent<Animator>().SetTrigger("mismatched");
        }


        void PlayAnim(int index, string param)
        {
            TA_Questions[index].GetChild(0).GetComponent<Animator>().SetTrigger(param);
        }


        if (IF_Q1[0].text.ToLower().Equals("s") && IF_Q1[1].text.ToLower().Equals("n"))
        {
            TA_Questions[0].GetChild(TA_Questions[0].childCount - 1).gameObject.SetActive(true);
        }

    }



    //!end of region - private functions
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion




    #region coroutines


    IEnumerator IENUM_AnswerCheck()
    {
        //*correct answer
        if (I_ImageIndex == I_WordIndex)
        {
            // GA_ImageDots[I_ImageIndex].SetActive(true);
            StartCoroutine(IENUM_FillImage(IMGA_Lines[I_ImageIndex]));

            yield return new WaitForSeconds(1f);

            AudioManager.Instance.PlayVoice(ACA_Words[I_WordIndex]);
            // GA_WordDots[I_WordIndex].SetActive(true);
            GA_WordDots[I_WordIndex].GetComponentInChildren<ParticleSystem>().Play();
            GA_Images[I_ImageIndex].GetComponent<Animator>().SetTrigger("matched");
            GA_Words[I_WordIndex].GetComponent<Animator>().SetTrigger("matched");

            GA_Images[I_ImageIndex].GetComponent<Button>().interactable = false;
            GA_Words[I_WordIndex].GetComponent<Button>().interactable = false;

            Invoke(nameof(Reset), 1f);
            I_AnswerCount++;

            //*checking if all the answers are correct
            if (I_AnswerCount == TA_Questions.Length)
            {
                G_ProceedButton1.SetActive(true);
                AudioManager.Instance.PlaySFX(AC_Proceed);
            }

        }
        //!wrong answer
        else
        {
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




}
