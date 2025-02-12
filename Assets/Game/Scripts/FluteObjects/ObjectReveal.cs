using System;
using UnityEngine;
[ExecuteInEditMode]
public class ObjectReveal : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] Light spotLight;

    private void Update()
    {
        if (mat && spotLight)
        {
            mat.SetVector("_LightPosition", spotLight.transform.position);
            mat.SetVector("_LightDirection", spotLight.transform.forward);
            // mat.SetFloat("MyLightIntensity", spotLight.intensity);
            mat.SetFloat("_LightAngle", spotLight.spotAngle);
        }
    }
}
