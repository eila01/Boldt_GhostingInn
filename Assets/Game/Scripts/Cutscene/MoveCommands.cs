using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

public class MoveCommands : MonoBehaviour
{
    public DialogueRunner runner;

    [System.Serializable]
    public class NamedObject
    {
        public string name;
        public GameObject target;
    }

    public List<NamedObject> objectsToControl = new List<NamedObject>();
    private Dictionary<string, GameObject> objectLookup;

    void Awake()
    {
        objectLookup = new Dictionary<string, GameObject>();
        foreach (var item in objectsToControl)
        {
            objectLookup[item.name] = item.target;
        }
    }
    void Start()
    {
        if (runner == null)
            runner = FindObjectOfType<DialogueRunner>();

        if (runner != null)
        {
            runner.AddCommandHandler<string>("showObject", ShowObject);
            runner.AddCommandHandler<string>("hideObject", HideObject);
        }
    }
    [YarnCommand("move")]
    public static IEnumerator MoveObject(string objectName, float x, float y, float z, float duration)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj == null) yield break;

        // Lock player movement if necessary
        PlayerController.canMove = false;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Temporarily make Rigidbody kinematic to avoid physics interference
        }

        Vector3 start = obj.transform.position;
        Vector3 target = obj.transform.position + new Vector3(x, y, z);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Vector3 newPos = Vector3.Lerp(start, target, elapsed / duration);

            // Update position with Rigidbody (physics-friendly movement)
            if (rb != null)
            {
                rb.MovePosition(newPos); // Physics-based movement
            }
            else
            {
                obj.transform.position = newPos; // Non-physics-based movement
            }

            yield return null;
        }

        // Final position after the movement is complete
        if (rb != null)
        {
            rb.MovePosition(target); // Ensure the final position is applied
            rb.isKinematic = false; // Restore Rigidbody physics interaction
        }
        else
        {
            obj.transform.position = target; // Non-physics-based final position
        }

        // Unlock player movement
        PlayerController.canMove = true;
    }
    
    // Show a GameObject by name
    void ShowObject(string objectName)
    {
        GameObject obj = FindInactiveObjectByName(objectName);
        if (obj != null)
        {
            obj.SetActive(true);
            Debug.Log($"[Yarn] Activated object: {objectName}");
        }
        else
        {
            Debug.LogWarning($"[Yarn] Object '{objectName}' not found.");
        }
    }

    void HideObject(string objectName)
    {
        GameObject obj = FindInactiveObjectByName(objectName);
        if (obj != null)
        {
            obj.SetActive(false);
            Debug.Log($"[Yarn] Deactivated object: {objectName}");
        }
        else
        {
            Debug.LogWarning($"[Yarn] Object '{objectName}' not found.");
        }
    }

    GameObject FindInactiveObjectByName(string name)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.name == name &&
                !go.hideFlags.HasFlag(HideFlags.NotEditable) &&
                !go.hideFlags.HasFlag(HideFlags.HideAndDontSave) &&
                go.scene.IsValid())
            {
                return go;
            }
        }
        return null;
    }

    void CameraFocus(GameObject target, Camera camera, CinemachineCamera cCamera)
    {
        cCamera.LookAt = target.transform;
    }
}
