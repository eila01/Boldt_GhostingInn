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
    
    private EventTrigger currentTrigger;
    private NPCManager npcManager;
     public bool talk = false;
    
     private RoomCameraSwap roomCameraSwap;
     
     public PauseMenu pauseMenu;
     bool ispaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTrigger = gameObject.GetComponent<EventTrigger>();
        npcManager = GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCManager>();

    }

    // Update is called once per frame
    void Update()
    {
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
}
