using UnityEngine;
using UnityEditor;

public class GameCreatorTool : EditorWindow
{
    // Object creation
    private string objectName = "New Object";
    private PrimitiveType objectType = PrimitiveType.Cube;
    private Color objectColor = Color.white;
    private Vector3 objectSize = Vector3.one;

    // Object modification
    private GameObject selectedObject;

    // Physics and movement
    private bool addPhysics = false;
    private float movementSpeed = 5f;

    // Animation and effects
    private string animationName = "New Animation";
    private float animationDuration = 1f;

    // UI creation
    private string uiName = "New UI";
    private Vector2 uiSize = Vector2.one;

    // Scene management
    private string sceneName = "New Scene";

    // Prefab management
    private string prefabName = "New Prefab";

    // Sound management
    private string soundName = "New Sound";
    private float soundVolume = 1f;

    // Particle effects
    private string particleEffectName = "New Particle Effect";
    private float particleEffectDuration = 1f;

    // Level design tools
    private string levelName = "New Level";
    private Vector3 levelSize = Vector3.one;

    // AI and pathfinding
    private string aiAgentName = "New AI Agent";
    private float aiAgentSpeed = 5f;

    // Networking and multiplayer
    private string networkName = "New Network";
    private int networkPort = 8080;

    [MenuItem("Window/Game Creator Tool")]
    public static void ShowWindow()
    {
        GetWindow<GameCreatorTool>("Game Creator Tool");
    }

    private void OnGUI()
    {
        // Object creation
        GUILayout.Label("Object Creation", EditorStyles.boldLabel);
        objectName = EditorGUILayout.TextField("Name", objectName);
        objectType = (PrimitiveType)EditorGUILayout.EnumPopup("Shape", objectType);
        objectColor = EditorGUILayout.ColorField("Color", objectColor);
        objectSize = EditorGUILayout.Vector3Field("Size", objectSize);

        if (GUILayout.Button("Create Object"))
        {
            CreateObject();
        }

        // Object modification
        GUILayout.Label("Object Modification", EditorStyles.boldLabel);
        selectedObject = (GameObject)EditorGUILayout.ObjectField("Select Object", selectedObject, typeof(GameObject), true);

        if (selectedObject != null)
        {
            if (GUILayout.Button("Modify Object"))
            {
                ModifyObject();
            }
        }

        // Physics and movement
        GUILayout.Label("Physics and Movement", EditorStyles.boldLabel);
        addPhysics = EditorGUILayout.Toggle("Add Physics", addPhysics);
        movementSpeed = EditorGUILayout.FloatField("Movement Speed", movementSpeed);

        if (GUILayout.Button("Add Physics and Movement"))
        {
            AddPhysicsAndMovement();
        }

        // Animation and effects
        GUILayout.Label("Animation and Effects", EditorStyles.boldLabel);
        animationName = EditorGUILayout.TextField("Name", animationName);
        animationDuration = EditorGUILayout.FloatField("Duration", animationDuration);

        if (GUILayout.Button("Create Animation"))
        {
            CreateAnimation();
        }

        // UI creation
        GUILayout.Label("UI Creation", EditorStyles.boldLabel);
        uiName = EditorGUILayout.TextField("Name", uiName);
        uiSize = EditorGUILayout.Vector2Field("Size", uiSize);

        if (GUILayout.Button("Create UI"))
        {
            CreateUI();
        }

        // Scene management
        GUILayout.Label("Scene Management", EditorStyles.boldLabel);
        sceneName = EditorGUILayout.TextField("Name", sceneName);

        if (GUILayout.Button("Create Scene"))
        {
            CreateScene();
        }

        // Prefab management
        GUILayout.Label("Prefab Management", EditorStyles.boldLabel);
        prefabName = EditorGUILayout.TextField("Name", prefabName);

        if (GUILayout.Button("Create Prefab"))
        {
            CreatePrefab();
        }

        // Sound management
        GUILayout.Label("Sound Management", EditorStyles.boldLabel);
        soundName = EditorGUILayout.TextField("Name", soundName);
        soundVolume = EditorGUILayout.FloatField("Volume", soundVolume);

        if (GUILayout.Button("Create Sound"))
        {
            CreateSound();
        }

        // Particle effects
        GUILayout.Label("Particle Effects", EditorStyles.boldLabel);
        particleEffectName = EditorGUILayout.TextField("Name", particleEffectName);
        particleEffectDuration = EditorGUILayout.FloatField("Duration", particleEffectDuration);

        if (GUILayout.Button("Create Particle Effect"))
        {
            CreateParticleEffect();
        }

        // Level design tools
        GUILayout.Label("Level Design Tools", EditorStyles.boldLabel);
        levelName = EditorGUILayout.TextField("Name", levelName);
        levelSize = EditorGUILayout.Vector3Field("Size", levelSize);

        if (GUILayout.Button("Create Level"))
        {
            CreateLevel();
        }

        // AI and pathfinding
        GUILayout.Label("AI and Pathfinding", EditorStyles.boldLabel);
        aiAgentName = EditorGUILayout.TextField("Name", aiAgentName);
        aiAgentSpeed = EditorGUILayout.FloatField("Speed", aiAgentSpeed);

        if (GUILayout.Button("Create AI Agent"))
        {
            CreateAIAGENT();
        }

        // Networking and multiplayer
        GUILayout.Label("Networking and Multiplayer", EditorStyles.boldLabel);
        networkName = EditorGUILayout.TextField("Name", networkName);
        networkPort = EditorGUILayout.IntField("Port", networkPort);

        if (GUILayout.Button("Create Network"))
        {
            CreateNetwork();
        }
    }

    private void CreateObject()
    {
        GameObject newObject = GameObject.CreatePrimitive(objectType);
        newObject.name = objectName;
        newObject.transform.localScale = objectSize;

        if (customMaterial != null)
        {
            newObject.GetComponent<Renderer>().material = customMaterial;
        }
        else
        {
            newObject.GetComponent<Renderer>().material.color = objectColor;
        }

        Undo.RegisterCreatedObjectUndo(newObject, "Create " + objectName);
    }

    private void ModifyObject()
    {
        if (selectedObject == null) return;

        // Modify the object's properties
        selectedObject.transform.localScale = objectSize;

        if (customMaterial != null)
        {
            selectedObject.GetComponent<Renderer>().material = customMaterial;
        }
        else
        {
            selectedObject.GetComponent<Renderer>().material.color = objectColor;
        }

        Undo.RecordObject(selectedObject.transform, "Modify Object");
    }

    private void AddPhysicsAndMovement()
    {
        if (selectedObject == null) return;

        // Add physics and movement to the object
        selectedObject.AddComponent<Rigidbody>();
        selectedObject.AddComponent<SimpleMovement>();

        Undo.RecordObject(selectedObject.transform, "Add Physics and Movement");
    }

    private void CreateAnimation()
    {
        // Create a new animation
        Animation newAnimation = new Animation();
        newAnimation.name = animationName;
        newAnimation.duration = animationDuration;

        Undo.RegisterCreatedObjectUndo(newAnimation, "Create " + animationName);
    }

    private void CreateUI()
    {
        // Create a new UI
        GameObject newUI = new GameObject();
        newUI.name = uiName;
        newUI.transform.localScale = uiSize;

        Undo.RegisterCreatedObjectUndo(newUI, "Create " + uiName);
    }

    private void CreateScene()
    {
        // Create a new scene
        Scene newScene = new Scene();
        newScene.name = sceneName;

        Undo.RegisterCreatedObjectUndo(newScene, "Create " + sceneName);
    }

    private void CreatePrefab()
    {
        // Create a new prefab
        GameObject newPrefab = new GameObject();
        newPrefab.name = prefabName;

        Undo.RegisterCreatedObjectUndo(newPrefab, "Create " + prefabName);
    }

    private void CreateSound()
    {
        // Create a new sound
        AudioClip newSound = new AudioClip();
        newSound.name = soundName;
        newSound.volume = soundVolume;

        Undo.RegisterCreatedObjectUndo(newSound, "Create " + soundName);
    }

    private void CreateParticleEffect()
    {
        // Create a new particle effect
        ParticleSystem newParticleEffect = new ParticleSystem();
        newParticleEffect.name = particleEffectName;
        newParticleEffect.duration = particleEffectDuration;

        Undo.RegisterCreatedObjectUndo(newParticleEffect, "Create " + particleEffectName);
    }

    private void CreateLevel()
    {
        // Create a new level
        GameObject newLevel = new GameObject();
        newLevel.name = levelName;
        newLevel.transform.localScale = levelSize;

        Undo.RegisterCreatedObjectUndo(newLevel, "Create " + levelName);
    }

    private void CreateAIAGENT()
    {
        // Create a new AI agent
        GameObject newAIAGENT = new GameObject();
        newAIAGENT.name = aiAgentName;
        newAIAGENT.transform.localScale = Vector3.one;

        Undo.RegisterCreatedObjectUndo(newAIAGENT, "Create " + aiAgentName);
    }

    private void CreateNetwork()
    {
        // Create a new network
        GameObject newNetwork = new GameObject();
        newNetwork.name = networkName;
        newNetwork.transform.localScale = Vector3.one;

        Undo.RegisterCreatedObjectUndo(newNetwork, "Create " + networkName);
    }
}

public class SimpleMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;
        transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
}
