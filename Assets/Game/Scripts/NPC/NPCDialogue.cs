using System;
using Unity.VisualScripting;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    private Transform player;
    public SpriteRenderer speechBubbleRenderer; // allows to turn the speech bubble off / on

    void Awake()
    {
        speechBubbleRenderer = transform.Find("SpeechBubble_01").GetComponent<SpriteRenderer>();
                speechBubbleRenderer.enabled = false; 
    }

    void Start()
    {
        
    }

    void OnTriggerStay(Collider collision)
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

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // off
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
