using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class ExitButtonController : ButtonController
    {
        /////////////////////////////////////////////////////////////////////////
        // Public関数
        /////////////////////////////////////////////////////////////////////////
        public override void OnPress()
        {
            if (is_pressed) return;
            is_pressed = true;
            Application.Quit();
            Input.ResetInputAxes();
        }
    }
}