using System.Collections;
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
    private void Update()
    {
        if (tutorialMenu.activeInHierarchy && Input.anyKeyDown)
        {
            StartCoroutine(waitForMovement());
            //Resume();
        }
    }
    public void Home()
    {
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
    }

    public void PauseTutorial()
    {
        tutorialMenu.SetActive(true);
        //Time.timeScale = 0;
        TutorialPanelIntro();
    }

    public async void Resume()
    {
        await TutorialPanelOutro();
        tutorialMenu.SetActive(false);
        //Time.timeScale = 1;
    }

    void TutorialPanelIntro()
    {
        tutorialCanvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
        tutorialContainer.DOAnchorPosY(endPosY, tweenDuration).SetUpdate(true);
    }
    async Task TutorialPanelOutro()
    {
        await tutorialContainer.DOAnchorPosY(startPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }

    public void interactTutorialText()
    {
        tutorialText.text = "Press E or LMB to interact with objects";
    }
    public void pauseTutorialText()
    {
        tutorialText.text = "Press P to pause and bring up current objective";
    }

    public void switchTutorialText()
    {
        Debug.Log("Before: " + tutorialText.text);

        tutorialText.text = "Press Right Shift to swap between Flute and Corinne. When controlling Flute press Left Shift and Ctrl to move up or down.";
        Debug.Log("After: " + tutorialText.text);

    }
    public void fluteTutorialText()
    {
        tutorialText.text = "Flute can possess electric devices that can cause a disturbance or a change in the environment. Press E to possess electric interactable objects. ";

    }
    public void talkToRoyText()
    {
        Debug.Log("Before: " + tutorialText.text);
        tutorialText.text = "Swap to Corinne to talk to Roy";
        Debug.Log("After: " + tutorialText.text);

    }

    IEnumerator waitForMovement()
    {
        yield return new WaitForSeconds(3.5f);
        Resume();
    }
}
