using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public partial class Dialogue : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI textComponent;
    public GameObject buttonPanel;
    public Image characterPortrait;
    public QuestManager questManager;
    
    [Header("Characters")]
    public Sprite blank;
    [Tooltip("Insert character sprites here")]
    public Sprite[] characterImages;
    [Tooltip("Insert character names here")]
    public string[] characterNames;
    
    [Header("Dialogue")]
    [Tooltip("Insert opening dialogue text here")]
    public string[] lines;
    public float textSpeed;
    [Tooltip("Quest trigger uses keywords to activate button responses")]
    public string questTrigger;

    [HideInInspector]
    public string currentQuestID;
    private int _index;
    
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    
    void Update()
    {
        if (!buttonPanel.activeSelf)
        {
            if (lines[_index].Contains("accept a quest") && !questManager.IsQuestAccepted(currentQuestID))
            {
                buttonPanel.SetActive(true);
            }
            
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
            {
                if (textComponent.text == lines[_index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[_index];
                }
            }
        }
    }

    public void StartDialogue()
    {
        _index = 0;
        UpdatePortrait();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        if (lines[_index] == null || lines[_index] == string.Empty) yield break;
        // Type each character 1 by 1
        foreach (char c in lines[_index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (_index < lines.Length - 1)
        {
            _index++;
            textComponent.text = string.Empty;
            UpdatePortrait();
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void ResetDialogue()
    {
        StopAllCoroutines();
        _index = 0;
        textComponent.text = string.Empty;
    }
    
    public void ClearLines()
    {
        Array.Clear(lines, 0, lines.Length);
    }
    
    void UpdatePortrait()
    {
        bool matchFound = false;
        
        for (int i = 0; i < characterNames.Length; i++)
        {
            if (lines[_index].Contains(characterNames[i]))
            {
                characterPortrait.sprite = characterImages[i];
                matchFound = true;
                break;
            }
        }
        
        if(!matchFound)
        {
            characterPortrait.sprite = blank;
        }
    }
}
