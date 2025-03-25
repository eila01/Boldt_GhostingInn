using System;
using UnityEngine;
using Unity.Cinemachine;
public class startGame : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _mainMenuCamera;
    void Awake()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
        _mainMenuCamera.enabled = false;
    }

    void Start()
    {
        
    }
}
