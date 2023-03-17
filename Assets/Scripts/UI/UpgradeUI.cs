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

    public void Init(string name, int cost)
    {
        title.text = name;
        costText.text = $"$ {cost}";
    }
}