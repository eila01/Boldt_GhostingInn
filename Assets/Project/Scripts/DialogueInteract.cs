using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity.Example;

public class DialogueInteract : MonoBehaviour
{
    private GameObject _dialogueBox;
    private Dialogue _dia;
    private PlayerController _player;
    private QuestManager _qm;
    
    [Header("Dialogue Options")]
    public string[] defaultLines;
    public string[] questAcceptedLines;
    public string[] questCompletedLines;

    [Header("Dialogue Conditions")] 
    [Tooltip("Use Auto Activate whenever you want a collider to activate the dialogue when entered")]
    public bool autoActivate;
    [Tooltip("Use the quest ID to separate different quests")]
    public string questID;

    private bool _insideTriggerZone;
    private bool _dialogueStarted;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _dialogueBox = GameObject.FindGameObjectWithTag("DiaCanvas");
        _dia = _dialogueBox.GetComponent<Dialogue>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _qm = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
    }

    void Update()
    {
        if (_insideTriggerZone && !_dialogueStarted )
        {
            if(Input.GetKeyDown(KeyCode.E) || autoActivate)
                StartDialogue();
        }
    
        if (_dialogueStarted && !_dialogueBox.activeSelf)
        {
            PlayerController.canMove = true;
            _dialogueStarted = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
            _insideTriggerZone = true;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _insideTriggerZone = false;
            _dialogueStarted = false;
            _dialogueBox.SetActive(false);
            PlayerController.canMove = true;
            _dia.ClearLines();
        }
    }

    void StartDialogue()
    {
        autoActivate = false;
        _dialogueStarted = true;
        _dialogueBox.SetActive(true);
        PlayerController.canMove = false;
        _player.rb.linearVelocity = new Vector2(0f, 0f);

        _qm.SetCurrentDialogue(this);
        
        _dia.ResetDialogue();
        _dia.lines = GetDialogueLines();
        _dia.currentQuestID = questID;
        _dia.StartDialogue();
    }

    public void QuestAccepted()
    {
        _qm.AcceptQuest(questID);
        _dia.ResetDialogue();
        _dia.lines = GetDialogueLines();
        _dia.StartDialogue();
    }

    string[] GetDialogueLines()
    {
        if (_qm.IsQuestCompleted(questID)) return (string[])questCompletedLines.Clone();
        if (_qm.IsQuestAccepted(questID)) return (string[])questAcceptedLines.Clone();
        return (string[])defaultLines.Clone();
    }
}
