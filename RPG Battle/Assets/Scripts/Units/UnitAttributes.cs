using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttributes {
    public string unitName;
    public string unitDescription;
    public int unitLevel;
    public int attack;
    public int magic;
    public int magicDefence;
    public int defense;
    public int speed;
    public int maxHP;

    public UnitAttributes() {
        unitName = "Knight";
        unitDescription = "Knight in shining armor";
        unitLevel = 1;
        attack = 1;
        magicDefence = 1;
        defense = 1;
        maxHP = 1;
    }

    public UnitAttributes(string name, string description, int level, int attack, int magicDefence, int defense, int speed, int maxHP) {
        unitName = name;
        unitDescription = description;
        unitLevel = level;
        this.attack = attack;
        this.magicDefence = magicDefence;
        this.defense = defense;
        this.speed = speed;
        this.maxHP = maxHP;
    }

    public UnitAttributes(UnitAttributes attributes) {
        unitName = attributes.unitName;
        unitDescription = attributes.unitDescription;
        unitLevel = attributes.unitLevel;
        attack = attributes.attack;
        magicDefence = attributes.magicDefence;
        defense = attributes.defense;
        speed = attributes.speed;
        maxHP = attributes.maxHP;
    }
}
