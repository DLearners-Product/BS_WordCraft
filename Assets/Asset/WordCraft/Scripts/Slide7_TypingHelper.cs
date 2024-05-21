using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Slide7_TypingHelper : MonoBehaviour
{

    [SerializeField] private TMP_InputField IF1;
    [SerializeField] private TMP_InputField IF2;



    void Start()
    {
        IF1.onValueChanged.AddListener(delegate { SwitchToIF2(); });

    }


    public void SwitchToIF2()
    {
        if (IF1.text.Length > 0)
        {
            IF2.ActivateInputField();
        }


    }








}
