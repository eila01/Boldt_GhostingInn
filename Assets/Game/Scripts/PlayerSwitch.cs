using System;
using UnityEngine;
using Yarn.Unity.Example;
using Object = UnityEngine.Object;

public class PlayerSwitch : MonoBehaviour
{
    // Ref PlayerController
    public PlayerController playerController;
    public FluteController player2Controller; // ref Flute Controller (maybe create FluteFollow script )
    // keep track which player is active
    public bool player1Active = true; 
    public bool player2Active = false;

    [SerializeField] public FluteController activeFlute;
    [SerializeField] private CameraSwitch cameraSwitch;
    [SerializeField] private AudioClip interactSoundClip;

    private void Start()
    {
        activeFlute = GameObject.Find("Flute").GetComponent<FluteController>();
        //cameraSwitch = GameObject.Find("CameraSwitch").GetComponent<CameraSwitch>();
    }

    void Update()
    {
        // switch player once RIGHT SHIFT is pressed
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            SwitchPlayer(); 
        }

        if (player2Active == true)
        {
            activeFlute._isEnable = true;
           
        }
        else
        {
            activeFlute._isEnable = false;
            
        }
    }

    public void SwitchPlayer()
    {
        if (player1Active)
        {
            playerController.enabled = false; // turn Off the player 1 controller
            player2Controller.enabled = true; // turn on pFlute
            player1Active = false; 
            player2Active = true; 
            cameraSwitch.Manager = 1;
            cameraSwitch.ManageCamera();
        }
        else
        {
            playerController.enabled = true; // turn On the player 1 controller
            //player2Controller.enabled = false; // turn off pFlute
            player1Active = true; 
            player2Active = false;
            cameraSwitch.Manager = 0;
            cameraSwitch.ManageCamera();
        }
        SoundFXManager.instance.playSoundFXClip(interactSoundClip, transform, 0.05f);

    }
    public void APressedFunction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }
}
