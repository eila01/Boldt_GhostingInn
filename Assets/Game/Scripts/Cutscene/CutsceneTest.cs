using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
/*
 * NOTE: Try to Implement PlayerPrefs
 * Cutscenes needs to be played only once
 */
public class CutsceneTest : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public Camera cutsceneCam;
    public Image fadeImage;
    public float fadeDuration;
   // public Animator cutsceneAnimator;
   public GameObject cutsceneObj;
   public GameObject[] noncutsceneObj;
    private bool playCutsceneOnce = false;
[SerializeField] private AudioClip audioClip;
    

    void Start()
    {
        
        Color color = fadeImage.color; 
        color.a = 0;
        fadeImage.color = color;
        
        if (!playCutsceneOnce)
        {
            cutsceneCam.gameObject.SetActive(true);
            
            mainCamera.gameObject.SetActive(false);
            player.gameObject.SetActive(false);
           // playCutsceneOnce = PlayerPrefs.GetInt("CutscenePlayed", 0) == 1;
        }
        else if (PlayerPrefs.GetInt("CutscenePlayed", 1) == 1)
        {
            cutsceneCam.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !playCutsceneOnce)
        {
            
            StartCoroutine(FadeInAndOut());
            StartCoroutine(StopCutscene());
            playCutsceneOnce = true;
            // Save the cutscene state
            PlayerPrefs.SetInt("CutscenePlayed", 1);
            PlayerPrefs.Save(); // call immediately
            Debug.Log("play cutscene: " + playCutsceneOnce);
            
            
        }
    }
// Function to set all objects active or inactive
    public void SetAllObjectsActive(bool isActive)
    {
        foreach (GameObject obj in noncutsceneObj)
        {
            obj.SetActive(isActive);
        }
    }

    // Function to set a specific object active or inactive by index
    public void SetObjectActive(int index, bool isActive)
    {
        if (index >= 0 && index < noncutsceneObj.Length)
        {
            noncutsceneObj[index].SetActive(isActive);
        }
        else
        {
            Debug.LogWarning("Index out of range");
        }
    }
    IEnumerator FadeInAndOut()
    {
        //yield return StartCoroutine(FadeImage(fadeImage, 0, 1f, fadeDuration));
        yield return new WaitForSeconds(1);
        player.SetActive(false);
        SetAllObjectsActive(false);
        cutsceneCam.gameObject.SetActive(true);
        cutsceneObj.SetActive(true);
      //  playCutsceneOnce = true;
        //yield return StartCoroutine(FadeImage(fadeImage, 1, 0, fadeDuration));

    }
    IEnumerator StopCutscene()
    {
        yield return new WaitForSeconds(8);
        yield return StartCoroutine(FadeImage(fadeImage, 0, 1f, fadeDuration));
        yield return new WaitForSeconds(0.5f);
        SoundFXManager.instance.playSoundFXClip(audioClip, transform, 1f); // play any sfx
        yield return new WaitForSeconds(1);
         
        player.SetActive(true);
        SetAllObjectsActive(true);
        cutsceneCam.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
       cutsceneObj.SetActive(false);
      
        yield return StartCoroutine(FadeImage(fadeImage, 1, 0, fadeDuration));
        // Save the cutscene state
       // PlayerPrefs.SetInt("CutscenePlayed", 1);
       // PlayerPrefs.Save(); // call immediately
        // Debug.Log("play cutscene : " + playCutsceneOnce);
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
}
