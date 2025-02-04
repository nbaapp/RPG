using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AttackUp status effect increases the attack of the target unit by a set percentage
public class AttackUp : StatusEffect
{
    [SerializeField]
    private double attackIncrease = 0.2;

    public AttackUp(double attackIncrease = 0.2, int duration = 3) : base("Attack Up", duration)
    {
        this.attackIncrease = attackIncrease;
    }

    public override void ApplyEffect(Unit target)
    {
        target.SetAttack((int)(target.GetAttack() + target.GetAttack() * attackIncrease));
    }

    public override void RemoveEffect(Unit target)
    {
        target.SetAttack(target.GetBaseAttributes().attack);
    }
}
