using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterProgressionEditor : MonoBehaviour
{
    public List<Character> characters = new List<Character>();

    [System.Serializable]
    public class Character
    {
        public string name;
        public int level;
        public int experience;
        public List<Skill> skills = new List<Skill>();
    }

    [System.Serializable]
    public class Skill
    {
        public string name;
        public int level;
        public int experience;
    }

    private void Start()
    {
        // Initialize the character progression system
        characters = new List<Character>();
    }

    private void Update()
    {
        // Update the character progression system
        foreach (Character character in characters)
        {
            // Check if the character has enough experience to level up
            if (character.experience >= GetExperienceRequiredForNextLevel(character))
            {
                // Level up the character
                LevelUpCharacter(character);
            }
        }
    }

    private int GetExperienceRequiredForNextLevel(Character character)
    {
        // Calculate the experience required for the next level
        return character.level * 100;
    }

    private void LevelUpCharacter(Character character)
    {
        // Increase the character's level
        character.level++;
        // Reset the character's experience
        character.experience = 0;
        // Increase the character's skills
        foreach (Skill skill in character.skills)
        {
            skill.level++;
        }
    }
}
