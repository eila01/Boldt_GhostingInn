using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class CamreralWallFader : MonoBehaviour
{
    public Transform target;
    public LayerMask wallMask;
    public float fadeSpeed = 5f;
    private float hiddenFade = 0f;
    private float visibleFade = 1f;

    private Dictionary<Renderer, Coroutine> fadeCoroutines = new Dictionary<Renderer, Coroutine>();
    private HashSet<Renderer> fadedWalls = new HashSet<Renderer>();

    void Update()
    {
        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;

        Ray ray = new Ray(transform.position, direction);
        RaycastHit[] hits = Physics.RaycastAll(ray, distance, wallMask);

        // Walls hit this frame
        HashSet<Renderer> wallsHit = new HashSet<Renderer>();

        foreach (RaycastHit hit in hits)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend == null) continue;

            wallsHit.Add(rend);

            if (!fadedWalls.Contains(rend))
            {
                StartFade(rend, hiddenFade);
                fadedWalls.Add(rend);
            }
        }

        // Fade back any walls that were previously faded but are no longer hit
        List<Renderer> toUnfade = new List<Renderer>();

        foreach (Renderer rend in fadedWalls)
        {
            if (!wallsHit.Contains(rend))
            {
                StartFade(rend, visibleFade);
                toUnfade.Add(rend);
            }
        }

        // Clean up
        foreach (Renderer rend in toUnfade)
        {
            fadedWalls.Remove(rend);
        }
    }

    void StartFade(Renderer rend, float targetFade)
    {
        if (fadeCoroutines.TryGetValue(rend, out Coroutine running))
        {
            if (running != null)
            {
                StopCoroutine(running);
            }
        }

        Coroutine c = StartCoroutine(FadeTo(rend, targetFade));
        fadeCoroutines[rend] = c;
    }

    private IEnumerator FadeTo(Renderer rend, float targetFade)
    {
        foreach (Material mat in rend.materials)
        {
            if (!mat.HasFloat("_Fade")) continue;

            float current = mat.GetFloat("_Fade");
            while (Mathf.Abs(current - targetFade) > 0.01f)
            {
                current = Mathf.Lerp(current, targetFade, Time.deltaTime * fadeSpeed);
                mat.SetFloat("_Fade", current);
                yield return new WaitForEndOfFrame();
            }

            mat.SetFloat("_Fade", targetFade);
        }

        fadeCoroutines.Remove(rend);
    }
}

