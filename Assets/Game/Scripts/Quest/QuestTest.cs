using System;
using UnityEngine;

public class QuestTest : MonoBehaviour
{
    public float speed;
    private bool startedQuest = false;
    private int currentPossession = 0;
    public NPCManager npcManager;
    public GameObject npcTransform;
    public Transform fleePosition;
    private bool atPosition = false;
    public ObjectiveManager objectiveManager;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            
        }
    }

     void Update()
    {
        if (currentPossession >= 1)
        {
            npcManager.transform.position = Vector3.MoveTowards(npcManager.transform.position,fleePosition.position, speed * Time.deltaTime);
            objectiveManager.TalkToRoy();

        }

        
    }

     void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Collision")
        {
            
            if (npcManager.transform.position == fleePosition.position)
            {
                currentPossession = 0;
                npcTransform.transform.Rotate(0, 90, 0);
                atPosition = true; 
                
                       // fleePosition.position = 0;
            }
           
                        //atPosition = false;
                    
        }
    }

    public void countUp()
    {
        Debug.Log("countUp");
        currentPossession++;
    }

    public void countDown()
    {
        currentPossession--;
        Debug.Log("countUp");
    }
    
}
