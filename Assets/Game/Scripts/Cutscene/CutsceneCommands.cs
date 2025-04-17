using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using Yarn.Unity.Example;

public class CutsceneCommands : MonoBehaviour
{
    public PlayerController player;
    public Animator guardAnimator;
    public CameraController cameraManager;

    [YarnCommand("stopPlayer")]
    public void StopPlayer()
    {
        PlayerController.canMove = false;
    }

    [YarnCommand("playAnimation")]
    public void PlayAnimation(string objectName, string animName)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj != null)
        {
            Animator anim = obj.GetComponent<Animator>();
            if (anim != null)
                anim.Play(animName);
        }
    }
    [YarnCommand("playTimeline")]
    public void PlayTimeline(string timelineName)
    {
        TimelineManager.Instance.Play(timelineName);
    }
    [YarnCommand("stopTimeline")]
    public void StopTimeline(string timelineName)
    {
        TimelineManager.Instance.Stop(timelineName);
    }
    [YarnCommand("changeCamera")]
    public void ChangeCamera(string camName)
    {
        cameraManager.SwitchToCamera(camName);
    }

    [YarnCommand("endCutscene")]
    public void EndCutscene()
    {
        PlayerController.canMove = true;
        cameraManager.SwitchToCamera("MainCam");
    }

    [YarnCommand("wait")]
    public IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
    }
    
    public Image fadeImage; // Assign a full-screen UI CanvasGroup
    public GameObject[] characterPrefabs; // Assign your spawnable characters in the inspector

    // Fade Out
    [YarnCommand("fadeOut")]
    public IEnumerator FadeOut(float duration)
    {
        CutsceneCommands instance = FindObjectOfType<CutsceneCommands>();
        yield return StartCoroutine(FadeImage(fadeImage, 0, 1f, duration));
    }

    // Fade In
    [YarnCommand("fadeIn")]
    public IEnumerator FadeIn(float duration)
    {
        CutsceneCommands instance = FindObjectOfType<CutsceneCommands>();
        yield return StartCoroutine(FadeImage(fadeImage, 1f, 0, duration));
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

    // Spawn character by name at position
    [YarnCommand("spawnCharacter")]
    public static void SpawnCharacter(string characterName, float x, float y, float z)
    {
        CutsceneCommands instance = FindObjectOfType<CutsceneCommands>();

        GameObject prefab = System.Array.Find(instance.characterPrefabs,
            go => go.name == characterName);

        if (prefab != null)
        {
            Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
        }
        else
        {
            Debug.LogWarning($"Character prefab '{characterName}' not found.");
        }
    }
    [YarnCommand("hide_object")]
    public void HideObject(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj != null)
        {
            obj.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"GameObject named '{objectName}' not found.");
        }
    }

    [YarnCommand("show_object")]
    public void ShowObject(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj != null)
        {
            obj.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"GameObject named '{objectName}' not found.");
        }
    }
    void Awake()
    {
        var runner = FindObjectOfType<DialogueRunner>();
        if (runner != null)
        {
            runner.AddCommandHandler<string>("delete_object", DeleteObject);
        }
    }
    
    [YarnCommand("delete_object")]
    public void DeleteObject(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj != null)
        {
            Destroy(obj);
            Debug.Log($"Deleted object: {objectName}");
        }
        else
        {
            Debug.LogWarning($"GameObject '{objectName}' not found.");
        }
    }
}
