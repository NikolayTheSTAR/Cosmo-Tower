using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheSTAR.Utility.Pointer;

namespace TheSTAR.GUI.Screens
{
    public class MenuScreen : GuiScreen
    {
        [SerializeField] private PointerButton playButton;

        public override void Init()
        {
            playButton.Init(OnPlayButtonClick);
        }

        private void OnPlayButtonClick()
        {
            Debug.Log("Play");
        }
    }
}