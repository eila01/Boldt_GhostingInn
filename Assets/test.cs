using System;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

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
       [SerializeField] AudioClip interactSoundClip;
       private bool hasPlayed = false; 
       [SerializeField] GameObject spriteInteract;

       void Start()
       {
           dialogueRunner = FindObjectOfType<DialogueRunner>();
           outline.enabled = false;
           dialogueRunner.onDialogueStart.AddListener(() => PlayerController.canMove = false);
           dialogueRunner.onDialogueComplete.AddListener(() => PlayerController.canMove = true);
           // This listener waits until this dialogue finishes
           dialogueRunner.onDialogueComplete.AddListener(() =>
           {
               if (!isRepeatable && hasPlayed)
               {
                   Debug.Log("Dialogue completed and is not repeatable.");
                   // Prevent replaying
                   hasPlayed = true;
               }
           });
       }

       private void OnTriggerEnter(Collider other)
       {
           if ((other.CompareTag("Player") || other.gameObject.CompareTag("Player")) && !hasPlayed){
               spriteInteract.SetActive(true);
               SoundFXManager.instance.playSoundFXClip(interactSoundClip, transform, 1f);
                
       }
    }

        private void OnTriggerStay(Collider other)
        {
            
            // Check if the player enters the trigger zone
            if ((other.CompareTag("Player") || other.gameObject.CompareTag("Player")))
            {
               Debug.Log("Test Collider: " + other.gameObject.name);
              if(isRepeatable)
               outline.enabled = true;
               // If the dialogue hasn't been played yet, or it's repeatable
               if (!dialogueRunner.IsDialogueRunning &&
                   (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonUp(0)) &&
                   (isRepeatable || !hasPlayed))
               {
                   spriteInteract.SetActive(false);
                   Debug.Log(dialogueRunner.name + " In Test — Starting node: " + startNodeName);
                   dialogueRunner.StartDialogue(startNodeName);
                   
                   hasPlayed = true;
               }

               // Progress the dialogue manually
               if (dialogueRunner.IsDialogueRunning &&
                   (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonUp(0)))
               {
                   lineView.OnContinueClicked();
               }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            spriteInteract.SetActive(false);
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


