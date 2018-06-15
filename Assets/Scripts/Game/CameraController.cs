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

        private enum RotationMode
        {
            kRaise,
            kWait,
            kFall,
        }

        [SerializeField] Vector3 kOffset = Vector3.zero;
        [SerializeField, Range(0f, 1f)] float kLerpOverDistance = 0.01f;
        [SerializeField] float kDistance = 5f;
        [SerializeField] float kMoveSpeed = 1f;
        [SerializeField] float kMaxRotationX = 90f;
        [SerializeField] float kRotationSpeed = 30f;
        [SerializeField] float kWaitTime = 1f;
        [SerializeField] bool kLockCursor = false;

        private Transform target_ = null;
        private Vector3 target_position_ = Vector3.zero;
        private CameraMode mode_ = CameraMode.kLerp;
        private RotationMode rotation_mode_ = RotationMode.kWait;
        private float default_rotation_x_ = 0f;
        private float wait_time_ = 0f;
        private int default_layer_ = 0;

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
            default_rotation_x_ = transform.rotation.eulerAngles.x;
            default_layer_ = 1 << LayerMask.NameToLayer("Default");
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void LateUpdate()
        {
            FollowTarget();
            AvoidGimic();
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

            transform.position = target_position_ + kOffset + transform.forward * -1f * kDistance;
        }

        private void AvoidGimic()
        {
            if (target_ == null) return;

            if(wait_time_ > 0f)
            {
                wait_time_ -= Time.deltaTime;
            }

            float new_rotation_x = CalculateRotation();

            // 新しい回転と位置でraycastする
            Ray ray = new Ray();
            Vector3 forward = Quaternion.Euler(new_rotation_x, 0f, 0f) * Vector3.forward;
            ray.origin = target_position_ + kOffset + forward * -1f * kDistance;
            ray.direction = forward;
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * kDistance, Color.yellow);
            if (Physics.Raycast(ray, kDistance, default_layer_))
            {// 当たった場合
                print("hit new");
                switch(rotation_mode_)
                {
                    case RotationMode.kWait:
                        {// 待ち状態なら上昇
                            rotation_mode_ = RotationMode.kRaise;
                            break;
                        }
                    case RotationMode.kRaise:
                        {// 上昇状態なら何もしない
                            break;
                        }
                    case RotationMode.kFall:
                        {// 降下状態なら止まる
                         // そして新しい回転をなしにする
                            wait_time_ = kWaitTime;
                            rotation_mode_ = RotationMode.kWait;
                            new_rotation_x = transform.rotation.eulerAngles.x;
                            break;
                        }
                }
            }
            else
            {// 当たってない場合
                switch (rotation_mode_)
                {
                    case RotationMode.kWait:
                        {// 待ち状態なら降下状態にする
                            if(wait_time_ <= 0f)
                            {
                                rotation_mode_ = RotationMode.kFall;
                            }
                            break;
                        }
                    case RotationMode.kRaise:
                        {// 上昇状態なら止まる
                            wait_time_ = kWaitTime;
                            rotation_mode_ = RotationMode.kWait;
                            break;
                        }
                    case RotationMode.kFall:
                        {// 降下状態なら何もしない
                            break;
                        }
                }
            }

            new_rotation_x = Mathf.Lerp(transform.rotation.eulerAngles.x, new_rotation_x, 0.5f);
            transform.rotation = Quaternion.Euler(new_rotation_x, 0f, 0f);
        }

        private float CalculateRotation()
        {
            float rotation_x = transform.rotation.eulerAngles.x;

            if (rotation_mode_ == RotationMode.kRaise)
            {
                rotation_x = Mathf.Clamp(rotation_x + kRotationSpeed * Time.deltaTime, rotation_x, kMaxRotationX);
            }
            else if(rotation_mode_ == RotationMode.kFall)
            {
                rotation_x = Mathf.Clamp(rotation_x - kRotationSpeed * Time.deltaTime, default_rotation_x_, rotation_x);
            }

            return rotation_x;
        }
    }
}