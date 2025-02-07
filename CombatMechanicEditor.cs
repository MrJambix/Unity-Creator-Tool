using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CombatMechanicsEditor : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();

    [System.Serializable]
    public class Enemy
    {
        public string name;
        public int health;
        public int damage;
    }

    private void Start()
    {
        // Initialize the combat mechanics system
        enemies = new List<Enemy>();
    }

    private void Update()
    {
        // Update the combat mechanics system
        foreach (Enemy enemy in enemies)
        {
            // Check if the enemy is alive
            if (enemy.health > 0)
            {
                // Attack the player
                AttackPlayer(enemy);
            }
        }
    }

    private void AttackPlayer(Enemy enemy)
    {
        // Calculate the damage dealt to the player
        int damageDealt = enemy.damage;
        // Apply the damage to the player
        PlayerHealth.TakeDamage(damageDealt);
    }
}
