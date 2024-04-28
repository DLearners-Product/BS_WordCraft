using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordsOnPaper2 : MonoBehaviour
{

    [SerializeField] private AudioClip[] ACA_Words;

    [SerializeField] private Animator ANIM_SpeakerEffect;




    private int I_CurrentIndex;


    void Start()
    {
        I_CurrentIndex = 0;
    }


    public void BUT_Next()
    {
        I_CurrentIndex++;

        if (I_CurrentIndex >= ACA_Words.Length)
        {
            return;
        }

        BUT_Speaker();
    }


    public void BUT_Back()
    {
        I_CurrentIndex--;
        BUT_Speaker();
    }


    public void BUT_Speaker()
    {
        ANIM_SpeakerEffect.SetTrigger("active");
        AudioManager.Instance.PlayVoice(ACA_Words[I_CurrentIndex]);
    }



}
