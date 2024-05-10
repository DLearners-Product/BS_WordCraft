using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


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

    [SerializeField] private Animator ANIM_Speaker;

    [Space(10)]

    [SerializeField] private Button BTN_Nxt;
    [SerializeField] private Button BTN_Bck;

    [Space(10)]

    [SerializeField] private GameObject[] GA_Para1;
    [SerializeField] private GameObject[] GA_Examples1;
    [SerializeField] private GameObject[] GA_Para2;
    [SerializeField] private GameObject[] GA_Examples2;
    [SerializeField] private GameObject G_Text;
    [SerializeField] private GameObject[] GA_Words;
    [SerializeField] private GameObject G_DotNavigation;
    [SerializeField] private GameObject G_TransparentScreen;


    private int I_Count;
    private int I_CurrentIndex;
    private int I_FlowIndex;
    private float F_ShortDelay;
    private List<AudioClip> ACA_WordsList = new List<AudioClip>();


    void Start()
    {
        I_Count = 0;
        I_CurrentIndex = -1;
        I_FlowIndex = 0;
        F_ShortDelay = 0.5f;
        StartCoroutine(IENUM_Play(0));
    }


    public void BUT_Nxt()
    {
        I_FlowIndex++;
        StopAllCoroutines();
        StartCoroutine(IENUM_Play(I_FlowIndex));
    }


    public void BUT_Bck()
    {
        I_FlowIndex--;
        StopAllCoroutines();
        StartCoroutine(IENUM_Play(I_FlowIndex));
    }


    IEnumerator IENUM_Play(int flowIndex)
    {
        DisableNavButtons();
        ANIM_Speaker.SetTrigger("active");

        if (flowIndex == 0)
        {
            GA_Para1[1].SetActive(false);
            //what is word
            AudioManager.Instance.PlayVoice(ACA_Para1[0]);
            GA_Para1[0].SetActive(true);

            yield return new WaitForSeconds(ACA_Para1[0].length + F_ShortDelay);
        }
        else if (flowIndex == 1)
        {
            //disabling examples
            for (int i = 0; i < GA_Examples1.Length; i++) { GA_Examples1[i].SetActive(false); }
            //enabling what is a word
            GA_Para1[0].SetActive(true);

            //a word is a group
            AudioManager.Instance.PlayVoice(ACA_Para1[1]);
            GA_Para1[1].SetActive(true);

            yield return new WaitForSeconds(ACA_Para1[1].length + F_ShortDelay);
        }
        else if (flowIndex == 2)
        {
            //disabling
            for (int i = 0; i < GA_Para1.Length; i++) { GA_Para1[i].SetActive(false); }

            //house
            AudioManager.Instance.PlayVoice(ACA_Examples1[0]);
            GA_Examples1[0].SetActive(true);

            yield return new WaitForSeconds(ACA_Examples1[0].length + F_ShortDelay);

            //that
            AudioManager.Instance.PlayVoice(ACA_Examples1[1]);
            GA_Examples1[1].SetActive(true);

            yield return new WaitForSeconds(ACA_Examples1[1].length + F_ShortDelay);

            //are words
            AudioManager.Instance.PlayVoice(ACA_Examples1[2]);
            yield return new WaitForSeconds(ACA_Examples1[2].length + F_ShortDelay);
        }
        else if (flowIndex == 3)
        {
            //disabling para2.1
            GA_Para2[0].SetActive(false);

            //jgly
            GA_Examples1[2].SetActive(true);
            AudioManager.Instance.PlayVoice(ACA_Examples1[3]);
            yield return new WaitForSeconds(ACA_Examples1[3].length + F_ShortDelay);

            //is not a word
            AudioManager.Instance.PlayVoice(ACA_Examples1[4]);
            yield return new WaitForSeconds(ACA_Examples1[4].length + F_ShortDelay);
        }
        else if (flowIndex == 4)
        {
            //disabling
            for (int i = 0; i < GA_Para2.Length; i++) { GA_Para2[i].SetActive(false); }

            GA_Examples1[2].SetActive(true);

            //disabling
            GA_Examples1[2].SetActive(false);
            for (int i = 0; i < GA_Para1.Length; i++) { GA_Examples1[i].SetActive(false); }

            //para2.1
            AudioManager.Instance.PlayVoice(ACA_Para2[0]);
            GA_Para2[0].SetActive(true);

            yield return new WaitForSeconds(ACA_Para2[0].length + F_ShortDelay);
        }
        else if (flowIndex == 5)
        {
            //disabling para2.2 and example2.1
            GA_Para2[0].SetActive(false);
            GA_Examples2[0].SetActive(false);

            AudioManager.Instance.PlayVoice(ACA_Para2[1]);
            GA_Para2[1].SetActive(true);

            yield return new WaitForSeconds(ACA_Para2[1].length + F_ShortDelay);
        }
        else if (flowIndex == 6)
        {
            //disabling
            GA_Para2[1].SetActive(false);
            GA_Examples2[1].SetActive(false);

            //write your name
            AudioManager.Instance.PlayVoice(ACA_Examples2[0]);
            GA_Examples2[0].SetActive(true);
        }
        else if (flowIndex == 7)
        {
            GA_Examples2[0].SetActive(false);
            GA_Examples2[1].SetActive(true);

            //write i
            AudioManager.Instance.PlayVoice(ACA_Examples2[1]);

            yield return new WaitForSeconds(ACA_Examples2[1].length + F_ShortDelay);
        }
        else if (flowIndex == 8)
        {
            //I am smart
            AudioManager.Instance.PlayVoice(ACA_Examples2[2]);

            yield return new WaitForSeconds(ACA_Examples2[2].length + F_ShortDelay);
        }
        else if (flowIndex == 9)
        {
            //disabling
            GA_Examples2[1].SetActive(false);

            //now lets try
            AudioManager.Instance.PlayVoice(ACA_OnlyVO[0]);

            yield return new WaitForSeconds(ACA_OnlyVO[0].length + F_ShortDelay);

            //write big
            AudioManager.Instance.PlayVoice(ACA_OnlyVO[1]);

            yield return new WaitForSeconds(ACA_OnlyVO[1].length + F_ShortDelay);
        }
        else if (flowIndex == 10)
        {
            //write the
            AudioManager.Instance.PlayVoice(ACA_OnlyVO[2]);

            yield return new WaitForSeconds(ACA_OnlyVO[2].length + F_ShortDelay);
        }
        else if (flowIndex == 11)
        {
            //lets write words
            G_Text.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            HideNavButtons();
            G_DotNavigation.SetActive(true);
            BUT_Next();
        }

        EnableNavButtons();
        ANIM_Speaker.SetTrigger("inactive");
    }


    private void EnableNavButtons()
    {
        BTN_Nxt.interactable = true;
        BTN_Bck.interactable = true;

        if (I_FlowIndex == 1) { BTN_Bck.interactable = true; }
        if (I_FlowIndex == 0) { BTN_Bck.interactable = false; }
    }


    private void DisableNavButtons()
    {
        BTN_Nxt.interactable = false;
        BTN_Bck.interactable = false;
    }


    private void HideNavButtons()
    {
        BTN_Nxt.gameObject.SetActive(false);
        BTN_Bck.gameObject.SetActive(false);
    }


    public void BUT_Next()
    {
        AudioManager.Instance.StopVoice();
        StopAllCoroutines();
        StartCoroutine(IENUM_Next("next"));
    }


    public void BUT_Back()
    {
        AudioManager.Instance.StopVoice();
        StopAllCoroutines();
        StartCoroutine(IENUM_Next("back"));
    }


    IEnumerator IENUM_Next(string dir)
    {

        G_TransparentScreen.SetActive(true);
        ANIM_Speaker.SetTrigger("active");

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

        if (I_CurrentIndex < 4)
        {
            ANIM_Speaker.SetTrigger("active");
        }
        else
        {
            ANIM_Speaker.SetTrigger("inactive");
            yield break;
        }

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


    public void PlayWordSet1VO(int index) { AudioManager.Instance.PlayVoice(ACA_Words1[index]); }

    public void PlayWordSet2VO(int index) { AudioManager.Instance.PlayVoice(ACA_Words2[index]); }

    public void PlayWordSet3VO(int index) { AudioManager.Instance.PlayVoice(ACA_Words3[index]); }

    public void PlayWordSet4VO(int index) { AudioManager.Instance.PlayVoice(ACA_Words4[index]); }


    void OnDisable()
    {
        ANIM_Speaker.SetTrigger("inactive");
        AudioManager.Instance.StopVoice();
        AudioManager.Instance.StopSFX();
    }


}
