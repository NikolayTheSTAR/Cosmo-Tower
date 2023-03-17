using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheSTAR.Utility;

public class UpgradePanel : MonoBehaviour, IUpgradeReactable
{
    [SerializeField] private UpgradeUI[] upgrades = new UpgradeUI[0];

    private UpgradeController _upgradesController;

    public void Init(UpgradeController upgradesController)
    {
        _upgradesController = upgradesController;

        var upgradeTypes = EnumUtility.GetValues<UpgradeType>();

        UpgradeType upgradeType;
        UpgradeUI ui;
        UpgradeData data;
        for (int i = 0; i < upgradeTypes.Length; i++)
        {
            upgradeType = upgradeTypes[i];
            ui = upgrades[i];
            data = _upgradesController.GetUpgradeData(upgradeType);
            ui.Init(upgradeType, data.Name);
            ui.gameObject.SetActive(true);
        }
    }

    public void OnUpgradeReact(UpgradeType upgradeType, float value)
    {
        var ui = upgrades[(int)upgradeType];
        int cost = _upgradesController.GetNextUpgradeLevelData(upgradeType).Cost;

        ui.Set(cost);
    }
}