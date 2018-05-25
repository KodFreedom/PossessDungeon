using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

namespace ProjectBaka
{
    public class FlashButtonController : MonoBehaviour
    {
        /////////////////////////////////////////////////////////////////////////
        // 定数宣言
        /////////////////////////////////////////////////////////////////////////
        [SerializeField] float kNormalFlashingTime = 0.5f;
        [SerializeField] float kPushedFlashingTime = 0.1f;
        [SerializeField] float kWaitForChangeScene = 0.5f;
        [SerializeField] string kNextSceneName = string.Empty;

        /////////////////////////////////////////////////////////////////////////
        // 変数宣言
        /////////////////////////////////////////////////////////////////////////
        private float flashing_time_ = 0.0f;
        private float time_counter_ = 0.0f;
        private float time_counter_for_change_scene_ = 0.0f;
        private int   sign_ = 1;
        private Image image_ = null;

        /////////////////////////////////////////////////////////////////////////
        // Private関数
        /////////////////////////////////////////////////////////////////////////
        // 初期化処理
        private void Start()
        {
            // 変数初期化
            flashing_time_ = kNormalFlashingTime;
            time_counter_ = 0.0f;
            time_counter_for_change_scene_ = 0.0f;
            sign_ = 1;

            // Image Componentを探す
            image_ = GetComponentInChildren<Image>();
            Assert.IsNotNull(image_, "FlashButtonController: Image Componentが見つからない！");

            // ColorのAlpha値を0にする
            image_.color = new Color(1f, 1f, 1f, 0f);
        }

        // 更新処理
        private void Update()
        {
            // Alpha値更新
            float alpha = time_counter_ / flashing_time_;
            image_.color = new Color(1f, 1f, 1f, alpha);

            // 点滅カウント
            time_counter_ += sign_ * Time.deltaTime;

            // カウンターが0かフラッシング時間になったら符号を転換する
            if (time_counter_ >= flashing_time_
                || time_counter_ <= 0f)
            {
                sign_ *= -1;
            }

            // 表示までの遅延演出
            if (time_counter_for_change_scene_ > 0f)
            {
                time_counter_for_change_scene_ -= Time.deltaTime;
                if (time_counter_for_change_scene_ <= 0f)
                {
                    time_counter_for_change_scene_ = -1f;
                    SceneManager.Instance.ChangeScene(kNextSceneName);   
                }
                return;
            }

            // ボタン押す判定
            if (Input.GetButtonDown("Submit"))
            {
                // 点滅間隔更新
                flashing_time_ = kPushedFlashingTime;
                time_counter_ = alpha * flashing_time_;

                time_counter_for_change_scene_ = kWaitForChangeScene;

                // se再生
                //SoundPlayer.Instance.PlayOneShot(SoundSE.Select);
            }
        }
    }
}