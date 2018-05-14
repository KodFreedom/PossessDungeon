using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Vector3 offset_ = new Vector3(0f, 5f, 0f);
        [SerializeField] float move_speed_ = 1f;
        [SerializeField] bool lock_cursor_ = false;
        [SerializeField] Transform target_ = null;
        private Vector3 target_position_ = Vector3.zero;

        /// <summary>
        /// ターゲットの設定
        /// </summary>
        /// <param name="target"></param>
        public void SetTarget(Transform target)
        {
            target_ = target;
        }

        /// <summary>
        /// ターゲットの取得
        /// </summary>
        public Transform Target
        {
            get { return target_; }
        }

        private void Start()
        {
            Cursor.lockState = lock_cursor_ ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !lock_cursor_;
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void LateUpdate()
        {
            FollowTarget();
        }

        private void FollowTarget()
        {
            if (target_ == null) return;
            target_position_ = Vector3.Lerp(target_position_, target_.position, Time.deltaTime * move_speed_);
            transform.position = target_position_ + offset_;
        }
    }
}