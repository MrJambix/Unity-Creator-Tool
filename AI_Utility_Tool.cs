using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class AI_Utility_Tool : MonoBehaviour
{
    private List<AI_Agent> agents = new List<AI_Agent>();
    private Dictionary<BehaviorType, float> utilityScores = new Dictionary<BehaviorType, float>();

    void Start()
    {
        InitializeUtilityScores();
        agents = new List<AI_Agent>();
    }

    private void InitializeUtilityScores()
    {
        foreach (BehaviorType type in System.Enum.GetValues(typeof(BehaviorType)))
        {
            utilityScores[type] = 1.0f;
        }
    }

    void Update()
    {
        foreach (AI_Agent agent in agents)
        {
            UpdateAgentBehavior(agent);
        }
    }

    private void UpdateAgentBehavior(AI_Agent agent)
    {
        float highestScore = float.MinValue;
        AI_Behavior bestBehavior = null;

        foreach (var behavior in agent.behaviors)
        {
            float score = CalculateUtilityScore(agent, behavior);
            if (score > highestScore)
            {
                highestScore = score;
                bestBehavior = behavior;
            }
        }

        agent.currentBehavior = bestBehavior;
        agent.ExecuteBehavior();
    }

    private float CalculateUtilityScore(AI_Agent agent, AI_Behavior behavior)
    {
        float score = behavior.priority * utilityScores[behavior.behaviorType];

        foreach (var condition in behavior.conditions)
        {
            if (!EvaluateCondition(agent, condition))
            {
                return 0f;
            }
        }

        return score;
    }

    private bool EvaluateCondition(AI_Agent agent, Condition condition)
    {
        float currentValue = GetConditionValue(agent, condition.conditionType);
        
        switch (condition.comparison)
        {
            case ComparisonType.LessThan:
                return currentValue < condition.value;
            case ComparisonType.GreaterThan:
                return currentValue > condition.value;
            case ComparisonType.Equals:
                return Mathf.Approximately(currentValue, condition.value);
            case ComparisonType.NotEquals:
                return !Mathf.Approximately(currentValue, condition.value);
            case ComparisonType.LessThanOrEqual:
                return currentValue <= condition.value;
            case ComparisonType.GreaterThanOrEqual:
                return currentValue >= condition.value;
            default:
                return false;
        }
    }

    private float GetConditionValue(AI_Agent agent, ConditionType conditionType)
    {
        switch (conditionType)
        {
            case ConditionType.Health:
                return agent.stats.health;
            case ConditionType.Distance:
                return Vector3.Distance(agent.transform.position, agent.target ? agent.target.position : agent.transform.position);
            case ConditionType.Stamina:
                return agent.stats.stamina;
            case ConditionType.Mana:
                return agent.stats.mana;
            default:
                return 0f;
        }
    }

    public void AddAgent(AI_Agent agent)
    {
        agents.Add(agent);
    }
}

[System.Serializable]
public class AI_Agent : MonoBehaviour
{
    public string agentName;
    public List<AI_Behavior> behaviors = new List<AI_Behavior>();
    public AI_Behavior currentBehavior;
    public Transform target;
    public AgentStats stats;

    public void ExecuteBehavior()
    {
        if (currentBehavior == null) return;

        switch (currentBehavior.behaviorType)
        {
            case BehaviorType.MeleeAttack:
                ExecuteMeleeAttack();
                break;
            case BehaviorType.RangedAttack:
                ExecuteRangedAttack();
                break;
            case BehaviorType.Flee:
                ExecuteFlee();
                break;
            case BehaviorType.Chase:
                ExecuteChase();
                break;
            // Add other behavior types here
        }
    }

    private void ExecuteMeleeAttack()
    {
        Debug.Log($"{agentName} executing melee attack");
    }

    private void ExecuteRangedAttack()
    {
        Debug.Log($"{agentName} executing ranged attack");
    }

    private void ExecuteFlee()
    {
        Debug.Log($"{agentName} fleeing from target");
    }

    private void ExecuteChase()
    {
        Debug.Log($"{agentName} chasing target");
    }
}

[System.Serializable]
public class AgentStats
{
    public float health = 100f;
    public float stamina = 100f;
    public float mana = 100f;
    public float energy = 100f;
}
