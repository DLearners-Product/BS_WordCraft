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

    [SerializeField] private Animator ANIM_SpeakerEffect;

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



    void Start()
    {
        I_CurrentIndex = -1;
        letterImageIndexList = new List<Image>();
        GotoNextWord();
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
                for (int i = 0; i < letterImageIndexList.Count; i++) { letterImageIndexList[i].color = CLR_Correct; }

                PS_CorrectEffect.Play();
                AudioManager.Instance.PlaySFX(AC_Correct);
                G_TransparentScreen.SetActive(true);
                Invoke(nameof(GotoNextWord), 2.5f);
            }
            else
            {
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
            Invoke(nameof(ShowActivityCompleted), 2.5f);
        }

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



}
