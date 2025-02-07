using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestSystemEditor : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();

    [System.Serializable]
    public class Quest
    {
        public string name;
        public string description;
        public List<QuestObjective> objectives = new List<QuestObjective>();
        public List<Reward> rewards = new List<Reward>();
    }

    [System.Serializable]
    public class QuestObjective
    {
        public string name;
        public string description;
        public int requiredAmount;
    }

    [System.Serializable]
    public class Reward
    {
        public string name;
        public string description;
        public int amount;
    }

    private void Start()
    {
        // Initialize the quest system
        quests = new List<Quest>();
    }

    private void Update()
    {
        // Update the quest system
        foreach (Quest quest in quests)
        {
            // Check if the quest is completed
            if (IsQuestCompleted(quest))
            {
                // Reward the player
                RewardPlayer(quest);
            }
        }
    }

    private bool IsQuestCompleted(Quest quest)
    {
        // Check if all objectives are completed
        foreach (QuestObjective objective in quest.objectives)
        {
            if (objective.requiredAmount > 0)
            {
                return false;
            }
        }
        return true;
    }

    private void RewardPlayer(Quest quest)
    {
        // Give the player the rewards
        foreach (Reward reward in quest.rewards)
        {
            // Add the reward to the player's inventory
            PlayerInventory.AddReward(reward);
        }
    }
}
