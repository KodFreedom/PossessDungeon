using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class PanelController : MonoBehaviour
    {
        /////////////////////////////////////////////////////////////////////////
        // 定数宣言
        /////////////////////////////////////////////////////////////////////////
        [SerializeField] float kLerpSpeed = 0.2f;
        [SerializeField] float kMinScale = 0.01f;

        /////////////////////////////////////////////////////////////////////////
        // 変数宣言
        /////////////////////////////////////////////////////////////////////////
        private bool is_alive = false;

        /////////////////////////////////////////////////////////////////////////
        // Public関数
        /////////////////////////////////////////////////////////////////////////
        public void Disable()
        {
            is_alive = false;
        }

        /////////////////////////////////////////////////////////////////////////
        // Private関数
        /////////////////////////////////////////////////////////////////////////
        // 更新処理
        private void Update()
        {
            if (is_alive)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, kLerpSpeed);
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, kLerpSpeed);
                if (transform.localScale.sqrMagnitude <= kMinScale * kMinScale)
                {
                    gameObject.SetActive(false);
                }
            }
        }

        // Enableされた時の処理
        private void OnEnable()
        {
            is_alive = true;
            transform.localScale = Vector3.zero;
        }
    }
}