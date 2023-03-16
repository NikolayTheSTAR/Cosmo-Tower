using System;
using System.Collections.Generic;
using TheSTAR.Utility.Pointer;
using UnityEngine;
using TheSTAR.GUI;

namespace TheSTAR.GUI.Screens
{
    public class GameScreen : GuiScreen
    {
        [SerializeField] private PointerButton exitButton;
        [SerializeField] private CanvasGroup canvasGroup;

        private Sound.SoundController _sound;

        public event Action AnimateExitGameEvent;
        public event Action DoExitGameEvent;

        public void Init(Sound.SoundController sound)
        {
            _sound = sound;

            exitButton.Init(OnExitButtonClick);
        }

        private void OnExitButtonClick()
        {
            AnimateExitGameEvent?.Invoke();

            Hide(DoExitGameEvent);
            _sound.StopMusic(Sound.MusicChangeType.Volume);
        }

        protected override void AnimateShow(out int time)
        {
            canvasGroup.alpha = 0;
            LeanTween.alphaCanvas(canvasGroup, 1, AnimateTime);
            time = (int)(AnimateTime * 1000);
        }

        protected override void AnimateHide(out int time)
        {
            canvasGroup.alpha = 1;
            LeanTween.alphaCanvas(canvasGroup, 0, AnimateTime);
            time = (int)(AnimateTime * 1000);
        }

        protected override void OnShow() => _sound.PlayMusic(Sound.MusicType.BattleTheme);
    }
}