using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

using Yarn.Unity;

public class CinemachineCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform defaultTarget;

    private Transform currentTarget;
    private GameObject corinne;
    private GameObject flute;
   private PlayerSwitch playerSwitch;
 
   void Awake()
   {
       DialogueRunner runner = FindObjectOfType<DialogueRunner>();

       if (runner != null)
       {
           Debug.Log("Registering look_at and revert_camera commands.");
           runner.AddCommandHandler<string>("camera_look_at", LookAtCharacter);
           runner.AddCommandHandler("camera_revert", RevertCameraCoroutine);
       }
       else
       {
           Debug.LogError("DialogueRunner not found in scene!");
       }
       playerSwitch = FindObjectOfType<PlayerSwitch>();  // Update this line to use FindObjectOfType or direct reference
       flute = GameObject.FindGameObjectWithTag("Flute");
       corinne = GameObject.FindGameObjectWithTag("Player");
        
       if (virtualCamera != null && defaultTarget != null)
       {
           if (playerSwitch != null)
           {
               UpdateDefaultTarget();
           }
            
           /*
           virtualCamera.Follow = defaultTarget;
           virtualCamera.LookAt = defaultTarget;*/
           currentTarget = defaultTarget;
       }
   }
    void Start()
    {
        
        
    }

    [YarnCommand("look_at")]
    public void LookAtCharacter(string characterName)
    {
        Debug.Log($"YarnCommand look_at called with: {characterName}");
        GameObject target = GameObject.Find(characterName);
        
        if (target == null)
        {
            Debug.LogWarning($"No GameObject named '{characterName}' was found in the scene.");
        }
        if (target != null && virtualCamera != null)
        {
            virtualCamera.Follow = target.transform;
            virtualCamera.LookAt = target.transform;
            currentTarget = target.transform;
        }
    }

    [YarnCommand("revert_camera")]
    public void RevertCamera()
    {
        Debug.Log("Reverting camera...");

        if (playerSwitch == null)
        {
            Debug.LogError("Missing reference: playerSwitch");
        }
        if (flute == null)
        {
            Debug.LogError("Missing reference: flute");
        }
        if (corinne == null)
        {
            Debug.LogError("Missing reference: corinne");
        }
        if (virtualCamera == null)
        {
            Debug.LogError("Missing reference: virtualCamera");
        }

        if (playerSwitch == null || flute == null || corinne == null || virtualCamera == null)
        {
            return;  // Do nothing if there's a missing reference
        }

        if (!playerSwitch.player1Active)
            defaultTarget = flute.transform;
        else
            defaultTarget = corinne.transform;

        virtualCamera.Follow = defaultTarget;
        virtualCamera.LookAt = defaultTarget;
        currentTarget = defaultTarget;

        Debug.Log($"✅ Camera reverted to {defaultTarget.name}");
    }
    private void UpdateDefaultTarget()
    {
        defaultTarget = playerSwitch.player1Active ? corinne.transform : flute.transform;
        virtualCamera.Follow = defaultTarget;
        virtualCamera.LookAt = defaultTarget;
        currentTarget = defaultTarget;
    }
    
    private IEnumerator RevertCameraCoroutine()
    {
        RevertCamera(); // Your existing logic
        yield break;    // No wait, just completes immediately
    }
}
