using System.Collections;
using UnityEngine;
using TMPro;


public class WordsOnPaper1 : MonoBehaviour
{

    [SerializeField] private AudioClip[] ACA_Sentences;
    [SerializeField] private AudioClip[] ACA_Words;

    [Space(10)]

    [SerializeField] private GameObject G_Notebook;
    [SerializeField] private GameObject G_NotebookAndNumbers;
    [SerializeField] private GameObject[] GA_Words;


    private string[] words = new string[] { "less", "star", "fish", "the", "net" };
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

        for (int i = I_CurrentIndex; i < words.Length; i++)
        {
            string fullText = words[I_CurrentIndex];

            for (int j = 0; j < fullText.Length; j++)
            {
                GA_Words[I_CurrentIndex].GetComponent<TextMeshProUGUI>().text += fullText[j];

                yield return new WaitForSeconds(0.2f);
            }

            AudioManager.Instance.PlayVoice(ACA_Words[I_CurrentIndex]);
            I_CurrentIndex++;
            yield return new WaitForSeconds(2f);
        }

    }

    void OnDisable()
    {
        AudioManager.Instance.StopVoice();
        AudioManager.Instance.StopSFX();
    }

}