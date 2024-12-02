using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHud : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider hpSlider;

    public void SetHud(Unit unit)
    {
        nameText.text = unit.GetName();
        levelText.text = "Lv " + unit.GetLevel();
        hpSlider.maxValue = unit.GetMaxHP();
        hpSlider.value = unit.GetCurrentHP();
    }

    public void setHP(int hp)
    {
        hpSlider.value = hp;
    }

}
