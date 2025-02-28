using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Action
{
    public string name;
    public string description;
    public string actionData;
    public TargetType targetType;
    public Damage damage;
    public List<StatusEffect> statusEffects;

    //Dictionary to map target type value to string name
    public static Dictionary<TargetType, string> targetTypeToString = new Dictionary<TargetType, string>
    {
        {TargetType.Self, "Self"},
        {TargetType.SingleEnemy, "SingleEnemy"},
        {TargetType.SingleAlly, "SingleAlly"},
        {TargetType.AllEnemies, "AllEnemies"},
        {TargetType.AllAllies, "AllAllies"},
        {TargetType.AllUnits, "AllUnits"}
    };

    public static Dictionary<string, TargetType> stringToTargetType = new Dictionary<string, TargetType>
    {
        {"Self", TargetType.Self},
        {"SingleEnemy", TargetType.SingleEnemy},
        {"SingleAlly", TargetType.SingleAlly},
        {"AllEnemies", TargetType.AllEnemies},
        {"AllAllies", TargetType.AllAllies},
        {"AllUnits", TargetType.AllUnits}
    };

    public static string TargetTypeToString(TargetType targetType)
    {
        return targetTypeToString[targetType];
    }

    public static TargetType StringToTargetType(string targetType)
    {
        try
        {
            return stringToTargetType[targetType];
        }
        catch (KeyNotFoundException)
        {
            throw new DataException("Invalid target type: " + targetType);
        }
    }

    public Action(string name, string description, TargetType targetType, Damage damage, List<StatusEffect> statusEffects)
    {
        this.name = name;
        this.description = description;
        this.targetType = targetType;
        this.damage = damage;
        this.statusEffects = statusEffects;
    }

    public void SetActionData()
    {
        actionData = name + "\n" + TargetTypeToString(targetType) + "\n" + damage.ToString() + "\n";
        foreach (StatusEffect effect in statusEffects)
        {
            actionData += effect.name + ", ";
        }
    }
    public string GetActionData()
    {
        SetActionData();
        return actionData;
    }
}

