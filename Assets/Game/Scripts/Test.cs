using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Test : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpreed, jumpForce;
    
    public LayerMask whatIsGround, whatIsWall, whatIsCeiling;
    public Transform groundPoint;
    private bool isGrounded;
    
    private Vector2 moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector3(0f, jumpForce, 0f);
        }
    }
}
