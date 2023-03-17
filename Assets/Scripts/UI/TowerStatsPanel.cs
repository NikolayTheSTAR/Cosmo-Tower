using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerStatsPanel : MonoBehaviour, IUpgradeReactable, IHpReactable
{
    [SerializeField] private TextMeshProUGUI attackStatText;
    [SerializeField] private TextMeshProUGUI hpCounter;
    [SerializeField] private HpBar hpBar;

    public void OnChangeHpReact(HpOwner hpOwner)
    {
        hpBar.SetHp(hpOwner.CurrentHp, hpOwner.MaxHp);
        hpCounter.text = $"{hpOwner.CurrentHp}/{hpOwner.MaxHp}";
    }

    public void OnUpgradeReact(UpgradeType upgradeType, float value)
    {
        if (upgradeType != UpgradeType.Damage) return;

        attackStatText.text = value.ToString();
    }
}