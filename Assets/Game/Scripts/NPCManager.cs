using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class NPCManager : MonoBehaviour
{
    public string characterName = "";

    public string talkToNode = "";
    public Rigidbody rb;
    public Transform playerTarget;
    public float speed;
    public EventTrigger currentTrigger;
    private bool playerEnter;
    public bool isGhost;
    private bool idle = true;
    public float startPosY;
    public float startPosX;
    public float startPosZ;
   // public float posY;
    private float time;
    private float posValue;
    public float amp;
    public float freq;
    void Update()
    {
        if (isGhost == true)
        {
           // transform.position = new Vector3(startPosX, Mathf.Sin(Time.time * freq) * amp + startPosY, startPosZ);
        }
         //GhostFloatAnim();
    }
   
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

    private void GhostFloatAnim()
    {
        if (idle)
        {
            StartCoroutine(GhostHover());
        }
    }

    private void Flip()
    {
        
    }
    
    IEnumerator GhostHover()
    {
        // randomize time
        time = Random.Range(1.5f, 3.25f);
        // randomize position
        posValue = Random.Range(0.3f, 0.6f);
        
        yield return new WaitForSeconds(time);
        // Up
        transform.DOMove(transform.position + new Vector3(0, posValue, 0), 5.2f);
        yield return new WaitForSeconds(time);
        // OG Pos
        transform.DOMove(new Vector3(startPosX, startPosY, 0), 5.2f);
        yield return new WaitForSeconds(time);
        
        // Down
      //  transform.DOMove(transform.position - new Vector3(0,Mathf.Sin(Time.time)posValue, 0), 5.2f);
        yield return new WaitForSeconds(time);
        // OG Pos
        transform.DOMove( new Vector3(startPosX, startPosY, 0), 5.2f);
        yield return new WaitForSeconds(time);
    }
    IEnumerator NPCMovement()
    {
       yield return new WaitForSeconds(1);
    }
}
