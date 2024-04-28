using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LetterHunt : MonoBehaviour
{

    [SerializeField] private AudioClip[] ACA_Words;
    [SerializeField] private AudioClip[] ACA_Letters;
    [SerializeField] private AudioClip AC_LetterClick;
    [SerializeField] private AudioClip AC_Clear;
    [SerializeField] private AudioClip AC_Correct;
    [SerializeField] private AudioClip AC_Wrong;


    [SerializeField] private TextMeshProUGUI TXT_FormedWord;


    [SerializeField] private GameObject G_TransparentScreen;
    [SerializeField] private GameObject G_ActivityCompleted;


    private string[] STRA_Letters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    private string[] STRA_Words = new string[] { "and", "big", "it", "is", "the", "pot", "not", "we", "us", "boy", "girl", "sun" };
    private int I_CurrentIndex;



    void Start()
    {
        I_CurrentIndex = -1;
        GotoNextWord();
    }



    public void BUT_Speaker()
    {
        AudioManager.Instance.PlayVoice(ACA_Words[I_CurrentIndex]);
    }





    public void BUT_LetterClick(int index)
    {
        // AudioManager.Instance.PlayVoice(ACA_Letters[index]);
        AudioManager.Instance.PlayVoice(AC_LetterClick);

        if (TXT_FormedWord.text.Length >= 4)
        {
            return;
        }

        TXT_FormedWord.text += STRA_Letters[index];

        if (TXT_FormedWord.text.Length == STRA_Words[I_CurrentIndex].Length)
        {
            if (TXT_FormedWord.text.ToString().Equals(STRA_Words[I_CurrentIndex]))
            {
                AudioManager.Instance.PlaySFX(AC_Correct);
                G_TransparentScreen.SetActive(true);
                Invoke(nameof(GotoNextWord), 2.5f);
            }
            else
            {
                AudioManager.Instance.PlaySFX(AC_Wrong);
                Invoke(nameof(ClearText), 1f);
            }

        }

    }


    private void ClearText()
    {
        AudioManager.Instance.PlaySFX(AC_Clear);
        TXT_FormedWord.text = "";
    }


    private void GotoNextWord()
    {
        G_TransparentScreen.SetActive(false);
        I_CurrentIndex++;

        if (I_CurrentIndex >= STRA_Words.Length)
        {
            Invoke(nameof(ShowActivityCompleted), 2.5f);
        }

        TXT_FormedWord.text = "";
        PlayWordSound();
    }


    private void PlayWordSound()
    {
        AudioManager.Instance.PlayVoice(ACA_Words[I_CurrentIndex]);
    }


    public void BUT_Check()
    {
        //if (TXT_FormedWord.text == "")
        //{
        //    return;
        //}

        //if (TXT_FormedWord.text.Length < 3)
        //{
        //    return;
        //
    }


    private void ShowActivityCompleted()
    {
        G_ActivityCompleted.SetActive(true);
    }



}
