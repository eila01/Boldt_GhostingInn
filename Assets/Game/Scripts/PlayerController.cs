using System;
using UnityEngine;
//UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    
    public Rigidbody rb;
    public float moveSpreed, jumpForce;
    
    public LayerMask whatIsGround, whatIsWall, whatIsCeiling;
    public Transform groundPoint;
    private bool isGrounded;
    
    private Vector2 moveInput;
    
    public EventTrigger currentTrigger;
    public NPCManager npcManager;
   public bool talk = false;
    
     private RoomCameraSwap roomCameraSwap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
//        roomCameraSwap.rightRotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) { //If you press R
            Application.LoadLevel (Application.loadedLevel);
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
        if (currentTrigger != null)
        { 
           if (currentTrigger.triggerType == EventTrigger.TriggerType.PressButtonInTrigger &&
               currentTrigger.button == EventTrigger.whichButton.AButton && 
               Input.GetMouseButtonDown(0) && 
               npcManager.playerEnter == true)
           {
               Debug.Log("Player is entering");
               talk = true;
               currentTrigger.thisEvent.TriggerFunction();
           }

           if (currentTrigger.triggerType == EventTrigger.TriggerType.EnterTrigger && 
               Input.GetMouseButtonDown(0) &&
               npcManager.playerEnter == true)
           {
               talk = true;
               currentTrigger.thisEvent.TriggerFunction();
           }   
        }

    }
}
