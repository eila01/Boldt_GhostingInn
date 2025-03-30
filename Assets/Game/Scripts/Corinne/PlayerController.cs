using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Yarn.Unity.Example
{
    

public class PlayerController : MonoBehaviour
{
    
    public Rigidbody rb;
    public float moveSpreed, jumpForce;
    
    public LayerMask whatIsGround, whatIsWall, whatIsCeiling;
    public Transform groundPoint;
    private bool isGrounded;
    
    private Vector2 moveInput;
    
    private EventTrigger currentTrigger;
    private NPCManager npcManager;
     public bool talk = false;
    
     private RoomCameraSwap roomCameraSwap;
     
     public PauseMenu pauseMenu;
     bool ispaused = false;
     
     private DialogueAdvanceInput dialogueInput;
     
     public float interactionRadius = 2.0f;
     
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTrigger = gameObject.GetComponent<EventTrigger>();
        npcManager = GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCManager>();
        dialogueInput = FindObjectOfType<DialogueAdvanceInput>();
        dialogueInput.enabled = false;
    }
    /// <summary>
    /// Draw the range at which we'll start talking to people.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        
        Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, new Vector3(1, 1, 1));

        // Need to draw at position zero because we set position in the line above
        Gizmos.DrawWireSphere(Vector3.zero, interactionRadius);
    }
    // Update is called once per frame
    void Update()
    {
        /*
        // Remove all player control when we're in dialogue
        if (FindObjectOfType<DialogueRunner>().IsDialogueRunning == true)
        {
            return;
        }

        // every time we LEAVE dialogue we have to make sure we disable the input again
        if (dialogueInput.enabled)
        {
            dialogueInput.enabled = false;
        }*/
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CheckForNearbyNPC();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Game/Scene/Outside/Scene01_CCI_MainMenu");
        }
        // restart level
        if (Input.GetKeyDown(KeyCode.R)) { //If you press R
            Application.LoadLevel (Application.loadedLevel);
        }
        // pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            
            ispaused = !ispaused;
            if (ispaused)
            {
                pauseMenu.Pause();
            }
            else
            {
                pauseMenu.Resume();
            }
        }
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
           
           
        
        moveInput.Normalize();
        
        rb.linearVelocity = new Vector3(moveInput.x * moveSpreed, rb.linearVelocity.y, moveInput.y * moveSpreed);

        RaycastHit hit;
        if (Physics.Raycast(groundPoint.position, Vector3.down, out hit, .3f, whatIsGround))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (!talk)
        {
           if (Input.GetButtonDown("Jump") && isGrounded)
           {
               rb.linearVelocity = new Vector3(0f, jumpForce, 0f);
           }  
        }

//        if (DialogueManager.Instance.isDialogueActive)
  //      {
            // stop player movement
            // horizontal = 0f;
            return;
    //    }
        /*
        if (currentTrigger != null)
        { 
            if (currentTrigger.triggerType == EventTrigger.TriggerType.PressButtonInTrigger &&
                currentTrigger.button == EventTrigger.whichButton.AButton && 
                Input.GetMouseButtonDown(0) &&
                Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Player press key");
                talk = true;
                currentTrigger.thisEvent.TriggerFunction();
            }

            if (currentTrigger.triggerType == EventTrigger.TriggerType.EnterTrigger && 
                Input.GetMouseButtonDown(0)&&
                Input.GetKeyDown(KeyCode.E))
            {
                talk = true;
                currentTrigger.thisEvent.TriggerFunction();
            }   
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == npcManager)
        {
            if (currentTrigger != null)
            { 
                if (currentTrigger.triggerType == EventTrigger.TriggerType.PressButtonInTrigger &&
                    currentTrigger.button == EventTrigger.whichButton.AButton && 
                    Input.GetMouseButtonDown(0) &&
                    Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Player is entering");
                    talk = true;
                    currentTrigger.thisEvent.TriggerFunction();
                }

                if (currentTrigger.triggerType == EventTrigger.TriggerType.EnterTrigger && 
                    Input.GetMouseButtonDown(0)&&
                    Input.GetKeyDown(KeyCode.E))
                {
                    talk = true;
                    currentTrigger.thisEvent.TriggerFunction();
                }   
            }
        }
    }
    public void CheckForNearbyNPC()
    {
        var allParticipants = new List<NPC>(FindObjectsOfType<NPC>());
        var target = allParticipants.Find(delegate (NPC p)
        {
            return string.IsNullOrEmpty(p.talkToNode) == false && // has a conversation node?
                   (p.transform.position - this.transform.position)// is in range?
                   .magnitude <= interactionRadius;
        });
        if (target != null)
        {
            // Kick off the dialogue at this node.
            FindObjectOfType<DialogueRunner>().StartDialogue(target.talkToNode);
            // reenabling the input on the dialogue
            dialogueInput.enabled = true;
        }
    }
}
}