using System;
using UnityEngine;

public class PlacePlayer : MonoBehaviour
{
    [SerializeField] Vector3 placePosition;
   // private GameObject spawnedPlayer;
    private void Awake()
    {
        GameObject player =  GameObject.FindWithTag("Player");
        player.transform.position = placePosition;
    }
}
