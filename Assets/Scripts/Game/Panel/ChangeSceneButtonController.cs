using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class ChangeSceneButtonController : ButtonController
    {
        /////////////////////////////////////////////////////////////////////////
        // 定数宣言
        /////////////////////////////////////////////////////////////////////////
        [SerializeField] string kNextSceneName = string.Empty;

        /////////////////////////////////////////////////////////////////////////
        // Public関数
        /////////////////////////////////////////////////////////////////////////
        public override void OnPress()
        {
            if (is_pressed) return;
            is_pressed = true;
            OverrideEventSystem.Instance.SetSelectedObject(null);
            SceneManager.Instance.ChangeScene(kNextSceneName);
        }
    }
}