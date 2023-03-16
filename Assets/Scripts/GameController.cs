using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TheSTAR.Sound;
using TheSTAR.GUI;

public class GameController : MonoBehaviour
{
    [Inject] private SoundController sounds;
    [Inject] private GuiController gui;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        sounds.Init();
        sounds.PlayMusic(MusicType.MainMenuTheme);

        gui.Init();
    }
}