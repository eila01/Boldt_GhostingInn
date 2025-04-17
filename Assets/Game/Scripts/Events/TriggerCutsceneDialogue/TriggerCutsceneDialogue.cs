using System;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

namespace Yarn.Unity
{

    public class TriggerCutsceneDialogue : MonoBehaviour
    {
        [SerializeField] private DialogueRunner dialogueRunner;
        [SerializeField] private string startNodeName = "";

        [SerializeField] private bool isRepeatable = false;
        [SerializeField] private LineView lineView;

        private bool hasPlayed = false;

        void Start()
        {
            if (dialogueRunner == null)
                dialogueRunner = FindObjectOfType<DialogueRunner>();

            // Disable movement during dialogue
            dialogueRunner.onDialogueStart.AddListener(() => PlayerController.canMove = false);
            dialogueRunner.onDialogueComplete.AddListener(() => PlayerController.canMove = true);

            // Only set hasPlayed if the correct node finishes
            dialogueRunner.onNodeComplete.AddListener((string nodeName) =>
            {
                if (!isRepeatable && nodeName == startNodeName)
                {
                    hasPlayed = true;
                    Debug.Log($"Node {nodeName} completed and marked as played.");
                }
            });
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            // Prevent retriggering
            if (hasPlayed && !isRepeatable)
                return;

            // Start dialogue only if it's not running
            if (!dialogueRunner.IsDialogueRunning)
            {
                Debug.Log($"Triggering dialogue node: {startNodeName}");
                dialogueRunner.StartDialogue(startNodeName);
            }

            // Progress dialogue
            if (dialogueRunner.IsDialogueRunning &&
                (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonUp(0)))
            {
                lineView.OnContinueClicked();
            }
        }
    }
}