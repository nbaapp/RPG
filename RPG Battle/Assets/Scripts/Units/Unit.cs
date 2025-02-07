using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected BattleSystem battleSystem;
    protected UnitAttributes baseAttributes;
    protected BattleHud battleHud;
    [SerializeField]
    protected string unitName;
    [SerializeField]
    protected string unitDescription;
    [SerializeField]
    protected int unitLevel;
    [SerializeField]
    protected int attack;
    [SerializeField]
    protected int magic;
    [SerializeField]
    protected int magicDefence;
    [SerializeField]
    protected int defense;
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int maxHP;
    [SerializeField]
    protected int currentHP;

    protected List<Action> actions;

    [SerializeField]
    protected List<StatusEffect> statusEffects;

    public bool isDead = false;

    void Start() {
        battleSystem = GameObject.Find("Battle System").GetComponent<BattleSystem>();
        battleHud = transform.parent.parent.GetComponent<BattleHud>();
        currentHP = maxHP;
        isDead = false;
        statusEffects = new List<StatusEffect>();
    }
    
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        battleHud.SetHP(currentHP);

        if(currentHP <= 0)
        {
            isDead = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Heal(int healAmount)
    {
        currentHP += healAmount;
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    //FIX AND MOVE (Probly)
    public Action SelectAction() {
        if (actions.Count > 0) {
            return actions[UnityEngine.Random.Range(0, actions.Count)];
        }
        return null;
    }

    public void ExecuteAction(Action action, List<Unit> targets) {
        foreach (Unit target in targets) {
            Debug.Log(unitName + " used " + action.name + " on " + target.unitName);
            foreach (StatusEffect effect in action.statusEffects) {
                target.ApplyStatusEffect(effect);
            }
            target.ApplyDamage(action.damage);
        }
    }

    public void ApplyDamage(Damage damage) {
        switch (damage.damageType) {
            case DamageType.Damage:
                TakeDamage(damage.value);
                Debug.Log(unitName + " took " + damage.value + " damage");
                break;
            case DamageType.Heal:
                Heal(damage.value);
                Debug.Log(unitName + " healed " + damage.value + " health");
                break;
            case DamageType.None:
                Debug.Log("Not a damaging move");
                break;
            default:
                Debug.Log("Invalid Damage Type");
                break;
        }
    }

    public void ApplyStatusEffect(StatusEffect statusEffect) {
        if (statusEffects == null) {
            statusEffects = new List<StatusEffect>();
        }
        if (statusEffects.Contains(statusEffect)) {
            StatusEffect status = statusEffects.Find(x => x.name == statusEffect.name);
            status.ResetDuration();
            return;
        }
        statusEffect.ApplyEffect(this);
        statusEffects.Add(statusEffect);
        battleHud.SetStatusText(StatusEffectsToString());
        Debug.Log(unitName + " has been affected by " + statusEffect.name);
    }

    public void RemoveStatusEffect(StatusEffect statusEffect) {
        if (statusEffects == null) {
            return;
        }
        if (!statusEffects.Contains(statusEffect)) {
            Debug.Log(unitName + " does not have " + statusEffect.name);
            return;
        }
        statusEffects.Remove(statusEffect);
        statusEffect.RemoveEffect(this);
        battleHud.SetStatusText(StatusEffectsToString());
        Debug.Log(unitName + " has been cured of " + statusEffect.name);
    }

    public void SetBaseValues(UnitAttributes attributes = null) {
        if (attributes == null) {
            Debug.Log("UnitAttributes is null");
            return;
        }
        UnitAttributes newAttributes = new UnitAttributes(attributes);
        baseAttributes = newAttributes;

        unitName = attributes.unitName;
        unitDescription = attributes.unitDescription;
        unitLevel = attributes.unitLevel;
        attack = attributes.attack;
        magic = attributes.magic;
        magicDefence = attributes.magicDefence;
        defense = attributes.defense;
        speed = attributes.speed;
        maxHP = attributes.maxHP;
        currentHP = attributes.maxHP;
    }
    public void SetActions(List<Action> actions) {
        this.actions = actions;
    }
    public List<Action> GetActions() {
        return actions;
    }
    public void addAction(Action action) {
        if (actions == null) {
            actions = new List<Action>();
        }
        actions.Add(action);
    }
    public void removeAction(Action action) {
        actions.Remove(action);
    }
    public string GetName() {
        return unitName;
    }
    public void SetName(string unitName) {
        this.unitName = unitName;
    }
    public string GetDescription() {
        return unitDescription;
    }
    public void SetDescription(string unitDescription) {
        this.unitDescription = unitDescription;
    }
    public int GetLevel() {
        return unitLevel;
    }
    public void SetLevel(int unitLevel) {
        this.unitLevel = unitLevel;
    }
    public int GetAttack() {
        return attack;
    }
    public void SetAttack(int attack) {
        this.attack = attack;
    }
    public int GetMagic() {
        return magic;
    }
    public void SetMagic(int magic) {
        this.magic = magic;
    }
    public int GetMagicDefence() {
        return magicDefence;
    }
    public void SetMagicDefence(int magicDefence) {
        this.magicDefence = magicDefence;
    }
    public int GetDefense() {
        return defense;
    }
    public void SetDefense(int defense) {
        this.defense = defense;
    }
    public int GetMaxHP() {
        return maxHP;
    }
    public int GetSpeed() {
        return speed;
    }
    public void SetSpeed(int speed) {
        this.speed = speed;
    }
    public void SetMaxHP(int maxHP) {
        this.maxHP = maxHP;
    }
    public int GetCurrentHP() {
        return currentHP;
    }
    public void SetCurrentHP(int currentHP) {
        this.currentHP = currentHP;
    }
    public UnitAttributes GetBaseAttributes() {
        return baseAttributes;
    }
    public List<StatusEffect> GetStatusEffects() {
        return statusEffects;
    }
    public string StatusEffectsToString() {
        string statusEffectsString = "";
        foreach (StatusEffect effect in statusEffects) {
            statusEffectsString += effect.name + ", ";
        }
        return statusEffectsString;
    }
    public Action GetAction(string actionName) {
        return actions.Find(x => x.name == actionName);
    }

}