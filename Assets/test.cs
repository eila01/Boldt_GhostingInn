using System;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;
namespace Yarn.Unity
{



    public class test : MonoBehaviour
    {
        public DialogueRunner dialogueRunner;  // Drag and drop your DialogueRunner here
        [SerializeField] String startNodeName = "";
        // public DialogueController dialogueController;
        private int repeating = 1;
        [SerializeField] bool isRepeatable = false;
        public Outline outline;
        public LineView lineView;
       // [SerializeField] Sprite interactSprite;
        void Start()
        {
           
           // interactSprite = Resources.Load<Sprite>("Interact");
           // dialogueRunner = gameObject.GetComponent<DialogueRunner>();
             outline.enabled = false;
        }
        private void OnTriggerStay(Collider other)
        {
            // Check if the player enters the trigger zone
            if ((other.CompareTag("Player") || other.gameObject.CompareTag("Player")))
            {
               Debug.Log("Test Collider: " + other.gameObject.name);
               outline.enabled = true;
                if (dialogueRunner != null && (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonUp(0)) && repeating > 0)
                {
                    if (!dialogueRunner.IsDialogueRunning)
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

                    if (dialogueRunner.IsDialogueRunning)
                    {
                        lineView.OnContinueClicked();
                    }
                }
                
               
                //StartDialogue();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            
                outline.enabled = false;
            
        }
       private void EndDialogue()
       {
           if (dialogueRunner != null)
           {
               dialogueRunner.Stop();
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


