using UnityEngine;
using Yarn.Unity;


public class DialogueControls : MonoBehaviour
{
    /*
    [SerializeField] private TMPro.TMP_Text dialogue = null;

    private DialogueUI dialogueUI = null;
    private TMPro.TMP_Text[] options;

    private int optionSize;
    private int currentOption;

    private bool isOptionDisplayed;
    
    void Start()
    {
        // Flag check when the options are displayed to enable the controls for them 
        isOptionDisplayed = false;
        // Get a Ref to the DialogueUI
        dialogueUI = FindObjectOfType<DialogueUI>();
        // save the # of options available
        optionSize = dialogueUI.optionButtons.Count;
        // Initialize Current Index
        currentOption = 0;
        // Initialize the array size to the # of options
        options = new TMPro.TMP_Text[optionSize];
        // Get the TextMeshPro Text components from the option buttons in the DialogueUI
        for (int i = 0; i < optionSize; i++)
        {
            options[i] = dialogueUI.optionButtons[i].GetComponentInChildren<TMPro.TMP_Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ControlOptions();
    }

    private void ChangeOption()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentOption = (currentOption + 1) % optionSize;
            dialogue.SetText(options[currentOption].text);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // Move to the Previous Option
            if (currentOption == 0)
            {
                currentOption = optionSize - 1;
            }
            else
            {
                currentOption = (Mathf.Abs(currentOption - 1) % optionSize);
            }
            
            dialogue.SetText(options[currentOption].text);
        }
    }

    private void SkipDialogue()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogueUI.MarkLineComplete();
        }
    }

    private void SelectOption()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            dialogueUI.SelectOption(currentOption);
            ResetCurrentOption();
        }
    }

    private void ResetCurrentOption()
    {
        currentOption = 0;
    }

    public void SetStartingOption()
    {
        dialogue.SetText(options[0].text);
    }

    public void setOptionDisplayed(bool flag)
    {
        isOptionDisplayed = flag;
    }
    */
}
