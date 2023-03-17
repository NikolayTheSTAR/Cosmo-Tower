using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheSTAR.Utility.Pointer;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private PointerButton buyButton;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI costText;

    private UpgradeType _upgradeType;

    public void Init(UpgradeType upgradeType, string name)
    {
        _upgradeType = upgradeType;
        title.text = name;
    }

    public void Set(int cost)
    {
        costText.text = $"$ {cost}";
    }
}