using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickAudio : MonoBehaviour, IPointerDownHandler
{
    public AudioClip clip;

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.Instance.PlayVoice(clip);
    }
}