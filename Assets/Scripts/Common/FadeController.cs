using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

namespace ProjectBaka
{
    public class FadeController : MonoBehaviour
    {
        /////////////////////////////////////////////////////////////////////////
        // 列挙型宣言
        /////////////////////////////////////////////////////////////////////////
        private enum FadeStatus
        {
            kFadeNone = 0,
            kFadeIn,
            kFadeOut
        }

        /////////////////////////////////////////////////////////////////////////
        // 定数宣言
        /////////////////////////////////////////////////////////////////////////
        private const float kFadeTime = 0.5f;

        /////////////////////////////////////////////////////////////////////////
        // 変数宣言
        /////////////////////////////////////////////////////////////////////////
        private FadeStatus status_ = FadeStatus.kFadeNone;
        private Image image_ = null;

        /////////////////////////////////////////////////////////////////////////
        // Public関数
        /////////////////////////////////////////////////////////////////////////
        /// <summary> FadeOut開始関数（SceneManagerが呼ぶ） </summary>
        public void StartFadeOut()
        {
            status_ = FadeStatus.kFadeOut;
        }

        /// <summary> FadeIn開始関数（SceneManagerが呼ぶ） </summary>
        public void StartFadeIn()
        {
            status_ = FadeStatus.kFadeIn;
        }

        /////////////////////////////////////////////////////////////////////////
        // Private関数
        /////////////////////////////////////////////////////////////////////////
        // 初期化処理
        private void Start()
        {
            status_ = FadeStatus.kFadeIn;

            // Image Componentを探す
            image_ = GetComponentInChildren<Image>();
            Assert.IsNotNull(image_, "FadeController: Image Componentが見つからない！");

            // SortOrderを1000にする
            image_.canvas.sortingOrder = 1000;

            // ColorのAlpha値を1にする
            image_.color = new Color(0f, 0f, 0f, 1f);
        }

        // 更新処理
        private void Update()
        {
            switch (status_)
            {
                case FadeStatus.kFadeNone:
                    break;
                case FadeStatus.kFadeIn:
                    FadeIn();
                    break;
                case FadeStatus.kFadeOut:
                    FadeOut();
                    break;
                default:
                    break;
            }
        }

        // Fade in処理
        private void FadeIn()
        {
            var color = image_.color;
            color.a -= Time.unscaledDeltaTime * (1.0f / kFadeTime);
            if (color.a <= 0f)
            {
                // Fade in処理終了
                color.a = 0f;
                status_ = FadeStatus.kFadeNone;
            }
            image_.color = color;
        }

        // Fade out処理
        private void FadeOut()
        {
            var color = image_.color;
            color.a += Time.unscaledDeltaTime * (1.0f / kFadeTime);
            if (color.a >= 1f)
            {
                // Fade out処理終了
                color.a = 1f;
                status_ = FadeStatus.kFadeNone;

                // シーンの切り替え
                SceneManager.Instance.StartChange();
            }
            image_.color = color;
        }
    }
}