using System;
using UnityEngine;

public class PlacePlayer : MonoBehaviour
{
    [SerializeField] Vector3 placePosition;
    [SerializeField] Vector3 pushPosition;
   // private GameObject spawnedPlayer;
    private void Awake()
    {
        GameObject player =  GameObject.FindWithTag("Player");
        player.transform.position = placePosition;
        pushPlayer();
    }

    private void pushPlayer()
    {
        GameObject player =  GameObject.FindWithTag("Player");
           
    }
}
