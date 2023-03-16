using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TheSTAR.Sound;

public class GameController : MonoBehaviour
{
    [Inject] private SoundController sounds;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        sounds.Init();
        sounds.PlayMusic(MusicType.MainMenuTheme);
    }
}