using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect
{
    public string name;
    public int duration;
    public int maxDuration = 3;

    public static Dictionary<string, StatusEffect> stringToStatusEffect = new Dictionary<string, StatusEffect>
    {
        {"AttackUp", new AttackUp()},
        {"DefenseUp", new DefenseUp()},
        {"MagicUp", new MagicUp()},
        {"MagicDefenseUp", new MagicDefenseUp()},
        {"SpeedUp", new SpeedUp()},
        {"AttackDown", new AttackDown()},
        {"DefenseDown", new DefenseDown()},
        {"MagicDown", new MagicDown()},
        {"MagicDefenseDown", new MagicDefenseDown()},
        {"SpeedDown", new SpeedDown()},
    };

    public StatusEffect(string name, int duration = 3)
    {
        this.name = name;
        this.duration = duration;
    }

    public abstract void ApplyEffect(Unit target);
    public abstract void RemoveEffect(Unit target);

    public void Tick(Unit target)
    {
        duration--;
        if (duration <= 0)
        {
            RemoveEffect(target);
        }
    }
    public void ResetDuration()
    {
        duration = maxDuration;
    }

    public static StatusEffect StringToStatusEffect(string name)
    {
        return stringToStatusEffect[name];
    }
}
