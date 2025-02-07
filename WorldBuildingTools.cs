using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class WorldBuildingTools : EditorWindow
{
    private List<Zone> zones = new List<Zone>();
    private Zone selectedZone;
    private Vector2 scrollPosition;
    private bool showAdvancedOptions = false;

    [MenuItem("MMORPG Tools/World Building")]
    public static void ShowWindow()
    {
        GetWindow<WorldBuildingTools>("World Building");
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        DrawZoneList();
        DrawZoneDetails();
        EditorGUILayout.EndHorizontal();
    }

    private void DrawZoneList()
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(200));
        EditorGUILayout.LabelField("Zones", EditorStyles.boldLabel);
        
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        foreach (var zone in zones)
        {
            if (GUILayout.Button(zone.zoneName))
                selectedZone = zone;
        }
        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("Create New Zone"))
            zones.Add(new Zone());

        EditorGUILayout.EndVertical();
    }

    private void DrawZoneDetails()
    {
        if (selectedZone == null) return;

        EditorGUILayout.BeginVertical();
        selectedZone.zoneName = EditorGUILayout.TextField("Name", selectedZone.zoneName);
        selectedZone.zoneType = (ZoneType)EditorGUILayout.EnumPopup("Type", selectedZone.zoneType);
        selectedZone.description = EditorGUILayout.TextField("Description", selectedZone.description);
        
        showAdvancedOptions = EditorGUILayout.Foldout(showAdvancedOptions, "Advanced Options");
        if (showAdvancedOptions)
        {
            DrawAdvancedOptions();
        }

        EditorGUILayout.EndVertical();
    }

    private void DrawAdvancedOptions()
    {
        if (selectedZone == null) return;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Terrain", EditorStyles.boldLabel);
        
        selectedZone.terrainHeight = EditorGUILayout.FloatField("Height", selectedZone.terrainHeight);
        selectedZone.terrainWidth = EditorGUILayout.FloatField("Width", selectedZone.terrainWidth);
        selectedZone.terrainDepth = EditorGUILayout.FloatField("Depth", selectedZone.terrainDepth);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Buildings", EditorStyles.boldLabel);
        
        for (int i = 0; i < selectedZone.buildings.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            selectedZone.buildings[i].buildingName = EditorGUILayout.TextField("Name", selectedZone.buildings[i].buildingName);
            selectedZone.buildings[i].buildingType = (BuildingType)EditorGUILayout.EnumPopup("Type", selectedZone.buildings[i].buildingType);
            if (GUILayout.Button("X", GUILayout.Width(20)))
                selectedZone.buildings.RemoveAt(i);
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Building"))
            selectedZone.buildings.Add(new Building());
    }
}

[System.Serializable]
public class Zone
{
    public string zoneName = "New Zone";
    public ZoneType zoneType;
    public string description;
    public float terrainHeight;
    public float terrainWidth;
    public float terrainDepth;
    public List<Building> buildings = new List<Building>();
}

public enum ZoneType
{
    Forest,
    Desert,
    Mountain,
    City,
    Dungeon
}

[System.Serializable]
public class Building
{
    public string buildingName = "New Building";
    public BuildingType buildingType;
}

public enum BuildingType
{
    House,
    Shop,
    Inn,
    Castle,
    Temple
}
