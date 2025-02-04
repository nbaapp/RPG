using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MagicUp status effect increases the magic of the target unit by a set percentage
public class MagicUp : StatusEffect
{
    [SerializeField]
    private double magicIncrease = 0.2;

    public MagicUp(double magicIncrease = 0.2, int duration = 3) : base("Magic Up", duration)
    {
        this.magicIncrease = magicIncrease;
    }

    public override void ApplyEffect(Unit target)
    {
        target.SetMagic((int)(target.GetMagic() + target.GetMagic() * magicIncrease));
    }

    public override void RemoveEffect(Unit target)
    {
        target.SetMagic(target.GetBaseAttributes().magic);
    }
}
