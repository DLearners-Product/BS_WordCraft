using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InASentence : MonoBehaviour
{


    [SerializeField] private AudioClip AC_Click;
    [SerializeField] private AudioClip AC_Correct;
    [SerializeField] private AudioClip AC_Wrong;

    [Space(10)]

    [SerializeField] private GameObject[] GA_Questions;
    [SerializeField] private GameObject[] GA_Options;
    [SerializeField] private GameObject[] GA_Answers;
    [SerializeField] private GameObject G_Next;
    [SerializeField] private GameObject G_ActivityCompleted;




    private int I_CurrentIndex;




    void Start()
    {
        I_CurrentIndex = -1;
        BUT_Next();
    }


    public void BUT_Next() { StartCoroutine(IENUM_ShowNextQuestion()); }


    IEnumerator IENUM_ShowNextQuestion()
    {
        yield return null;
        G_Next.SetActive(false);

        if (I_CurrentIndex > -1)
        {

            GA_Options[I_CurrentIndex].SetActive(false);
            GA_Questions[I_CurrentIndex].SetActive(false);
        }

        I_CurrentIndex++;

        if (I_CurrentIndex >= GA_Questions.Length)
        {
            ShowActivityCompleted();
            yield break;
        }

        GA_Options[I_CurrentIndex].SetActive(true);
        GA_Questions[I_CurrentIndex].SetActive(true);
    }


    public void BUT_CorrectAnswer()
    {
        //audio feedback
        AudioManager.Instance.PlaySFX(AC_Correct);

        //visual feedback
        //TODO : particles
        GA_Answers[I_CurrentIndex].SetActive(true);

        //show next button
        G_Next.SetActive(true);


    }


    public void BUT_WrongAnswer()
    {
        AudioManager.Instance.PlaySFX(AC_Wrong);




    }




    private void ShowActivityCompleted()
    {
        G_ActivityCompleted.SetActive(true);
    }


}