using UnityEngine;
using UnityEngine.EventSystems;


public class MouseHoverAudio : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioClip clip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlayVoice(clip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AudioManager.Instance.StopVoice();
    }
}
