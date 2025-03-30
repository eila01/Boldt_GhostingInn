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
            Debug.unityLogger.Log("Dialogue Active");
            dialogueRunner.Dialogue.Continue();  // This will continue to the next line
        }
    }

    public void StartDialogue(string startingNode)
    {
        dialogueRunner.StartDialogue(startingNode);
        isDialogueActive = true;
    }
}
