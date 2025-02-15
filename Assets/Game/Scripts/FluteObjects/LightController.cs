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

   //[SerializeField] private GhostObjects ghostObjects;
  // private double timer = 0;
   [SerializeField] TextMeshPro textMesh;
   [SerializeField] Light pointLight;
   [SerializeField] Outline outline;
   [SerializeField] public FluteController activeFlute;
   

   void Start()
   {
      outline.enabled = false;
      activeFlute = GameObject.Find("Flute").GetComponent<FluteController>();
      textMesh.text = "";

   }

   void Awake()
   {
      if (lightOn == true)
      {
         pointLight.intensity = 6;
      }
      else
      {
         pointLight.intensity = 0;
      }
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

   private void OnTriggerStay(Collider collision)
   {
      {
         if (collision.gameObject.tag == "Flute" && activeFlute._isEnable)
         {
            textMesh.text = "E";
            outline.enabled = true;
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
            {
               
               if (lightOn == true)
               {
                  lightOn = false;
                  pointLight.intensity = 0;
                 // timer = 0;

               }

               else if (lightOn == false)
               {
                  lightOn = true;
                  pointLight.intensity = 6;
                  //timer = 0;
               }


            }

         }
         
      }
   }




   private void OnTriggerExit(Collider collision)
   {
      if (collision.gameObject.tag == "Flute" && activeFlute._isEnable)
      {
         outline.enabled = false;
         textMesh.text = "";
      }
   }
   
   
   
}
