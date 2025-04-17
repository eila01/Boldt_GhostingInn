using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Only if you're using TextMeshPro

public class TutorialPopup : MonoBehaviour
{
    public CanvasGroup canvasGroup;        // Drag the Canvas Group here
    public TextMeshProUGUI popupText;      // Or use `Text` if not TMP
    public float displayTime = 2f;         // How long to show
    public float fadeDuration = 0.5f;      // Fade in/out time

    public void ShowMessage(string message)
    {
        popupText.text = message;
        StartCoroutine(ShowPopup());
    }

    private IEnumerator ShowPopup()
    {
        gameObject.SetActive(true);

        // Fade in
        yield return FadeCanvas(0f, 1f);

        // Wait while showing
        yield return new WaitForSeconds(displayTime);

        // Fade out
        yield return FadeCanvas(1f, 0f);

        gameObject.SetActive(false);
    }

    private IEnumerator FadeCanvas(float from, float to)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = to;
    }
}