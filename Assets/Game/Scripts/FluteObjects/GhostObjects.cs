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
    private PointLight pointLight;
    [SerializeField] ParticleSystem particles;
    [SerializeField] TextMeshPro textMesh;
    public Transform spawnPoint;
    private Transform player;

    void Update()
    {

    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Flute")
        {
            // enable text
            
            // if flute enter collision then check if flute is holding down E
            
            // if flute holds down E then turn down light intensity / turn off particles / get disable text / and spawn Ghost
        }
    }
}

