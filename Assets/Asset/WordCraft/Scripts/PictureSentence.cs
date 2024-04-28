using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PictureSentence : MonoBehaviour
{


    [SerializeField] private AudioClip[] ACA_Words;

    [Space(10)]

    [SerializeField] private Sprite[] SPRA_Images;

    [Space(10)]

    [SerializeField] private Image IMG_Placeholder;



    private int I_CurrentIndex;


    void Start()
    {
        I_CurrentIndex = 0;
    }


    public void BUT_Next()
    {
        I_CurrentIndex++;

        ShowImage();
    }


    public void BUT_Back()
    {
        I_CurrentIndex--;

        ShowImage();

    }


    private void ShowImage()
    {
        IMG_Placeholder.sprite = SPRA_Images[I_CurrentIndex];
    }




    public void BUT_Word(int index)
    {
        AudioManager.Instance.PlayVoice(ACA_Words[index]);
    }








}