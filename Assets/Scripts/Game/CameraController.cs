using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class CameraController : MonoBehaviour
    {
        private enum CameraMode
        {
            kLerp,
            kDirect,
        }

        [SerializeField, Range(0f, 1f)] float kLerpOverDistance = 0.01f;
        [SerializeField] Vector3 kOffset = new Vector3(0f, 5f, 0f);
        [SerializeField] float kMoveSpeed = 1f;
        [SerializeField] bool kLockCursor = false;
        private Transform target_ = null;
        private Vector3 target_position_ = Vector3.zero;
        private CameraMode mode_ = CameraMode.kLerp;

        /// <summary>
        /// ターゲットの設定
        /// </summary>
        /// <param name="target"></param>
        public void SetTarget(Transform target)
        {
            target_ = target;
            mode_ = CameraMode.kLerp;
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
            Cursor.lockState = kLockCursor ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !kLockCursor;
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

            if(mode_ == CameraMode.kLerp)
            {
                target_position_ = Vector3.Lerp(target_position_, target_.position, Time.deltaTime * kMoveSpeed);
                if(Vector3.Distance(target_position_, target_.position) <= kLerpOverDistance)
                {
                    mode_ = CameraMode.kDirect;
                }
            }
            else
            {
                target_position_ = target_.position;
            }
            transform.position = target_position_ + kOffset;
        }
    }
}