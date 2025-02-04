using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.SearchService;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    private Animator layoutAnimator;
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    private Story currentStory;
    
    public bool dialogueIsPlaying { get;private set; }
    
    private static DialogueManager instance;
    
    private const string SPEAKER_TAG = "Speaker";
    private const string PORTRAIT_TAG = "Target";
    private const string LAYOUT_TAG = "Layout";

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of DialogueManager found!");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        
        // get layout animator
        layoutAnimator = dialoguePanel.GetComponent<Animator>(); 
        
        // get all choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        // return if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }
    }

   

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // set text for the current dialogue line
            dialogueText.text = currentStory.Continue();
            // Display choices
            DisplayChoices();
            // Handle Tags
            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        dialoguePanel.SetActive(true);
        dialogueIsPlaying = true;
        currentStory = new Story(inkJSON.text);
        
        // reset
        displayNameText.text = "???";
        
        ContinueStory();

    }
    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

   
    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        { 
          // parse the tag
          string[] splitTag = tag.Split(':');
          if (splitTag.Length != 2)
          {
              Debug.LogError("Tag could not be appropriately parsed: " + tag);
          }
          string tagKey = splitTag[0].Trim();
          string tagValue = splitTag[1].Trim();
          
          // handle the tag
          switch (tagKey)
          {
              case SPEAKER_TAG:
                  displayNameText.text = tagValue;
                  Debug.Log("speaker" + tagValue);
                  break;
              case PORTRAIT_TAG:
                  portraitAnimator.Play(tagValue);
                  Debug.Log("portrait" + tagValue);
                  break;
              case LAYOUT_TAG:
                  layoutAnimator.Play(tagValue);
                  Debug.Log("layout" + tagValue);
                  break;
              default:
                  Debug.LogWarning("Tag could not be parsed: " + tagKey);
                  break;
          }
        }
    }
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("There are more choices to display!" + currentChoices.Count);
        }
    }
}
