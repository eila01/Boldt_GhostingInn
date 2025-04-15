using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity.Example;

public class EnterNewScene : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] GameObject spriteInteract;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.gameObject.tag == "Player")
        {
            
            spriteInteract.SetActive(true);
          //  SceneManager.LoadScene(sceneName);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" || other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
            {
                Debug.Log("loaded scene");
                SceneManager.LoadScene(sceneName);
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.gameObject.tag == "Player")
        {
            spriteInteract.SetActive(false);
        }
    }
}
