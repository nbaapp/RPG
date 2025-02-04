using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SpeedUp status effect increases the attack of the target unit by a set percentage
public class SpeedUp : StatusEffect
{
    [SerializeField]
    private double speedIncrease = 0.2;

    public SpeedUp(double speedIncrease = 0.2, int duration = 3) : base("Speed Up", duration)
    {
        this.speedIncrease = speedIncrease;
    }

    public override void ApplyEffect(Unit target)
    {
        target.SetSpeed((int)(target.GetSpeed() + target.GetSpeed() * speedIncrease));
    }

    public override void RemoveEffect(Unit target)
    {
        target.SetSpeed(target.GetBaseAttributes().speed);
    }
}
