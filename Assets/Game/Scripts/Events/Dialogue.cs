using System.Collections;
using UnityEngine;
using TMPro;
[System.Serializable]
public class Dialogue : Events
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

    public override void TriggerFunction()
    {
        // Debug.Log("Triggering Dialogue");
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
                rightPointer.SetActive(true);
                leftPointer.SetActive(false);
                trigger.StartCoroutine(SetPointerDirection(rightPointer.transform, character.transform));
            }

        }

        IEnumerator SetPointerDirection(Transform pointerT, Transform characterT)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public override void EndFunction()
    {
        
    }
}
