using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, bool> questAccepted = new Dictionary<string, bool>();
    private Dictionary<string, bool> questCompleted = new Dictionary<string, bool>();
    
    // Reference to current interaction
    private DialogueInteract currentDialogue;

    public void AcceptQuest(string questID)
    {
        questAccepted[questID] = true;
    }

    public void CompleteQuest(string questID)
    {
        questCompleted[questID] = true;
    }

    public bool IsQuestAccepted(string questID) => questAccepted.ContainsKey(questID) && questAccepted[questID];
    public bool IsQuestCompleted(string questID) => questCompleted.ContainsKey(questID) && questCompleted[questID];

    public void SetCurrentDialogue(DialogueInteract dialogue)
    {
        currentDialogue = dialogue;
    }
    public void TriggerAccept()
    {
        if (currentDialogue != null)
            currentDialogue.QuestAccepted();
    }
}
