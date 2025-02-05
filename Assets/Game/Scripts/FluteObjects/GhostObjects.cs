using System;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using TMPro;

public class GhostObjects : MonoBehaviour
{
    // Ghost inhabitating objects
    // a NPC Ghost would inhabit in these objects and Flute is the only way to drag these ghosts out of their hiding spots
    // Once Flute enters the object (check if Flute Holds E) then spit out the NPC Ghost that will talk to Flute
    [SerializeField] GameObject npcGhost;
    [SerializeField] Light pointLight;
    [SerializeField] ParticleSystem particles;
    [SerializeField] TextMeshPro textMesh;
    public Transform spawnPoint;
    private Transform player;
    private FluteController activeFlute;

    private double timer = 0;
    public bool isPossessed;

    private void Start()
    {
        isPossessed = true; 
        activeFlute = GameObject.Find("Flute").GetComponent<FluteController>();
        textMesh = GameObject.Find("Text").GetComponent<TextMeshPro>();
        textMesh.text = "";
    }

    void Update()
    {
        
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Flute" && isPossessed == true)
        {
            textMesh.text = "E";// enable text
            // if flute enter collision then check if flute is holding down E
                if (Input.GetKey(KeyCode.E)) 
                {
                    timer += 0.1;
                    if (timer >= 5)
                    {
                        isPossessed = false;
                        SpawnGhostNPC();
                        if (isPossessed == false)
                        {
                            pointLight.range = 0;
                            particles.Stop();
                            textMesh.text = "";
            
                        }
                    }
                    
                }
                else if (timer < 5 && Input.GetKeyUp(KeyCode.E))
                {
                    timer = 0;
                } 
        }
        
            
            
            
            
            // if flute holds down E then turn down light intensity / turn off particles / get disable text / and spawn Ghost
        }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Flute" && isPossessed == true)
        {
            // off
            textMesh.text = "";
        }
    }

    public void SpawnGhostNPC()
    {
        // GameObject GhostNPC = Instantiate(npcGhost, spawnPoint.position, spawnPoint.rotation);
        Instantiate(npcGhost, spawnPoint.position, spawnPoint.rotation);
        timer = 0;
    }
    
    }


