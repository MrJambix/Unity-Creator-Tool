using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemizationSystemEditor : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    private Dictionary<string, ItemTemplate> templates = new Dictionary<string, ItemTemplate>();

    void Start()
    {
        InitializeTemplates();
        CreateDefaultItems();
    }

    private void InitializeTemplates()
    {
        // Weapon Templates
        templates.Add("Basic Sword", new ItemTemplate(
            "Sword", 
            ItemType.Weapon, 
            new Dictionary<StatType, float> {
                { StatType.Damage, 10f },
                { StatType.Speed, 1.0f }
            }
        ));

        // Armor Templates
        templates.Add("Leather Armor", new ItemTemplate(
            "Light Armor",
            ItemType.Armor,
            new Dictionary<StatType, float> {
                { StatType.Defense, 5f },
                { StatType.Weight, 3f }
            }
        ));

        // Consumable Templates
        templates.Add("Health Potion", new ItemTemplate(
            "Potion",
            ItemType.Consumable,
            new Dictionary<StatType, float> {
                { StatType.Healing, 50f },
                { StatType.Duration, 1f }
            }
        ));
    }

    private void CreateDefaultItems()
    {
        CreateItem("Basic Sword", "Rusty Sword", ItemRarity.Common);
        CreateItem("Leather Armor", "Worn Leather", ItemRarity.Common);
        CreateItem("Health Potion", "Minor Healing Potion", ItemRarity.Common);
    }

    public Item CreateItem(string templateName, string itemName, ItemRarity rarity)
    {
        if (!templates.ContainsKey(templateName))
            return null;

        ItemTemplate template = templates[templateName];
        
        Item newItem = new Item
        {
            name = itemName,
            type = template.type,
            subType = template.subType,
            rarity = rarity,
            stats = new Dictionary<StatType, float>(template.baseStats)
        };

        // Apply rarity bonuses
        foreach (var stat in newItem.stats)
        {
            newItem.stats[stat.Key] *= (1 + ((float)rarity * 0.2f));
        }

        items.Add(newItem);
        return newItem;
    }

    void Update()
    {
        foreach (Item item in items)
        {
            if (IsItemEquipped(item))
            {
                ApplyItemEffects(item);
            }
        }
    }

    private bool IsItemEquipped(Item item)
    {
        return false; // Implement your equipment check logic
    }

    private void ApplyItemEffects(Item item)
    {
        foreach (var stat in item.stats)
        {
            // Apply stat effects based on type
            switch (stat.Key)
            {
                case StatType.Damage:
                    // Apply damage bonus
                    break;
                case StatType.Defense:
                    // Apply defense bonus
                    break;
            }
        }
    }
}

[System.Serializable]
public class Item
{
    public string name;
    public ItemType type;
    public string subType;
    public ItemRarity rarity;
    public Dictionary<StatType, float> stats;
}

[System.Serializable]
public class ItemTemplate
{
    public string subType;
    public ItemType type;
    public Dictionary<StatType, float> baseStats;

    public ItemTemplate(string subType, ItemType type, Dictionary<StatType, float> baseStats)
    {
        this.subType = subType;
        this.type = type;
        this.baseStats = baseStats;
    }
}

public enum ItemType
{
    Weapon,
    Armor,
    Consumable,
    Accessory
}

public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

public enum StatType
{
    Damage,
    Defense,
    Speed,
    Weight,
    Healing,
    Duration
}
