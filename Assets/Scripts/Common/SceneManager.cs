using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace ProjectBaka
{
    public class SceneManager : MonoBehaviour
    {
        /////////////////////////////////////////////////////////////////////////
        // シングルトーンインスタンス
        /////////////////////////////////////////////////////////////////////////
        private static SceneManager instance_ = null;
        public static SceneManager Instance { get { return instance_; } }

        /////////////////////////////////////////////////////////////////////////
        // 定数宣言
        /////////////////////////////////////////////////////////////////////////
        private const float kWaitTime = 0.5f;

        /////////////////////////////////////////////////////////////////////////
        // 変数宣言
        /////////////////////////////////////////////////////////////////////////
        private FadeController fade_controller_ = null;
        private string next_scene_name_ = string.Empty;
        private float time_counter_ = 0f;

        /////////////////////////////////////////////////////////////////////////
        // Public関数
        /////////////////////////////////////////////////////////////////////////
        /// <summary> Fadeでシーンを切り替える </summary>
        public void ChangeScene(string next_scene_name)
        {
            if (next_scene_name_.Length != 0) return;
            next_scene_name_ = next_scene_name;
            fade_controller_.StartFadeOut();
        }

        /// <summary> シーンの切り替えの開始（FadeControllerが呼ぶ） </summary>
        public void StartChange()
        {
            time_counter_ = kWaitTime;
        }

        /////////////////////////////////////////////////////////////////////////
        // Private関数
        /////////////////////////////////////////////////////////////////////////
        // 初期化処理
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

            // FadeControllerを探す
            fade_controller_ = gameObject.GetComponentInChildren<FadeController>();
            Assert.IsNotNull(fade_controller_, "SceneManager: FadeControllerが見つからない！");

            // シーン遷移の時破棄しない
            DontDestroyOnLoad(gameObject);

            // 最初のBGMを再生
            //SoundPlayer.Instance.PlayBGM(SoundBGM.Title);
        }

        // 更新処理
        private void Update()
        {
            if (0 == next_scene_name_.Length) return;
            if (time_counter_ > 0f)
            {
                time_counter_ -= Time.unscaledDeltaTime;
                if (time_counter_ <= 0f)
                {
                    // Scene遷移
                    ChangeBgm(next_scene_name_);
                    UnityEngine.SceneManagement.SceneManager.LoadScene(next_scene_name_);
                    next_scene_name_ = string.Empty;
                    Time.timeScale = 1f;
                    fade_controller_.StartFadeIn();
                }
            }
        }

        // BGMの変更
        private void ChangeBgm(string next_scene_name_)
        {
            if (next_scene_name_ == "Title")
            {
                //SoundPlayer.Instance.PlayBGM(SoundBGM.Title);
                return;
            }
            if (next_scene_name_ == "Game")
            {
                //SoundPlayer.Instance.PlayBGM(SoundBGM.Game);
                return;
            }
        }
    }
}