using System;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _mainMenuCamera;
    [SerializeField] CinemachineVirtualCamera _cutsceneCamera;
    [SerializeField] GameObject mainMenu;
    [SerializeField] RectTransform mainMenuContainer;
    [SerializeField] float startPosX, endPosX;
    [SerializeField] float tweenDuration;
    [SerializeField] CanvasGroup mainMenuCanvasGroup;
    
    public bool startGame = false;
    
    // Quit Game
    public void QuitGame()
    {
        Application.Quit();
    }
    
    // Start Game
    public void StartGame()
    {
        _mainMenuCamera.enabled = false;
        
        startGame = true;
        mainMenu.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("Game/Scene/Outside/Scene01_CCI_Outside");
    }

    public void Awake()
    {
        if (startGame == true)
        {
            mainMenu.SetActive(false);
        }
    }
}
