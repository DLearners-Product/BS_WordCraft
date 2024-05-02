using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PictureSentence : MonoBehaviour
{


    [SerializeField] private AudioClip[] ACA_Words;
    [SerializeField] private AudioClip[] ACA_ImageWords;

    [Space(10)]

    [SerializeField] private Sprite[] SPRA_Images;

    [Space(10)]

    [SerializeField] private Text TXT_Word;
    [SerializeField] private TextMeshProUGUI[] TXT_Sentence;

    [Space(10)]

    [SerializeField] private Image IMG_Placeholder;

    [Space(10)]

    [SerializeField] private TextMeshProUGUI[] TXTA_Sentences;

    [Space(10)]

    [SerializeField] private GameObject G_Overlay;
    [SerializeField] private GameObject G_Activity;



    private string[] words = new string[] { "pot", "toy", "run", "bed", "boy", "us", "sun", };
    private string[] overlayWords = new string[] { "The", " big ball rolls ", "and", " bounces well." };

    private int I_CurrentIndex;


    void Start()
    {
        I_CurrentIndex = 0;
        StartCoroutine(IENUM_Sentence());
    }


    public void BUT_Next()
    {
        I_CurrentIndex++;

        ShowImageAndText();
    }


    public void BUT_Back()
    {
        I_CurrentIndex--;

        ShowImageAndText();
    }


    private void ShowImageAndText()
    {
        IMG_Placeholder.GetComponent<Animator>().SetTrigger("active");
        IMG_Placeholder.sprite = SPRA_Images[I_CurrentIndex];
        TXT_Word.text = words[I_CurrentIndex];
    }


    public void BUT_ImageVO()
    {
        AudioManager.Instance.PlayVoice(ACA_ImageWords[I_CurrentIndex]);
    }


    public void BUT_Word(int index)
    {
        AudioManager.Instance.PlayVoice(ACA_Words[index]);
    }


    public void BUT_Skip()
    {
        G_Overlay.SetActive(false);
        G_Activity.SetActive(true);
    }


    IEnumerator IENUM_Sentence()
    {
        for (int i = 0; i < TXTA_Sentences.Length; i++)
        {
            string fullText = overlayWords[i];
            TXTA_Sentences[i].text = "";

            foreach (char letter in fullText)
            {
                TXTA_Sentences[i].text += letter;
                yield return new WaitForSeconds(0.15f);
            }
        }

        yield return new WaitForSeconds(2.5f);

        for (int i = 0; i < TXTA_Sentences.Length; i++) { TXTA_Sentences[i].text = ""; }
        StartCoroutine(IENUM_Sentence());
    }


    void OnDisable()
    {
        AudioManager.Instance.StopVoice();
        AudioManager.Instance.StopSFX();
    }

}