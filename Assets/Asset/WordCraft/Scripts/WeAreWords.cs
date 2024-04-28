using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeAreWords : MonoBehaviour
{

    [SerializeField] private AudioClip[] ACA_Para1;
    [SerializeField] private AudioClip[] ACA_Examples1;
    [SerializeField] private AudioClip[] ACA_Para2;
    [SerializeField] private AudioClip[] ACA_Examples2;
    [SerializeField] private AudioClip[] ACA_OnlyVO;

    [SerializeField] private AudioClip[] ACA_Words1;
    [SerializeField] private AudioClip[] ACA_Words2;
    [SerializeField] private AudioClip[] ACA_Words3;
    [SerializeField] private AudioClip[] ACA_Words4;

    [Space(10)]

    [SerializeField] private GameObject G_NextButton;
    [SerializeField] private GameObject[] GA_Para1;
    [SerializeField] private GameObject[] GA_Examples1;
    [SerializeField] private GameObject[] GA_Para2;
    [SerializeField] private GameObject[] GA_Examples2;
    [SerializeField] private GameObject G_Text;
    [SerializeField] private GameObject G_Words;
    [SerializeField] private GameObject[] GA_Words;
    [SerializeField] private GameObject G_DotNavigation;
    [SerializeField] private GameObject G_TransparentScreen;


    private int I_Count;
    private int I_CurrentIndex;
    private List<AudioClip> ACA_WordsList = new List<AudioClip>();


    void Start()
    {
        I_Count = 0;
        I_CurrentIndex = -1;

        StartCoroutine(IENUM_Play(0));
    }


    IEnumerator IENUM_Play(int flowIndex)
    {
        if (flowIndex == 0)
        {
            //what is word
            AudioManager.Instance.PlayVoice(ACA_Para1[0]);
            GA_Para1[0].SetActive(true);

            yield return new WaitForSeconds(ACA_Para1[0].length + 0.5f);

            //a word is a group
            AudioManager.Instance.PlayVoice(ACA_Para1[1]);
            GA_Para1[1].SetActive(true);

            yield return new WaitForSeconds(ACA_Para1[1].length + 1f);

            //disabling
            for (int i = 0; i < GA_Para1.Length; i++) { GA_Para1[i].SetActive(false); }

            //house
            AudioManager.Instance.PlayVoice(ACA_Examples1[0]);
            GA_Examples1[0].SetActive(true);

            yield return new WaitForSeconds(ACA_Examples1[0].length + 0.5f);

            //that
            AudioManager.Instance.PlayVoice(ACA_Examples1[1]);
            GA_Examples1[1].SetActive(true);

            yield return new WaitForSeconds(ACA_Examples1[1].length + 0.5f);

            //are words
            //jgly is not a word
            // AudioManager.Instance.PlayVoice(ACA_Examples1[2]);
            GA_Examples1[2].SetActive(true);

            // yield return new WaitForSeconds(ACA_Examples1[1].length + 0.5f);

            yield return new WaitForSeconds(2f);

            //disabling
            GA_Examples1[2].SetActive(false);
            for (int i = 0; i < GA_Para1.Length; i++) { GA_Examples1[i].SetActive(false); }

            //para2.1
            AudioManager.Instance.PlayVoice(ACA_Para2[0]);
            GA_Para2[0].SetActive(true);

            yield return new WaitForSeconds(ACA_Para2[0].length + 0.5f);

            //para2.2
            GA_Para2[0].SetActive(false);
            AudioManager.Instance.PlayVoice(ACA_Para2[1]);
            GA_Para2[1].SetActive(true);

            //disabling
            yield return new WaitForSeconds(ACA_Para2[1].length + 0.5f);

            GA_Para2[1].SetActive(false);

            //write your name
            AudioManager.Instance.PlayVoice(ACA_Examples2[0]);
            GA_Examples2[0].SetActive(true);
            G_NextButton.SetActive(true);
        }
        else if (flowIndex == 1)
        {
            GA_Examples2[0].SetActive(false);

            //write i
            AudioManager.Instance.PlayVoice(ACA_Examples2[1]);
            GA_Examples2[1].SetActive(true);
            G_NextButton.SetActive(true);
        }
        else if (flowIndex == 2)
        {
            GA_Examples2[1].SetActive(false);

            //now lets try
            AudioManager.Instance.PlayVoice(ACA_OnlyVO[0]);

            yield return new WaitForSeconds(ACA_OnlyVO[0].length + 0.5f);

            //write big
            AudioManager.Instance.PlayVoice(ACA_OnlyVO[1]);

            yield return new WaitForSeconds(ACA_OnlyVO[1].length + 2f);

            //write the
            AudioManager.Instance.PlayVoice(ACA_OnlyVO[2]);

            yield return new WaitForSeconds(ACA_OnlyVO[2].length + 2f);

            //lets write words
            G_Text.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            G_NextButton.SetActive(false);
            G_DotNavigation.SetActive(true);
            BUT_Next();

        }


    }


    public void BUT_Next() { StartCoroutine(IENUM_Next("next")); }


    public void BUT_Back() { StartCoroutine(IENUM_Next("back")); }


    IEnumerator IENUM_Next(string dir)
    {
        G_TransparentScreen.SetActive(true);

        if (dir == "next")
        {
            I_CurrentIndex++;
            if (I_CurrentIndex > 0)
            {
                for (int i = 0; i < GA_Words.Length; i++) GA_Words[I_CurrentIndex - 1].transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else if (dir == "back")
        {
            I_CurrentIndex--;
            for (int i = 0; i < GA_Words.Length; i++) GA_Words[I_CurrentIndex + 1].transform.GetChild(i).gameObject.SetActive(false);
        }

        ACA_WordsList.Clear();

        if (I_CurrentIndex == 0) for (int i = 0; i < ACA_Words1.Length; i++) ACA_WordsList.Add(ACA_Words1[i]);
        else if (I_CurrentIndex == 1) for (int i = 0; i < ACA_Words1.Length; i++) ACA_WordsList.Add(ACA_Words2[i]);
        else if (I_CurrentIndex == 2) for (int i = 0; i < ACA_Words1.Length; i++) ACA_WordsList.Add(ACA_Words3[i]);
        else if (I_CurrentIndex == 3) for (int i = 0; i < ACA_Words1.Length; i++) ACA_WordsList.Add(ACA_Words4[i]);


        for (int i = 0; i < GA_Words[I_CurrentIndex].transform.childCount; i++)
        {
            GA_Words[I_CurrentIndex].transform.GetChild(i).gameObject.SetActive(true);
            AudioManager.Instance.PlayVoice(ACA_WordsList[i]);
            yield return new WaitForSeconds(2f);
        }

        G_TransparentScreen.SetActive(false);
    }


    public void BUT_FlowNext()
    {
        StartCoroutine(IENUM_Play(++I_Count));
    }


}
