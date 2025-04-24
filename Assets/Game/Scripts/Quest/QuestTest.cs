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
    public PopupTutorial popupTutorial;
    public Door door;
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
           if (npcManager.transform.position == fleePosition.position)
           {
               currentPossession = 0;
           }
           // objectiveManager.TalkToRoy();
          //  popupTutorial.talkToRoyText();
          // popupTutorial.PauseTutorial();
        //  currentPossession = 0;
        }

        
    }

     void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Collision")
        {
            
            if (npcManager.transform.position == fleePosition.position)
            {
                currentPossession = 0;
               // npcTransform.transform.Rotate(0, 90, 0);
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
       // npcManager.transform.position = Vector3.MoveTowards(npcManager.transform.position,fleePosition.position, speed * Time.deltaTime);
        objectiveManager.TalkToRoy();
        popupTutorial.talkToRoyText();
        popupTutorial.PauseTutorial();
        door.Open(npcManager.transform.position);
        currentPossession = 0;
        // npcManager.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    public void countDown()
    {
        currentPossession--;
        Debug.Log("countUp");
    }
    
}
