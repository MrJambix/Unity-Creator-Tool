using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class AI_Behavior_Editor : EditorWindow
{
    private List<AI_Behavior> behaviors = new List<AI_Behavior>();
    private AI_Behavior selectedBehavior;
    private Vector2 scrollPosition;
    private bool showAdvancedOptions = false;

    [MenuItem("MMORPG Tools/AI Behavior Editor")]
    public static void ShowWindow()
    {
        GetWindow<AI_Behavior_Editor>("AI Behavior Editor");
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        DrawBehaviorList();
        DrawBehaviorDetails();
        EditorGUILayout.EndHorizontal();
    }

    private void DrawBehaviorList()
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(200));
        EditorGUILayout.LabelField("Behaviors", EditorStyles.boldLabel);
        
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        foreach (var behavior in behaviors)
        {
            if (GUILayout.Button(behavior.behaviorName))
                selectedBehavior = behavior;
        }
        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("Create New Behavior"))
            behaviors.Add(new AI_Behavior());

        EditorGUILayout.EndVertical();
    }

    private void DrawBehaviorDetails()
    {
        if (selectedBehavior == null) return;

        EditorGUILayout.BeginVertical();
        selectedBehavior.behaviorName = EditorGUILayout.TextField("Name", selectedBehavior.behaviorName);
        selectedBehavior.behaviorType = (BehaviorType)EditorGUILayout.EnumPopup("Type", selectedBehavior.behaviorType);
        selectedBehavior.description = EditorGUILayout.TextField("Description", selectedBehavior.description);
        
        showAdvancedOptions = EditorGUILayout.Foldout(showAdvancedOptions, "Advanced Options");
        if (showAdvancedOptions)
        {
            DrawAdvancedOptions();
        }

        EditorGUILayout.EndVertical();
    }

    private void DrawAdvancedOptions()
    {
        if (selectedBehavior == null) return;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Conditions", EditorStyles.boldLabel);
        
        for (int i = 0; i < selectedBehavior.conditions.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            selectedBehavior.conditions[i].conditionType = (ConditionType)EditorGUILayout.EnumPopup("Type", selectedBehavior.conditions[i].conditionType);
            selectedBehavior.conditions[i].value = EditorGUILayout.FloatField("Value", selectedBehavior.conditions[i].value);
            if (GUILayout.Button("X", GUILayout.Width(20)))
                selectedBehavior.conditions.RemoveAt(i);
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Condition"))
            selectedBehavior.conditions.Add(new Condition());
    }
}

[System.Serializable]
public class AI_Behavior
{
    public string behaviorName = "New Behavior";
    public BehaviorType behaviorType;
    public string description;
    public List<Condition> conditions = new List<Condition>();
}

public enum BehaviorType
{
    Patrol,
    Chase,
    Flee,
    Idle
}

[System.Serializable]
public class Condition
{
    public ConditionType conditionType;
    public float value;
}

public enum ConditionType
{
    Distance,
    Health,
    Hunger,
    Thirst
}
