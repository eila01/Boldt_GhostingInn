using System;
using UnityEngine;

public class PlacePlayer : MonoBehaviour
{
    [SerializeField]  Vector3 startPostion;
    [SerializeField] Vector3 placePosition;
    [SerializeField] Vector3 pushPosition;
    [SerializeField] GameController gameController;
   // private GameObject spawnedPlayer;

   private void Start()
   {
       gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            gameController.startGame = false;
   }
    
   private void Awake()
    {
        GameObject player =  GameObject.FindWithTag("Player");
        if (gameController.startGame == true)
        {
        
        player.transform.position = placePosition;
        pushPlayer();
        
        }
        // game start
        player.transform.position = startPostion;
    }

    private void Update()
    {
        
    }

    private void pushPlayer()
    {
        GameObject player =  GameObject.FindWithTag("Player");
           
    }
}
