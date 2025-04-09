using UnityEngine;
using TMPro;
public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _objectiveText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuestObjective01()
    {
        _objectiveText.text = "Find a way to get Roy to leave his room.";
    }

   public void FindRoy()
    {
        _objectiveText.text = "Find Roy"; 
    }

   public void TalkToRoy()
    {
        _objectiveText.text = "Talk to Roy";
    }

   public void CompletedObjective()
    {
        _objectiveText.text = "Completed Objective";
    }
}
