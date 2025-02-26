using System;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.UIElements;

public class RoomCameraSwap : MonoBehaviour
{
    public CinemachineVirtualCamera roomCamera;
    public CinemachineVirtualCamera primaryCamera;
   [SerializeField] private PlayerSwitch playerSwitch;
   [SerializeField] private CameraSwitch cameraSwitch;
   public GameObject corinne;
   public GameObject flute;
   public GameObject npc;

  [SerializeField] public bool rightRotate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //switchToCamera(primaryCamera);
        rightRotate = false;
        npc.transform.Rotate(0, -90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player" && (cameraSwitch.Manager == 1) && 
            (playerSwitch.player1Active == true))
        {
            rightRotate = true;
            corinne.transform.Rotate(0, -90, 0);
            npc.transform.Rotate(0, -90, 0);
            switchToCamera(roomCamera);
            roomCamera.Follow = corinne.transform;
            roomCamera.LookAt = corinne.transform;
        }
        else if (other.gameObject.tag == "Flute" &&(cameraSwitch.Manager == 0) &&
                 (playerSwitch.player1Active == false))
        {
            rightRotate = true;
            flute.transform.Rotate(0, -90 , 0);
            npc.transform.Rotate(0, -90, 0);
            switchToCamera(roomCamera);
            roomCamera.Follow = flute.transform;
           roomCamera.LookAt = flute.transform;
            
        }
        Debug.Log("OnTriggerEnter");
    }

    private void OnTriggerExit(Collider other)
    {
        rightRotate = false;
        Debug.Log("OnTriggerExit");
        if (other.gameObject.tag == "Player" && (cameraSwitch.Manager == 1) && 
            (playerSwitch.player1Active == true))
        {
            rightRotate = false;
            corinne.transform.Rotate(0, 90, 0);
            npc.transform.Rotate(0, 90, 0);
            switchToCamera(primaryCamera);
            primaryCamera.Follow = corinne.transform;
            primaryCamera.LookAt = corinne.transform;
            
        }
        else if (other.gameObject.tag == "Flute" && (cameraSwitch.Manager == 0) &&
                 (playerSwitch.player1Active == false))
        {
            rightRotate = false;
            flute.transform.Rotate(0, 90, 0);
            npc.transform.Rotate(0, 90, 0);
            switchToCamera(primaryCamera);
            primaryCamera.Follow = flute.transform;
            primaryCamera.LookAt = flute.transform;
        }
    }

    void switchToCamera(CinemachineVirtualCamera targetCamera)
    {
        primaryCamera.gameObject.SetActive(false);
        targetCamera.gameObject.SetActive(true);
    }

    
}
