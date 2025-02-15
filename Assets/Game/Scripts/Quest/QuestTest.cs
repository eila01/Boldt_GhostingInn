using System;
using UnityEngine;

public class QuestTest : MonoBehaviour
{
    private bool startedQuest = false;
    private int currentPossession = 0;
    public NPCManager npcManager;
    public GameObject npcTransform;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            
        }
    }

     void Update()
    {
        if (currentPossession >= 2)
        {
            npcManager.followPlayer();
        }
    }

    public void countUp()
    {
        currentPossession++;
    }

    public void countDown()
    {
        currentPossession--;
    }
    
}
