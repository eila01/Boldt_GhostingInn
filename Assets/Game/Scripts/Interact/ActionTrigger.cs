using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class ActionTrigger : MonoBehaviour
{
    [SerializeField] private SceneAction sceneAction = null;
    
    private Collider hitbox;
    private Vector3 hitpoint;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sceneAction.GetActionIcon();
        hitbox = GetComponent<Collider>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            hitpoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          
            if (hitbox.ClosestPoint(hitpoint) != default) //look at this
            {
                sceneAction.Interact();
                
                this.gameObject.SetActive(false);
            }
        }
    }
    
}
