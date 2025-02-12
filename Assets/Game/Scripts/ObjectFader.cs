using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    public float fadeSpeed, fadeAmount;
    float originalOpacity;
    Material[] Mat;
    public bool DoFade = false;
    void Start()
    {
     Mat = GetComponent<Renderer>().materials;
     foreach (Material mat in Mat)
     {
         originalOpacity = mat.color.a;
     }
    // originalOpacity = Mat.color.a;
    }

    void Update()
    {
        if (DoFade)
        {
            FadeNow();
        }
        else
        {
            ResetFade();
        }
    }

    void FadeNow()
    {
        foreach (Material mat in Mat)
        {
            Color currentColor = mat.color;
                 Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed * Time.deltaTime));
                 mat.color = smoothColor;
            
        }
        
    }

    void ResetFade()
    {
        foreach (Material mat in Mat)
        {
            Color currentColor = mat.color;
                    Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed));
                    mat.color = smoothColor;
        }
        
    }
}
