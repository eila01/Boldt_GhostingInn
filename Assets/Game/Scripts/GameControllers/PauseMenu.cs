using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] RectTransform pauseMenuContainer;
    [SerializeField] float startPosX, endPosX;
    [SerializeField] float tweenDuration;
    [SerializeField] CanvasGroup pauseMenuCanvasGroup;
    public void Home()
    {
        // SceneController.Instance.OpenScene("Main Menu")
        Time.timeScale = 1;
    }

    public void Restart()
    {
        // SceneController.Instance.RestartLevel();
        Time.timeScale = 1;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        PausePanelIntro();
    }

    public async void Resume()
    {
        await PausePanelOutro();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    void PausePanelIntro()
    {
        pauseMenuCanvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
        pauseMenuContainer.DOAnchorPosX(endPosX, tweenDuration).SetUpdate(true);
    }
     async Task PausePanelOutro()
    {
        await pauseMenuContainer.DOAnchorPosX(startPosX, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }
}
