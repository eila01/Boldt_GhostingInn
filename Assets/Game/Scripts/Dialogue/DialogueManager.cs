using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    
    [Header("Main Objects")]
    public GameObject character;
    public Camera camera;
    public GameObject leftorRightObject;
    [Space]
    [Header("Text Box Objects")]
    public GameObject leftPointer;
    public GameObject rightPointer;
    public GameObject icon;
    public DialogueTrigger trigger;
    public Animator textBoxAnimator;
    public TextMeshProUGUI textBox;
    public float typingSpeed = 0.2f;
    public bool isDialogueActive = false;
    // show queue of lines
    private Queue<DialogueLine> lines;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        
    }
    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;

        textBoxAnimator.Play("A_TextOpen", 0);
        
        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
        DialogueLine dialogueLine = lines.Dequeue();
        
        StopAllCoroutines();
        
        StartCoroutine(TypeSentence(dialogueLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        textBox.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            textBox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        textBoxAnimator.Play("A_TextClose", 0);
    }
}
