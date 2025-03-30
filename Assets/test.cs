using System;
using UnityEngine;
using Yarn.Unity;
namespace Yarn.Unity
{



    public class test : MonoBehaviour
    {
        public DialogueRunner dialogueRunner;  // Drag and drop your DialogueRunner here
        [SerializeField] String startNodeName = "";
        public DialogueController dialogueController;

        void Start()
        {
            dialogueRunner = gameObject.GetComponent<DialogueRunner>();
        }
        private void OnTriggerEnter(Collider other)
        {
            // Check if the player enters the trigger zone
            if (other.CompareTag("Player") || other.gameObject.CompareTag("Player"))
            {
               
                if (dialogueRunner != null)
                {
                     // Start the dialogue from the node in the Yarn script
                     
                     dialogueRunner.StartDialogue(startNodeName);
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


