using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class MinimapCameraController : MonoBehaviour
    {
        private Transform target_ = null;
        private Vector3 offset_ = Vector3.zero;

        /// <summary>
        /// ターゲットの設定
        /// </summary>
        /// <param name="target"></param>
        public void SetTarget(Transform target)
        {
            target_ = target;
        }

        private void Start()
        {
            offset_ = transform.position;
        }

        private void LateUpdate()
        {
            if (target_ == null) return;
            transform.position = target_.position + offset_;
        }
    }
}