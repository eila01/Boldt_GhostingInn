using UnityEngine;
using Yarn.Unity;

public class DialogueSetup : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public CutsceneCommands visibilityCommands;
    
    void Start()
    {
        if (dialogueRunner == null)
            dialogueRunner = FindObjectOfType<DialogueRunner>();

        if (visibilityCommands == null)
            visibilityCommands = FindObjectOfType<CutsceneCommands>();

        dialogueRunner.AddCommandHandler<string>("hide_object", visibilityCommands.HideObject);
        dialogueRunner.AddCommandHandler<string>("show_object", visibilityCommands.ShowObject);
    }
}