using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectBaka
{
    public class OverrideEventSystem : MonoBehaviour
    {
        /////////////////////////////////////////////////////////////////////////
        // シングルトーンインスタンス
        /////////////////////////////////////////////////////////////////////////
        private static OverrideEventSystem instance_ = null;
        public static OverrideEventSystem Instance { get { return instance_; } }

        /////////////////////////////////////////////////////////////////////////
        // 変数宣言
        /////////////////////////////////////////////////////////////////////////
        private GameObject last_selected_ = null;

        /////////////////////////////////////////////////////////////////////////
        // Public関数
        /////////////////////////////////////////////////////////////////////////
        public void SetSelectedObject(GameObject game_object)
        {
            last_selected_ = game_object;
            EventSystem.current.SetSelectedGameObject(game_object);
        }

        /////////////////////////////////////////////////////////////////////////
        // Private関数
        /////////////////////////////////////////////////////////////////////////
        // 初期化
        private void Start()
        {
            // インスタンスが生成されてるかどうかをチェックする
            if (null == instance_)
            {
                // ないなら自分を渡す
                instance_ = this;
            }
            else
            {
                // すでにあるなら自分を破棄する
                DestroyImmediate(gameObject);
                return;
            }
        }

        // 更新処理
        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(last_selected_);
                return;
            }

            last_selected_ = EventSystem.current.currentSelectedGameObject;
        }
    }
}