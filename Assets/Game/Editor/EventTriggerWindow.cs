using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EventTrigger))]
public class EventTriggerWindow : Editor
{
    EventTrigger trigger;

    public override void OnInspectorGUI()
    {
        trigger = (EventTrigger)target; // it will custom 
        GUILayout.Label("Player");
        SerializedProperty player = serializedObject.FindProperty("machine");
        EditorGUILayout.PropertyField(player);
        GUILayout.Label("Event");
        trigger.type = (EventTrigger.EventType)EditorGUILayout.EnumPopup(trigger.type);
        if(trigger.type == (EventTrigger.EventType.Dialogue))
        {
            SerializedProperty dialogue = serializedObject.FindProperty("dialogue");
            EditorGUILayout.PropertyField(dialogue);
            
        }
        GUILayout.Label("How To Trigger");
        trigger.triggerType = (EventTrigger.TriggerType)EditorGUILayout.EnumPopup(trigger.triggerType);
        // only appear the button type if press button
        if (trigger.triggerType == (EventTrigger.TriggerType.PressButtonInTrigger))
        {
            SerializedProperty button = serializedObject.FindProperty("button");
            EditorGUILayout.PropertyField(button);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
