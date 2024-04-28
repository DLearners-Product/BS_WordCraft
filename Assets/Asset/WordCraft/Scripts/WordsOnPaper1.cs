using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordsOnPaper1 : MonoBehaviour
{

    [SerializeField] private AudioClip[] ACA_Sentences;
    [SerializeField] private AudioClip[] ACA_Words;

    [Space(10)]

    [SerializeField] private GameObject G_Notebook;
    [SerializeField] private GameObject G_NotebookAndNumbers;
    [SerializeField] private GameObject[] GA_Words;

    private int I_CurrentIndex;


    void Start()
    {
        I_CurrentIndex = 0;

        StartCoroutine(IENUM_Play());
    }


    IEnumerator IENUM_Play()
    {
        AudioManager.Instance.PlayVoice(ACA_Sentences[0]);
        G_Notebook.SetActive(true);

        yield return new WaitForSeconds(ACA_Sentences[0].length + 1f);

        AudioManager.Instance.PlayVoice(ACA_Sentences[1]);
        G_NotebookAndNumbers.SetActive(true);

        yield return new WaitForSeconds(ACA_Sentences[1].length + 0.5f);

        for (int i = I_CurrentIndex; i < GA_Words.Length; i++)
        {
            PlayWord();
            yield return new WaitForSeconds(2f);
        }

    }


    public void PlayWord()
    {
        GA_Words[I_CurrentIndex].SetActive(true);
        AudioManager.Instance.PlayVoice(ACA_Words[I_CurrentIndex]);

        I_CurrentIndex++;
    }







}