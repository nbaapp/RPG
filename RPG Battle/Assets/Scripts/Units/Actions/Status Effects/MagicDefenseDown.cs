using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MagicDefenceUp status effect increases the magic defence of the target unit by a set percentage
public class MagicDefenseDown : StatusEffect
{
    [SerializeField]
    private double defenseDecrease = 0.2;

    public MagicDefenseDown(double defenseDecrease = 0.2, int duration = 3) : base("Magic Defence Down", duration)
    {
        this.defenseDecrease = defenseDecrease;
    }

    public override void ApplyEffect(Unit target)
    {
        target.SetMagicDefence((int)(target.GetMagicDefence() - target.GetMagicDefence() * defenseDecrease));
    }

    public override void RemoveEffect(Unit target)
    {
        target.SetMagicDefence(target.GetBaseAttributes().magicDefence);
    }
}
