using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AttackDown status effect decreases the attack of the target unit by a set percentage
public class AttackDown : StatusEffect
{
    [SerializeField]
    private double attackDecrease = 0.2;

    public AttackDown(double attackDecrease = 0.2, int duration = 3) : base("Attack Down", duration)
    {
        this.attackDecrease = attackDecrease;
    }

    public override void ApplyEffect(Unit target)
    {
        target.SetAttack((int)(target.GetAttack() - target.GetAttack() * attackDecrease));
    }

    public override void RemoveEffect(Unit target)
    {
        target.SetAttack(target.GetBaseAttributes().attack);
    }
}
