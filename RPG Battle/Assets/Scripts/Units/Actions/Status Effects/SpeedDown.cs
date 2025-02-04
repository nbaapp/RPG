using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SpeedUp status effect increases the attack of the target unit by a set percentage
public class SpeedDown : StatusEffect
{
    [SerializeField]
    private double speedDecrease = 0.2;

    public SpeedDown(double speedDecrease = 0.2, int duration = 3) : base("Speed Down", duration)
    {
        this.speedDecrease = speedDecrease;
    }

    public override void ApplyEffect(Unit target)
    {
        target.SetSpeed((int)(target.GetSpeed() - target.GetSpeed() * speedDecrease));
    }

    public override void RemoveEffect(Unit target)
    {
        target.SetSpeed(target.GetBaseAttributes().speed);
    }
}
