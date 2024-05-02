using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SentenceWriting : MonoBehaviour
{

    [SerializeField] private AudioClip[] ACA_Sentences;

    [Space(10)]

    [SerializeField] private Sprite[] SPRA_Images;

    [Space(10)]

    [SerializeField] private Image IMG_Placeholder;

    [Space(10)]

    [SerializeField] private TextMeshProUGUI TXT_Sentence;

    [Space(10)]

    [SerializeField] private MouseClickAudio REF_MouseClickAudio;



    private string[] STRA_Sentences = new string[] {
    "We ate a <color=orange><b>big</color><b> cake.",
    "<color=orange><b>I</color><b> like red <color=orange><b>and</color><b> blue.",
    "<color=orange><b>We</color><b> are eating.",
    "Do <color=orange><b>not</color><b> sit here.",
    "<color=orange><b>The</color><b> ball rolled away."
};
    private int I_CurrentIndex;


    void Start()
    {
        I_CurrentIndex = 0;
        AudioManager.Instance.PlayVoice(ACA_Sentences[I_CurrentIndex]);
        REF_MouseClickAudio.clip = ACA_Sentences[I_CurrentIndex];
    }



    public void BUT_Next()
    {
        I_CurrentIndex++;

        if (I_CurrentIndex >= STRA_Sentences.Length)
        {
            return;
        }

        ShowCurrent();
    }


    public void BUT_Back()
    {
        I_CurrentIndex--;

        ShowCurrent();
    }


    public void ShowCurrent()
    {
        IMG_Placeholder.GetComponent<Animator>().SetTrigger("active");
        IMG_Placeholder.sprite = SPRA_Images[I_CurrentIndex];
        TXT_Sentence.text = STRA_Sentences[I_CurrentIndex];
        AudioManager.Instance.PlayVoice(ACA_Sentences[I_CurrentIndex]);
        REF_MouseClickAudio.clip = ACA_Sentences[I_CurrentIndex];
    }


    void OnDisable()
    {
        AudioManager.Instance.StopVoice();
        AudioManager.Instance.StopSFX();
    }

}
