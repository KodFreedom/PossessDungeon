using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBaka
{
    public class SoulGaugeController : MonoBehaviour
    {
        [SerializeField] Image blue_gauge_ = null;
        [SerializeField] Image red_gauge_ = null;
        [SerializeField, Range(0f, 1f)] float life_rate_ = 0f;
        private Animator ui_animator_ = null;

        // シングルトーンインスタンスの生成
        private static SoulGaugeController instance_ = null;
        public static SoulGaugeController Instance { get { return instance_; } }

        public void SetLifeRate(float life_rate)
        {
            life_rate_ = Mathf.Max(0f, life_rate);
        }

        // Use this for initialization
        void Start()
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

            ui_animator_ = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            blue_gauge_.fillAmount = life_rate_;
            red_gauge_.fillAmount = 1f - life_rate_;
            ui_animator_.SetFloat("LifeRate", life_rate_);
        }
    }
}