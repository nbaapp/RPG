using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MagicDefenceUp status effect increases the magic defence of the target unit by a set percentage
public class MagicDefenseUp : StatusEffect
{
    [SerializeField]
    private double defenseIncrease = 0.2;

    public MagicDefenseUp(double defenseIncrease = 0.2, int duration = 3) : base("Magic Defence Up", duration)
    {
        this.defenseIncrease = defenseIncrease;
    }

    public override void ApplyEffect(Unit target)
    {
        target.SetMagicDefence((int)(target.GetMagicDefence() + target.GetMagicDefence() * defenseIncrease));
    }

    public override void RemoveEffect(Unit target)
    {
        target.SetMagicDefence(target.GetBaseAttributes().magicDefence);
    }
}
