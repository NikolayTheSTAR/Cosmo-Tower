using System;
using System.Collections.Generic;
using UnityEngine;
using TheSTAR.Utility.Pointer;

namespace TheSTAR.GUI.Screens
{
    public class ResultScreen : GuiScreen
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private PointerButton restartButton;
        [SerializeField] private PointerButton exitButton;

        private Sound.SoundController _sound;

        public void Init(Sound.SoundController sound, Action restartAction, Action exitAction)
        {
            _sound = sound;

            restartButton.Init(restartAction);
            exitButton.Init(exitAction);
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
    }
}