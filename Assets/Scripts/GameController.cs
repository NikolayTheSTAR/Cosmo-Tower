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

    public event Action StartGameEvent;
    public event Action AnimateExitGameEvent;
    public event Action ExitGameEvent;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        sounds.Init();
        gui.Init();
        world.Init();
    }

    public void StartGame() => StartGameEvent?.Invoke();
    public void AnimateExitGame() => AnimateExitGameEvent.Invoke();
    public void ExitGame() => ExitGameEvent.Invoke();
}