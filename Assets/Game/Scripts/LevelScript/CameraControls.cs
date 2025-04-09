using UnityEngine;
using Image = UnityEngine.UI.Image;
using System.Collections;
public class CameraControls : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration;

    private void Start()
    {
        Color color = fadeImage.color;
        color.a = 0;
        fadeImage.color = color;
    }
    
    IEnumerator FadeInAndOut()
    {
        yield return StartCoroutine(FadeImage(fadeImage, 0, 1f, fadeDuration));
        yield return new WaitForSeconds((float)0.1);
        yield return StartCoroutine(FadeImage(fadeImage, 1, 0, fadeDuration));

    }
    
    IEnumerator FadeImage(Image image, float startOpacity, float targetOpacity, float duration)
    {
        float elapsedTime = 0;
        Color color = fadeImage.color;
        color.a = startOpacity;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startOpacity, targetOpacity, elapsedTime / duration);
            image.color = color;
            yield return null;
        }
        color.a = targetOpacity;
        image.color = color;
    }

   public void StartFadeInAndOut()
   {
       //fadeDuration = (float)0.1;
        StartCoroutine(FadeInAndOut());
    }
}
