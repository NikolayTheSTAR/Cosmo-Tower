using System;
using System.Collections.Generic;
using TheSTAR.Utility.Pointer;
using UnityEngine;
using TheSTAR.GUI;
using TMPro;

namespace TheSTAR.GUI.Screens
{
    public class GameScreen : GuiScreen, ITransactionReactable
    {
        [SerializeField] private PointerButton exitButton;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI coinsCounter;
        [SerializeField] private PointerButton showHideUpgradesButton;
        [SerializeField] private UpgradePanel upgradePanel;

        private Sound.SoundController _sound;

        public void Init(Sound.SoundController sound, Action exitAction)
        {
            _sound = sound;

            exitButton.Init(exitAction);

            showHideUpgradesButton.Init(OnShowHideUpgradesButtonClick);
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

        public void OnTransactionReact(CurrencyType itemType, int finalValue)
        {
            coinsCounter.text = $"$ {finalValue}";
        }

        private void OnShowHideUpgradesButtonClick()
        {
            upgradePanel.gameObject.SetActive(!upgradePanel.gameObject.activeSelf);
        }
    }
}