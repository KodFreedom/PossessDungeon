using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace ProjectBaka
{
    public class GameFlowController : MonoBehaviour
    {
        /////////////////////////////////////////////////////////////////////////
        // シングルトーンインスタンス
        /////////////////////////////////////////////////////////////////////////
        private static GameFlowController instance_ = null;
        public static GameFlowController Instance { get { return instance_; } }

        /////////////////////////////////////////////////////////////////////////
        // 定数宣言
        /////////////////////////////////////////////////////////////////////////
        [SerializeField] PanelController kPausePanel = null;
        [SerializeField] PanelController kGameClearPanel = null;
        [SerializeField] PanelController kGameOverPanel = null;

        /////////////////////////////////////////////////////////////////////////
        // Public関数
        /////////////////////////////////////////////////////////////////////////
        /// <summary> ポーズから戻る処理 </summary>
        public void Resume()
        {
            kPausePanel.Disable();
            Time.timeScale = 1f;
            OverrideEventSystem.Instance.SetSelectedObject(null);
            Input.ResetInputAxes();
        }

        /// <summary> ゲームクリア処理 </summary>
        public void GameClear()
        {
            if (kPausePanel.gameObject.activeInHierarchy
                || kGameClearPanel.gameObject.activeInHierarchy
                || kGameOverPanel.gameObject.activeInHierarchy)
            {
                return;
            }

            kGameClearPanel.gameObject.SetActive(true);
            Time.timeScale = 0f;
            OverrideEventSystem.Instance.SetSelectedObject(kGameClearPanel.transform.Find("Title").gameObject);
        }

        /// <summary> ゲームオーバー処理 </summary>
        public void GameOver()
        {
            if (kPausePanel.gameObject.activeInHierarchy
                || kGameClearPanel.gameObject.activeInHierarchy
                || kGameOverPanel.gameObject.activeInHierarchy)
            {
                return;
            }

            kGameOverPanel.gameObject.SetActive(true);
            Time.timeScale = 0f;
            OverrideEventSystem.Instance.SetSelectedObject(kGameOverPanel.transform.Find("Retry").gameObject);
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

            Assert.IsNotNull(kPausePanel, "GameFlowController: PausePanelが見つからない！");
            Assert.IsNotNull(kGameClearPanel, "GameFlowController: GameClearPanelが見つからない！");
            Assert.IsNotNull(kGameOverPanel, "GameFlowController: GameOverPanelが見つからない！");
        }

        // 更新処理
        private void Update()
        {
            //if (Input.GetButtonDown("Jump"))
            //{
            //    if (kPausePanel.gameObject.activeInHierarchy)
            //    {
            //        Resume();
            //    }
            //    else
            //    {
            //        Pause();
            //    }
            //}
        }

        // ポーズ処理
        private void Pause()
        {
            kPausePanel.gameObject.SetActive(true);
            Time.timeScale = 0f;
            OverrideEventSystem.Instance.SetSelectedObject(kPausePanel.transform.Find("Resume").gameObject);
        }
    }
}