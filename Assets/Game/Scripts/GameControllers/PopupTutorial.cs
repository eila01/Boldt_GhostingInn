using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class PopupTutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorialMenu;
    [SerializeField] RectTransform tutorialContainer;
    [SerializeField] float startPosY, endPosY;
    [SerializeField] float tweenDuration;
    [SerializeField] CanvasGroup tutorialCanvasGroup;
    [SerializeField] TextMeshProUGUI tutorialText;
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
        tutorialMenu.SetActive(true);
        Time.timeScale = 0;
        PausePanelIntro();
    }

    public async void Resume()
    {
        await PausePanelOutro();
        tutorialMenu.SetActive(false);
        Time.timeScale = 1;
    }

    void PausePanelIntro()
    {
        tutorialCanvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
        tutorialContainer.DOAnchorPosY(endPosY, tweenDuration).SetUpdate(true);
    }
    async Task PausePanelOutro()
    {
        await tutorialContainer.DOAnchorPosY(startPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }

    void interactTutorial()
    {
        tutorialText.text = "Press E or LMB to interact with objects";
    }
    void pauseTutorial()
    {
        tutorialText.text = "Press P to pause and bring up current objective";
    }

    void switchTutorial()
    {
        tutorialText.text = "Press Right Shift to swap between Flute and Corinne";

    }
     
}
