using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class PopupTrigger : MonoBehaviour
{
    [SerializeField] private PopupTutorial popup;
      [SerializeField] private float seconds = 1f;
   public UnityEvent uEvent;
   public GameObject triggerObject;
private bool doOnce = false;
public bool isDoorLock = false;
public bool isOutside = true;
   

   public void OnTriggerEnter(Collider collider)
   {
      if (collider.gameObject.tag == "Player" && !doOnce)
      {
         doOnce = true;
         Debug.Log("Player entered trigger");
         if (!isDoorLock && !isOutside)
         {
            popup.switchTutorialText();
         }else if(isDoorLock && !isOutside)
         {
            popup.talkToRoyText();
         }
         popup.PauseTutorial();
         StartCoroutine(waitForTrigger());

      }
   }

   IEnumerator waitForTrigger()
   {
      yield return new WaitForSeconds(seconds);
      popup.Resume();
   }
}
