using System;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class TVChangeColor : MonoBehaviour
{
   public GameObject tvScreen;
    public Material material;
    public Color originalColor;
    public Color color;
    public Color secondColor;
    public SpriteRenderer noise;

    void Update()
    {
       /*
        //material.color = color;
        if (tvOn)
        {
            material.color = Color.Lerp(material.color, color, Time.deltaTime);
        }else if (tvOn)
        {
           material.color = originalColor;
        }
       */
    }

    
    
    [SerializeField] private bool tvOn;
    
       [SerializeField] private UnityEvent tvOnEvent;
       [SerializeField] private UnityEvent tvOffEvent;
    
       //[SerializeField] private GhostObjects ghostObjects;
      // private double timer = 0;
       [SerializeField] TextMeshPro textMesh;
     //  [SerializeField] Light pointLight;
       [SerializeField] Outline outline;
       [SerializeField] public FluteController activeFlute;
       //[SerializeField] float lightIntensity;
    
       void Start()
       {
          noise.enabled = false;
          outline.enabled = false;
          activeFlute = GameObject.Find("Flute").GetComponent<FluteController>();
          textMesh.text = "";
          tvScreen.GetComponent<Material>().color = material.color;
       }
    
       void Awake()
       {
          if (tvOn == true)
          {
             //pointLight.intensity = lightIntensity;
          }
          else
          {
             //pointLight.intensity = 0;
          }
       }
    
       public void InteractSwitch()
       {
          if (!tvOn)
          {
             tvOn = true;
             tvOnEvent.Invoke();
          }
          else
          {
             tvOn = false;
             tvOffEvent.Invoke();
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
                   
                   if (tvOn == true)
                   {
                      noise.enabled = false;
                      material.color = originalColor;
                      tvOn = false;
                    //  pointLight.intensity = 0;
                     // timer = 0;
    
                   }
    
                   else if (tvOn == false)
                   {
                      noise.enabled = true;
                      material.color = Color.Lerp(color, secondColor, Time.deltaTime);
                      tvOn = true;
                    //  pointLight.intensity = lightIntensity;
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
