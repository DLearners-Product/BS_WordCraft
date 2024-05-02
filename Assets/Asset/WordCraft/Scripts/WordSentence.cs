using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WordSentence : MonoBehaviour
{

    [SerializeField] private AudioClip[] ACA_Words;

    [Space(10)]

    [SerializeField] private TextMeshProUGUI TXT_Placeholder;

    [Space(10)]

    [SerializeField] private MouseClickAudio REF_MouseClickAudio;

    private string[] STRA_Words = new string[] {
    "and",
    "big",
    "the",
    "I",
    "it",
    "we",
    "not"
    };

    private int I_CurrentIndex;


    void Start()
    {
        I_CurrentIndex = 0;
        PlayCurrentAudio();
        SetCurrentAudioToText();
    }


    public void BUT_Next()
    {
        I_CurrentIndex++;

        if (I_CurrentIndex >= STRA_Words.Length)
        {
            return;
        }

        TXT_Placeholder.text = STRA_Words[I_CurrentIndex];
        PlayCurrentAudio();
        SetCurrentAudioToText();
    }


    public void BUT_Back()
    {
        I_CurrentIndex--;
        TXT_Placeholder.text = STRA_Words[I_CurrentIndex];
        PlayCurrentAudio();
        SetCurrentAudioToText();
    }


    private void PlayCurrentAudio()
    {
        AudioManager.Instance.PlayVoice(ACA_Words[I_CurrentIndex]);
    }


    private void SetCurrentAudioToText()
    {
        REF_MouseClickAudio.clip = ACA_Words[I_CurrentIndex];
    }


    void OnDisable()
    {
        AudioManager.Instance.StopVoice();
        AudioManager.Instance.StopSFX();
    }

}
