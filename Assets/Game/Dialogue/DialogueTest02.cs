using System;
using System.Collections;
using UnityEngine;
using TMPro;
public class DialogueTest02 : MonoBehaviour
{
   public TextMeshProUGUI textComponent;
   public string[] lines;
   public float textSpeed;

   private int index;
   
   public bool hasDialogue;

   void Update()
   {
      if (hasDialogue == true)
      {
         gameObject.SetActive(true);
         if (Input.GetMouseButtonDown(0))
         {
            if (textComponent.text == lines[index])
            {
               NextLine();
            }
            else
            {
               StopAllCoroutines();
               textComponent.text = lines[index];
              // hasDialogue = false;
            }
         }
      }

      if (index >= lines.Length)
      {
         StopAllCoroutines();
         gameObject.SetActive(false);
         textComponent.text = string.Empty;
         hasDialogue = false;
      }
   }
   void Start()
   {
      textComponent.text = string.Empty;
      hasDialogue = false;
      gameObject.SetActive(false);
      //StartDialogue();
   }

  public void StartDialogue()
   {
      gameObject.SetActive(true);
      index = 0;
      StartCoroutine(TypeLines());
   }

   IEnumerator TypeLines()
   {
      // Type each character 1 by 1
      foreach (char c in lines[index].ToCharArray())
      {
       textComponent.text += c;
       yield return new WaitForSeconds(textSpeed);
      }
   }

   void NextLine()
   {
      if (index < lines.Length - 1)
      {
         index++;
         textComponent.text = string.Empty;
         StartCoroutine(TypeLines());
      }
      else
      {
         gameObject.SetActive(false);
      }
   }
}
