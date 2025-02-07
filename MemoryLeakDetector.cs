using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MemoryLeakDetector : EditorWindow
{
    private const string TOOL_NAME = "Memory Leak Detector";
    private const string TOOL_DESCRIPTION = "Detect memory leaks and bad frame drops in Unity";

    private bool isRunning = false;
    private List<MemoryLeak> memoryLeaks = new List<MemoryLeak>();
    private List<FrameDrop> frameDrops = new List<FrameDrop>();

    [MenuItem("Tools/Memory Leak Detector")]
    public static void ShowWindow()
    {
        GetWindow<MemoryLeakDetector>(TOOL_NAME);
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField(TOOL_NAME, EditorStyles.boldLabel);
        EditorGUILayout.LabelField(TOOL_DESCRIPTION, EditorStyles.label);

        if (isRunning)
        {
            EditorGUILayout.LabelField("Running...", EditorStyles.label);
        }
        else
        {
            if (GUILayout.Button("Start"))
            {
                StartAnalysis();
            }
        }

        if (memoryLeaks.Count > 0)
        {
            EditorGUILayout.LabelField("Memory Leaks:", EditorStyles.boldLabel);
            foreach (MemoryLeak leak in memoryLeaks)
            {
                EditorGUILayout.LabelField(leak.ToString(), EditorStyles.label);
            }
        }

        if (frameDrops.Count > 0)
        {
            EditorGUILayout.LabelField("Frame Drops:", EditorStyles.boldLabel);
            foreach (FrameDrop drop in frameDrops)
            {
                EditorGUILayout.LabelField(drop.ToString(), EditorStyles.label);
            }
        }
    }

    private void StartAnalysis()
    {
        isRunning = true;
        memoryLeaks.Clear();
        frameDrops.Clear();

        // Start the Profiler
        Profiler.BeginSample("Memory Leak Detector");

        // Analyze the heap size and object count over time
        float heapSize = Profiler.GetHeapSize();
        int objectCount = Profiler.GetObjectCount();

        // Check for memory leaks
        if (heapSize > 1000000 || objectCount > 10000)
        {
            memoryLeaks.Add(new MemoryLeak(heapSize, objectCount));
        }

        // Analyze the frame rate and render time
        float frameRate = Profiler.GetFrameRate();
        float renderTime = Profiler.GetRenderTime();

        // Check for bad frame drops
        if (frameRate < 30 || renderTime > 100)
        {
            frameDrops.Add(new FrameDrop(frameRate, renderTime));
        }

        // Stop the Profiler
        Profiler.EndSample();

        isRunning = false;
    }
}

public class MemoryLeak
{
    public float HeapSize { get; set; }
    public int ObjectCount { get; set; }

    public MemoryLeak(float heapSize, int objectCount)
    {
        HeapSize = heapSize;
        ObjectCount = objectCount;
    }

    public override string ToString()
    {
        return $"Heap Size: {HeapSize} bytes, Object Count: {ObjectCount}";
    }
}

public class FrameDrop
{
    public float FrameRate { get; set; }
    public float RenderTime { get; set; }

    public FrameDrop(float frameRate, float renderTime)
    {
        FrameRate = frameRate;
        RenderTime = renderTime;
    }

    public override string ToString()
    {
        return $"Frame Rate: {FrameRate} FPS, Render Time: {RenderTime} ms";
    }
}
