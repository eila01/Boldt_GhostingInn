using System;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public string characterName = "";

    public string talkToNode = "";
    public Rigidbody rb;
    public Transform playerTarget;
    public float speed;
    public EventTrigger currentTrigger;
    public bool playerEnter;
    public void followPlayer()
    {
        
        float dist = Vector3.Distance(transform.position, playerTarget.position);
        // transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, speed * Time.deltaTime);
        if (dist <= 2)
        {
            rb.linearVelocity = Vector3.zero;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, speed * Time.deltaTime);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision enter");
        
        playerEnter = true;
        //throw new NotImplementedException();
    }

    private void OnCollisionExit(Collision other)
    {
        playerEnter = false;
    }
}
