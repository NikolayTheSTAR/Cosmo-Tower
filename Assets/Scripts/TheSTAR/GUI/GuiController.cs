using System;
using UnityEngine;
using TheSTAR.Utility;
using UnityEngine.Serialization;
using TheSTAR.GUI.Screens;
using Zenject;

namespace TheSTAR.GUI
{
    public sealed class GuiController : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private GuiScreen[] screens = new GuiScreen[0];
        [SerializeField] private GuiScreen mainScreen;
        [SerializeField] private bool deactivateOtherScreensByStart = true;
        [SerializeField] private bool showMainScreenByStart = true;

        #endregion // Inspector

        [Inject] private readonly Sound.SoundController _sound;
        [Inject] private readonly GameController gameController;

        private GuiScreen currentScreen;

        public void Init()
        {
            for (int i = 0; i < screens.Length; i++)
            {
                var screen = screens[i];

                if (screen == null) continue;

                if (deactivateOtherScreensByStart && screen.gameObject.activeSelf) screen.gameObject.SetActive(false);
            }

            var menu = FindScreen<MenuScreen>();
            menu.Init(_sound);
            menu.StartGameEvent += gameController.StartGame;

            var game = FindScreen<GameScreen>();
            game.Init(_sound);
            game.AnimateExitGameEvent += gameController.AnimateExitGame;
            game.DoExitGameEvent += gameController.ExitGame;

            if (showMainScreenByStart && mainScreen != null) Show(mainScreen, false);

            // game events

            gameController.StartGameEvent += () => Show<GameScreen>();
            gameController.ExitGameEvent += () => Show<MenuScreen>();
        }

        public void Show<TScreen>(bool closeCurrentScreen = false, Action endAction = null) where TScreen : GuiScreen
        {
            GuiScreen screen = FindScreen<TScreen>();
            Show(screen, closeCurrentScreen, endAction);
        }

        public void Show<TScreen>(TScreen screen, bool closeCurrentScreen = false, Action endAction = null, bool skipShowAnim = false) where TScreen : GuiScreen
        {
            if (!screen) return;
            if (closeCurrentScreen && currentScreen) currentScreen.Hide();
            
            screen.Show(endAction, skipShowAnim);
            currentScreen = screen;
        }

        public T FindScreen<T>() where T : GuiScreen
        {
            int index = ArrayUtility.FastFindElement<GuiScreen, T>(screens);

            if (index == -1)
            {
                Debug.LogError($"Not found screen {typeof(T)}");
                return null;
            }
            else return (T)(screens[index]);
        }

        [ContextMenu("SortScreens")]
        private void SortScreens()
        {
            System.Array.Sort(screens);
        }
    }
}