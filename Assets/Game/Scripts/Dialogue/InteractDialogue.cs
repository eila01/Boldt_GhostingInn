using System;
using UnityEngine;
using Yarn.Unity;

namespace Yarn.Unity
{

public class InteractDialogue : MonoBehaviour
{
    public DialogueRunner dialogueRunner;  // Drag and drop your DialogueRunner here
            [SerializeField] String startNodeName = "";
            public DialogueController dialogueController;
            private int repeating = 1;
            [SerializeField] bool isRepeatable = false;
           // [SerializeField] Sprite interactSprite;
            void Start()
            {
               // interactSprite = Resources.Load<Sprite>("Interact");
                dialogueRunner = gameObject.GetComponent<DialogueRunner>();
            }
            private void OnTriggerStay(Collider other)
            {
                // Check if the player enters the trigger zone
                if ((other.CompareTag("Player") || other.gameObject.CompareTag("Player")))
                {
                   Debug.Log("Test Collider: " + other.gameObject.name);
                   
                    if (dialogueRunner != null && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)) && repeating > 0)
                    {
                         // Start the dialogue from the node in the Yarn script
                         Debug.Log(dialogueRunner.name + " In Test");
                         dialogueRunner.StartDialogue(startNodeName);
                         if (isRepeatable)
                         {
                             repeating = 1;
                         }
                         else
                         {
                             repeating = 0;
                         }
                    }
                   
                    //StartDialogue();
                }
            }
    
            
    
            private void StartDialogue()
            {
                // Make sure the dialogue runner is set and start the dialogue from the 'Start' node
                if (dialogueRunner != null)
                {
                    dialogueRunner.StartDialogue(startNodeName);  // Replace "Start" with the node you want to start from
                }
            }
}
}
