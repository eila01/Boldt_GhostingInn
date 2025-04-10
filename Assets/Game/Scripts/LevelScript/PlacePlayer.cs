using System;
using UnityEngine;

public class PlacePlayer : MonoBehaviour
{
    [SerializeField] Vector3 startPostion;
    [SerializeField] Vector3 placePosition;
    [SerializeField] Vector3 pushPosition;
    [SerializeField] GameController gameController;
   // private GameObject spawnedPlayer;
   [SerializeField] GameObject player;
   [SerializeField] GameObject flute;
   public bool haveFluteFollowing = false;
    private FluteController fluteController;
   private void Start()
   {
       gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
       fluteController = flute.GetComponent<FluteController>();
            gameController.startGame = false;
   }
    
   private void Awake()
    {
       // GameObject player =  GameObject.FindWithTag("Player");
        if (gameController.startGame == true)
        {
         //player.gameObject.SetActive(true);
         player.transform.position = placePosition;
         pushPlayer();
         if (fluteController.followingPlayer == true)
         {
             Instantiate(flute, placePosition, Quaternion.identity);
         }
        
        }else if (gameController.startGame == false)
        {
            player.transform.position = startPostion;
        }
        
    }

    private void Update()
    {
        
    }

    void spawnPlayer()
    {
        
    }
    private void pushPlayer()
    {
        GameObject player =  GameObject.FindWithTag("Player");
           
    }
}
