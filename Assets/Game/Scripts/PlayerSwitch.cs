using System;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    // Ref PlayerController
    public PlayerController playerController;
    public FluteController player2Controller; // ref Flute Controller (maybe create FluteFollow script )
    // keep track which player is active
    public bool player1Active = true; 
    public bool player2Active;

    void Update()
    {
        // switch player once RIGHT SHIFT is pressed
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            SwitchPlayer(); 
        }
    }

    public void SwitchPlayer()
    {
        if (player1Active)
        {
            playerController.enabled = false; // turn Off the player 1 controller
            player2Controller.enabled = true; // turn on pFlute
            player1Active = false; 
        }
        else
        {
            playerController.enabled = true; // turn On the player 1 controller
            player2Controller.enabled = false; // turn of pFlute
            player1Active = true; 
        }
    }
}
