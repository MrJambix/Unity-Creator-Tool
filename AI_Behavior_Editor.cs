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
            selectedBehavior.conditions[i].comparison = (ComparisonType)EditorGUILayout.EnumPopup("Comparison", selectedBehavior.conditions[i].comparison);
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
    public float priority = 1.0f;
    public float cooldown = 0.0f;

    public AI_Behavior()
    {
        // Add default conditions based on behavior type
        switch (behaviorType)
        {
            case BehaviorType.MeleeAttack:
                conditions.Add(new Condition { conditionType = ConditionType.Health, value = 50f });
                conditions.Add(new Condition { conditionType = ConditionType.Distance, value = 2f });
                break;

            case BehaviorType.Flee:
                conditions.Add(new Condition { conditionType = ConditionType.Health, value = 25f });
                conditions.Add(new Condition { conditionType = ConditionType.EnemyCount, value = 3f });
                break;

            case BehaviorType.Guard:
                conditions.Add(new Condition { conditionType = ConditionType.Distance, value = 10f });
                conditions.Add(new Condition { conditionType = ConditionType.InCombat, value = 0f });
                break;
        }
    }
}

public enum BehaviorType
{
    MeleeAttack,
    RangedAttack,
    Defend,
    Flee,
    Chase,
    Patrol,
    Idle,
    Wander,
    Guard,
    ReturnToSpawn,
    Gather,
    Trade,
    Interact,
    CallForHelp,
    Rest,
    Eat,
    Heal,
    Hide
}

[System.Serializable]
public class Condition
{
    public ConditionType conditionType;
    public float value;
    public ComparisonType comparison = ComparisonType.GreaterThan;
}

public enum ConditionType
{
    Health,
    Stamina,
    Mana,
    Energy,
    InCombat,
    TargetHealth,
    EnemyCount,
    Damage,
    Distance,
    TimeOfDay,
    Weather,
    Visibility,
    GroupSize,
    AllyHealth,
    Reputation,
    Territory,
    Inventory,
    Equipment,
    Gold,
    Resources
}

public enum ComparisonType
{
    LessThan,
    GreaterThan,
    Equals,
    NotEquals,
    LessThanOrEqual,
    GreaterThanOrEqual
}
