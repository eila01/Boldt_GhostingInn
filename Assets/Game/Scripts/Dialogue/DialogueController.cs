using UnityEngine;
using Yarn.Unity;
public class DialogueController : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    private bool isDialogueActive = false;

    void Update()
    {
        if (isDialogueActive && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)))  // Left-click to progress
        {
            Debug.Log("Dialogue Active in Dialogue Controller");
            dialogueRunner.Dialogue.Continue();  // This will continue to the next line
        }
    }

    public void StartDialogue(string startingNode)
    {
        Debug.Log("Starting Dialogue in Dialogue Controller");
        dialogueRunner.StartDialogue(startingNode);
        isDialogueActive = true;
    }
}
