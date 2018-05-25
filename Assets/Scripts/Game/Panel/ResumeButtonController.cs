using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class ResumeButtonController : ButtonController
    {
        /////////////////////////////////////////////////////////////////////////
        // Public関数
        /////////////////////////////////////////////////////////////////////////
        public override void OnPress()
        {
            if (is_pressed) return;
            is_pressed = true;
            GameFlowController.Instance.Resume();
        }
    }
}