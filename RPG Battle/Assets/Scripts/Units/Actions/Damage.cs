using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage {
    public DamageType damageType;
    public int value;

    //Dictionary to map effect type value to string name
    public static Dictionary<DamageType, string> damageTypeToString = new Dictionary<DamageType, string>
    {
        {DamageType.Damage, "Damage"},
        {DamageType.Heal, "Heal"},
        {DamageType.None, "None"},
    };
    public static Dictionary<string, DamageType> stringToDamageType = new Dictionary<string, DamageType>
    {
        {"Damage", DamageType.Damage},
        {"Heal", DamageType.Heal},
        {"None", DamageType.None},
    };

    public Damage(DamageType damageType, int value) {
        this.damageType = damageType;
        this.value = value;
    }
    public DamageType GetDamageType() {
        return damageType;
    }
    public void SetDamageType(DamageType damageType) {
        this.damageType = damageType;
    }
    public int GetEffectValue() {
        return value;
    }
    public void SetEffectValue(int value) {
        this.value = value;
    }

    public static string DamageTypeToString(DamageType damageType) {
        return damageTypeToString[damageType];
    }
    public static DamageType StringToDamageType(string damageType) {
        return stringToDamageType[damageType];
    }
    public override string ToString() {
        string str = "";
        switch (damageType) {
            case DamageType.Damage:
                str = "Damage: " + value;
                break;
            case DamageType.Heal:
                str = "Heal: " + value;
                break;
            case DamageType.None:
                str = "No Damage";
                break;
        }
        return str;
    }
}

public enum DamageType
{
    Damage,
    Heal,
    None
}
