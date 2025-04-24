using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;
public class DialogueUI : DialogueViewBase
{
   // public TextMeshProUGUI dialogueText;
    public Image textBoxBackground;
    public Image characterBoxBackground;
    
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color unknownColor;
    [SerializeField] private Color signColor;
    [SerializeField] private Color corinneColor;
    [SerializeField] private Color fluteColor;
    [SerializeField] private Color sherryColor;
    [SerializeField] private Color royColor;
    [SerializeField] private Color clydeColor;
    [SerializeField] private Color ralphColor;
    [SerializeField] private Color johanColor;
    public override void RunLine(LocalizedLine line, System.Action onDialogueLineFinished)
    {
        Debug.Log("RunLine");
        Debug.Log($"[ColorManager] Speaker: {line.CharacterName}");
        string speaker = line.CharacterName; // line is a Yarn.Line object

        // Set color based on speaker
        switch (speaker)
        {
            case "Corinne":
                textBoxBackground.color = corinneColor;
                characterBoxBackground.color = corinneColor;
                break;
            case "Flute":
                textBoxBackground.color = fluteColor;
                characterBoxBackground.color = fluteColor;
                break;
            case "Sherry":
                textBoxBackground.color = sherryColor;
                characterBoxBackground.color = sherryColor;
                break;
            case "Roy":
                textBoxBackground.color = royColor;
                characterBoxBackground.color = royColor;
                break;
            case "Clyde":
                textBoxBackground.color = clydeColor;
                characterBoxBackground.color = clydeColor;
                break;
            case "Ralph":
                textBoxBackground.color = ralphColor;
                characterBoxBackground.color = ralphColor;
                break;
            case "Johan":
                textBoxBackground.color = johanColor;
                characterBoxBackground.color = johanColor;
                break;
            case "UNKNOWN":
                textBoxBackground.color = unknownColor;
                characterBoxBackground.color = unknownColor;
                break;
            case "Sign":
                textBoxBackground.color = signColor;
                characterBoxBackground.color = signColor;
                break;
            default:
                textBoxBackground.color = defaultColor; // Default color
                characterBoxBackground.color = defaultColor;
                break;
        }
    }
    
}
