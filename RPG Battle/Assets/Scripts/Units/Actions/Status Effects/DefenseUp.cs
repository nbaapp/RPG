using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DefenseUp status effect increases the defense of the target unit by a set percentage
public class DefenseUp : StatusEffect
{
    [SerializeField]
    private double defenseIncrease = 0.2;

    public DefenseUp(double defenseIncrease = 0.2, int duration = 3) : base("Defense Up", duration)
    {
        this.defenseIncrease = defenseIncrease;
    }

    public override void ApplyEffect(Unit target)
    {
        target.SetDefense((int)(target.GetDefense() + target.GetDefense() * defenseIncrease));
    }

    public override void RemoveEffect(Unit target)
    {
        target.SetDefense(target.GetBaseAttributes().defense);
    }
}
