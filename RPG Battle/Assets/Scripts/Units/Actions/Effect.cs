using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect {
    public EffectType effectType;
    public int value;
    public int duration;

    //Dictionary to map effect type value to string name
    public static Dictionary<EffectType, string> effectTypeToString = new Dictionary<EffectType, string>
    {
        {EffectType.Damage, "Damage"},
        {EffectType.Heal, "Heal"},
        {EffectType.Buff, "Buff"},
        {EffectType.Debuff, "Debuff"}
    };
    public static Dictionary<string, EffectType> stringToEffectType = new Dictionary<string, EffectType>
    {
        {"Damage", EffectType.Damage},
        {"Heal", EffectType.Heal},
        {"Buff", EffectType.Buff},
        {"Debuff", EffectType.Debuff}
    };

    public Effect(EffectType effectType, int value, int duration) {
        this.effectType = effectType;
        this.value = value;
        this.duration = duration;
    }
    public EffectType GetEffectType() {
        return effectType;
    }
    public void SetEffectType(EffectType effectType) {
        this.effectType = effectType;
    }
    public int GetEffectValue() {
        return value;
    }
    public void SetEffectValue(int value) {
        this.value = value;
    }
    public int GetEffectDuration() {
        return duration;
    }
    public void SetEffectDuration(int duration) {
        this.duration = duration;
    }
    public static string EffectTypeToString(EffectType effectType) {
        return effectTypeToString[effectType];
    }
    public static EffectType StringToEffectType(string effectType) {
        return stringToEffectType[effectType];
    }


}
