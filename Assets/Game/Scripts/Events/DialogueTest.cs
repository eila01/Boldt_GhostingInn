using System.Collections;
//using Ink.UnityIntegration;
using UnityEngine;
using TMPro;
[System.Serializable]
public class DialogueTest : Events
{
    [Header("Main Objects")]
    public GameObject character;
    public Camera camera;
    public GameObject leftorRightObject;
    [Space]
    [Header("Text Box Objects")]
    public GameObject leftPointer;
    public GameObject rightPointer;
    public GameObject icon;
    public EventTrigger trigger;
    public Animator textBoxAnimator;
    public TextMeshProUGUI textBox;
    int sentenceIndex;
    bool displayingCharacters;
    bool finishSentenceEarly;
    bool closingSentence;
    [Space]
    public Sentence[] sentences;

    // ReSharper disable Unity.PerformanceAnalysis
    public override void TriggerFunction()
    {
        Debug.Log("Triggering Dialogue");
        if (sentenceIndex == 0 && !displayingCharacters)
        {
            // lock the player movement
            textBoxAnimator.Play("A_TextOpen", 0);
            float distance = Vector3.Distance(camera.transform.position, character.transform.position);
            leftorRightObject.transform.position = camera.transform.position + (distance * camera.transform.forward);
            Vector3 A = character.transform.position - camera.transform.position;
            Vector3 B = leftPointer.transform.position - camera.transform.position;
            float angle = Vector3.SignedAngle(A, B, camera.transform.up);
            if (angle < 0)
            {
                rightPointer.SetActive(false);
                leftPointer.SetActive(true);
                trigger.StartCoroutine(SetPointerDirection(rightPointer.transform, character.transform));
            }
            else
            {
                rightPointer.SetActive(true);
                leftPointer.SetActive(false);
                trigger.StartCoroutine(SetPointerDirection(leftPointer.transform, character.transform));
            }
            
            trigger.StartCoroutine(DisplaySentence(sentenceIndex));
        }
        else if (sentenceIndex < sentences.Length && !displayingCharacters && !closingSentence)
        {
            // if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
                trigger.StartCoroutine(DisplaySentence(sentenceIndex)); // display next sentence 
            
            
        }
        else if (displayingCharacters)
        {
            finishSentenceEarly = true; 
            
        }
        else if (!closingSentence)
        {
            trigger.StartCoroutine(CloseSentence());
        }

        IEnumerator SetPointerDirection(Transform pointerT, Transform characterT)
        {
            yield return new WaitForEndOfFrame(); // takes a frame to play animation
            while (textBoxAnimator.GetCurrentAnimatorStateInfo(0).IsName("A_TextOpen"))
            {
                // from character to the pointer
                pointerT.up = pointerT.position - camera.WorldToScreenPoint(characterT.position);
                yield return null;
            }
        }
    }

    public override void EndFunction()
    {
        // allow player to move
    }

    IEnumerator DisplaySentence(int currentSentence)
    {
        icon.SetActive(false);
        displayingCharacters = true;
        textBox.text = "";
        yield return new WaitForSeconds(0.5f);
        char[] characters = sentences[currentSentence].text.ToCharArray();
        for (int i = 0; i < characters.Length; i++)
        {
            textBox.text += characters[i];
            yield return new WaitForSeconds(1/sentences[currentSentence].speed);
            if (finishSentenceEarly)
            { 
                textBox.text = sentences[currentSentence].text;
                i = characters.Length;
                finishSentenceEarly = false;
                yield return null;
            }
        }

        
        sentenceIndex++;
        displayingCharacters = false; // finish sentence
        icon.SetActive(true); // display icon 
    }

    IEnumerator CloseSentence()
    {
        closingSentence = true;
        textBoxAnimator.Play("A_TextClosed", 0); // takes a frame
        icon.SetActive(false);
        yield return new WaitForEndOfFrame();
        while (textBoxAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }
        // sentenceIndex = 0; // talk to NPC again
        if (trigger.dialogueIndex + 1 < trigger.dialogue.Length)
        {
            
            trigger.dialogueIndex++;
            trigger.SetThisEvent(trigger.type); // set next trigger
            trigger.thisEvent.TriggerFunction();
        }
        else
        {
            // if there is no dialogue
            trigger.dialogueIndex = 0;
            trigger.SetThisEvent(trigger.type);
            EndFunction();
        }
        closingSentence = false;
    }
}

[System.Serializable]
public class Sentence
{
    [TextArea()]
    public string text;
    public float speed;
}