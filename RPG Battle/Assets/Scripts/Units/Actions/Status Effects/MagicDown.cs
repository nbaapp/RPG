using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MagicUp status effect increases the magic of the target unit by a set percentage
public class MagicDown : StatusEffect
{
    [SerializeField]
    private double magicDecrease = 0.2;

    public MagicDown(double magicDecrease = 0.2, int duration = 3) : base("Magic Down", duration)
    {
        this.magicDecrease = magicDecrease;
    }

    public override void ApplyEffect(Unit target)
    {
        target.SetMagic((int)(target.GetMagic() - target.GetMagic() * magicDecrease));
    }

    public override void RemoveEffect(Unit target)
    {
        target.SetMagic(target.GetBaseAttributes().magic);
    }
}
