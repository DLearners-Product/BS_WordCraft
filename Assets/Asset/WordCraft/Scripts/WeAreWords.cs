using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


public class WeAreWords : MonoBehaviour
{
    public float typingSpeed = 0.1f;
    public Color[] colors; // Array of colors for text animation
    public AnimationCurve scaleCurve; // Animation curve for scaling effect
    public AnimationCurve positionCurve; // Animation curve for position effect

    public TextMeshProUGUI textComponent;
    private string fullText;
    private Vector3 originalScale;

    void Start()
    {
        fullText = textComponent.text;
        textComponent.text = "";
        originalScale = transform.localScale;
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            textComponent.text += fullText[i];
            textComponent.color = colors[i % colors.Length]; // Cycle through colors array

            float scale = scaleCurve.Evaluate((float)i / fullText.Length); // Evaluate scale curve
            float positionOffset = positionCurve.Evaluate((float)i / fullText.Length); // Evaluate position curve

            // Apply scale and position offset
            transform.localScale = originalScale * scale;
            transform.localPosition += Vector3.up * positionOffset;

            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
