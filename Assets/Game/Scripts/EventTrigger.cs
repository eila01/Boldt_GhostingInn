using System;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    // Event Type 
    public enum EventType
    {
        Dialogue
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            machine.currentTrigger = this;
            if (triggerType == TriggerType.EnterTrigger)
            {
                thisEvent.TriggerFunction();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
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
