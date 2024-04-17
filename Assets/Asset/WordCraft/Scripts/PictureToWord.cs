using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PictureToWord : MonoBehaviour
{

    #region ---------------------------------------unity reference variables---------------------------------------

    [Header("AUDIO---------------------------------------------------------")]
    [SerializeField] private AudioClip[] ACA_Words;
    [SerializeField] private AudioClip AC_Correct;
    [SerializeField] private AudioClip AC_Wrong;
    [SerializeField] private AudioClip AC_BubblyButtonClick;

    [Space(10)]

    [Header("IMAGE---------------------------------------------------------")]
    [SerializeField] private Image[] IMGA_Lines;

    [Space(10)]

    [Header("GAME OBJECT---------------------------------------------------------")]
    [SerializeField] private GameObject[] GA_Images;
    [SerializeField] private GameObject[] GA_Words;
    [SerializeField] private GameObject[] GA_ImageDots;
    [SerializeField] private GameObject[] GA_WordDots;
    [SerializeField] private GameObject G_ImgTransparentScreen;
    [SerializeField] private GameObject G_WordTransparentScreen;


    //!end of region - unity reference variables
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion



    #region ---------------------------------------local variables---------------------------------------
    private float _elapsedTime, _desiredDuration = 0.5f;
    private int I_ImageIndex;
    private int I_WordIndex;


    //!end of region - local variables
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion


    void Start()
    {
        I_ImageIndex = -1;
        I_WordIndex = -1;
    }




    #region ---------------------------------------public functions---------------------------------------


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


    //!end of region - public functions
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion



    #region ---------------------------------------private functions---------------------------------------


    private void Reset()
    {
        G_ImgTransparentScreen.SetActive(false);
        G_WordTransparentScreen.SetActive(false);
        I_ImageIndex = -1;
        I_WordIndex = -1;
    }


    //!end of region - private functions
    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
    #endregion




    #region ---------------------------------------coroutines---------------------------------------


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
