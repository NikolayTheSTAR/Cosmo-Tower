using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TheSTAR.Sound;
using TheSTAR.GUI;
using TheSTAR.GUI.Screens;

public class GameController : MonoBehaviour
{
    [Inject] private readonly SoundController sounds;
    [Inject] private readonly GuiController gui;
    [Inject] private readonly GameWorld world;
    [Inject] private readonly CurrencyController currency;

    public event Action StartBattleEvent;
    public event Action ExitBattleEvent;
    public event Action BattleLostEvent;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        sounds.Init();
        gui.Init(out var trs);
        world.Init();
        currency.Init(trs);
    }

    public void StartBattle() => StartBattleEvent?.Invoke();
    public void ExitBattle() => ExitBattleEvent.Invoke();
    public void BattleLost() => BattleLostEvent.Invoke();
}