using System;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    
    // Event Type 
    public enum EventType
    {
        Dialogue,
        Quest,
        Cutscene
    }

    public enum TriggerType
    {
        EnterTrigger,
        PressButtonInTrigger,
        ReleaseButtonInTrigger,
        ExitTrigger
    }

    public enum whichButton
    {
        AButton,
        BButton,
        XButton,
        YButton,
        ZButton
    }
    public EventType type; // see the options in EventType
    public TriggerType triggerType;
    public whichButton button; 
    public PlayerController machine;
    public Events thisEvent; // varies in different classes
    public Dialogue[] dialogue; // list of dialogues
    public int dialogueIndex;
    public PlayerController player;
    private void OnTriggerEnter(Collider other)
    {
        machine.currentTrigger = this;
        if (triggerType == TriggerType.EnterTrigger && other.tag == "Player")
        {
            Debug.Log("Player is enter");
            if(player.talk == true)
            {
                Debug.Log("Player is talking");
              thisEvent.TriggerFunction();  
            }
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (triggerType == TriggerType.ExitTrigger && other.gameObject.tag == "Player")
        {
            thisEvent.TriggerFunction();
            //triggerType = TriggerType.ExitTrigger;
        }
        if(other.gameObject.tag == "Player")
        {
            
            machine.currentTrigger = null;
        }
    }
    private void Update()
    {
        if (thisEvent == null)
        {
            SetThisEvent(type);
        }
    }

     void Start()
    {
        player = GetComponent<PlayerController>();
    }

    public void SetThisEvent(EventType eventType)
    {
        switch (eventType)
        {
            case EventType.Dialogue:
                thisEvent = dialogue[dialogueIndex];
                break;
        }
    }
}
