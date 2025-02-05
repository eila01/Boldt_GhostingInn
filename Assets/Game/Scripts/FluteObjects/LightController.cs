using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class LightController : MonoBehaviour
{
   [SerializeField] private bool lightOn;
   
   [SerializeField] private UnityEvent lightOnEvent;
   [SerializeField] private UnityEvent lightOffEvent;
   
   [SerializeField] private GhostObjects ghostObjects;
   private double timer = 0;
   [SerializeField] TextMeshPro textMesh;

    void Start()
   {
      // ghostObjects = GameObject.FindGameObjectWithTag("GhostsObj").GetComponent<GhostObjects>();
   }

   public void InteractSwitch()
   {
      if (!lightOn)
      {
         lightOn = true;
         lightOnEvent.Invoke();
      }
      else
      {
         lightOn = false;
         lightOffEvent.Invoke();
      }
   }
   
  private void OnTriggerStay(Collider collision){
   {
      if (collision.gameObject.tag == "Flute")
      {
         if (Input.GetKey(KeyCode.E)) 
         {
            timer += 0.1;
            if (timer >= 5)
            {
             //  isPossessed = false;
             //  SpawnGhostNPC();
               if (lightOn == true)
               {
                  
                  textMesh.text = "";
            
               }
            }
                    
         }
         else if (timer < 5 && Input.GetKeyUp(KeyCode.E))
         {
            timer = 0;
         } 
      }
   }
   
   }
   
   
   
}
