using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseDown : StatusEffect
{
    [SerializeField]
    private double defenseDecrease = 0.2;

    public DefenseDown(double defenseDecrease = 0.2, int duration = 3) : base("Defense Down", duration)
    {
        this.defenseDecrease = defenseDecrease;
    }

    public override void ApplyEffect(Unit target)
    {
        target.SetDefense((int)(target.GetDefense() - target.GetDefense() * defenseDecrease));
    }

    public override void RemoveEffect(Unit target)
    {
        target.SetDefense(target.GetBaseAttributes().defense);
    }
}
