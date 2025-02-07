using UnityEngine;
using System.Collections.Generic;

public class AI_Utility_Tool : MonoBehaviour
{
    private List<AI_Agent> agents = new List<AI_Agent>();

    void Start()
    {
        // Initialize the AI utility tool
        agents = new List<AI_Agent>();
    }

    void Update()
    {
        // Update the AI utility tool
        foreach (AI_Agent agent in agents)
        {
            // Update the agent's behavior
            agent.UpdateBehavior();
        }
    }

    public void AddAgent(AI_Agent agent)
    {
        // Add a new agent to the AI utility tool
        agents.Add(agent);
    }
}

[System.Serializable]
public class AI_Agent
{
    public string agentName;
    public AI_Behavior behavior;

    public void UpdateBehavior()
    {
        // Update the agent's behavior
        behavior.Update();
    }
}
