using System;
using Unity.VisualScripting;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    private Transform player;
    private Transform flute;
    [SerializeField] public FluteController activeFlute;
    public SpriteRenderer speechBubbleRenderer; // allows to turn the speech bubble off / on
    [SerializeField] private bool isGhost;
    private GameObject self;
    [SerializeField] public DialogueTest test;
    private int counter = 0;
    void Awake()
    {
      //  speechBubbleRenderer = transform.Find("SpeechBubble_01").GetComponent<SpriteRenderer>();
                speechBubbleRenderer.enabled = false;
                
    }

    void Start()
    {
        activeFlute = GameObject.Find("Flute").GetComponent<FluteController>();
       // test = GameObject.Find("DialogueTest").GetComponent<DialogueTest>();
    }

    void OnTriggerStay(Collider collision)
    {
        if (isGhost == true)
        {
            if (collision.gameObject.tag == "Player")
            {
                speechBubbleRenderer.enabled = true; // bubble on

                player = collision.gameObject.GetComponent<Transform>();
                // check where player is, then turn toward them
                if (player.position.x > transform.position.x && transform.parent.localScale.x < 0)
                {
                    Flip();
                }
                else if (player.position.x < transform.position.x && transform.parent.localScale.x > 0)
                {
                    Flip();
                }
            }
            else if (collision.gameObject.tag == "Flute" && activeFlute._isEnable)
            {
                speechBubbleRenderer.enabled = true; // bubble on

                flute = collision.gameObject.GetComponent<Transform>();
                if (flute.position.x > transform.position.x && transform.parent.localScale.x < 0)
                {
                    Flip();
                }
                else if (flute.position.x < transform.position.x && transform.parent.localScale.x > 0)
                {
                    Flip();
                }
            }
        }
        else if (isGhost == false)
        {
            if (collision.gameObject.tag == "Player")
            {
                
                speechBubbleRenderer.enabled = true; // bubble on
                
                player = collision.gameObject.GetComponent<Transform>();
                // check where player is, then turn toward them
                if (player.position.x > transform.position.x && transform.parent.localScale.x < 0)
                {
                    Flip();
                }
                else if (player.position.x < transform.position.x && transform.parent.localScale.x > 0)
                {
                    Flip();
                }
            }

        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
                test.hasDialogue = true;
                test.StartDialogue();
            
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // off
           // test.hasDialogue = false;
            speechBubbleRenderer.enabled = false;
        } else if (collision.gameObject.tag == "Flute" && activeFlute._isEnable)
        {
            speechBubbleRenderer.enabled = false;
        }
    }

    private void Flip()
    {
        
        Vector3 currentScale = transform.parent.localScale;
        currentScale.x *= -1; // flips value
        transform.parent.localScale = currentScale; // apply value
    }
}
