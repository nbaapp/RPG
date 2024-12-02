using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    protected string unitName;
    [SerializeField]
    protected string unitDescription;
    [SerializeField]
    protected int unitLevel;
    [SerializeField]
    protected int attack;
    [SerializeField]
    protected int magicDefence;
    [SerializeField]
    protected int defense;
    [SerializeField]
    protected int maxHP;
    [SerializeField]
    protected int currentHP;

    protected List<Action> actions;

    public bool isDead = false;

    void Start() {
        currentHP = maxHP;
        isDead = false;
    }
    
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

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
            return actions[Random.Range(0, actions.Count)];
        }
        return null;
    }

    public void ExecuteAction(Action action, List<Unit> targets) {
        foreach (Unit target in targets) {
            foreach (Effect effect in action.effects) {
                target.ApplyEffect(effect);
            }
        }
    }

    public void ApplyEffect(Effect effect) {
        switch (effect.GetEffectType()) {
            case EffectType.Damage:
                TakeDamage(effect.GetEffectValue());
                break;
            case EffectType.Heal:
                Heal(effect.GetEffectValue());
                break;
            case EffectType.Buff:
                break;
            case EffectType.Debuff:
                break;
        }
    }

    public void SetValues(UnitAttributes attributes) {
        unitName = attributes.unitName;
        unitDescription = attributes.unitDescription;
        unitLevel = attributes.unitLevel;
        attack = attributes.attack;
        magicDefence = attributes.magicDefence;
        defense = attributes.defense;
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
    public void SetMaxHP(int maxHP) {
        this.maxHP = maxHP;
    }
    public int GetCurrentHP() {
        return currentHP;
    }
    public void SetCurrentHP(int currentHP) {
        this.currentHP = currentHP;
    }


}