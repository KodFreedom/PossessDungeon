using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

namespace ProjectBaka
{
    public class SoulState : ActorState
    {
        public struct Parameter
        {
            public float max_soul_amount;
            public float soul_amount;
        }

        private Parameter parameter_ { get; set; }
        private RelyDetector rely_detector_ = null;
        private DragDetector drag_detector_ = null;
        private ItemController item_ = null;

        // input
        private Vector3 direction_ = Vector3.zero;
        private bool drag_and_drop_ = false;
        private bool rely_ = false;

        /// <summary>
        /// パラメーターの設定
        /// </summary>
        /// <param name="parameter"></param>
        public void SetParameter(Parameter parameter)
        {
            parameter_ = parameter;
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="actor_controller"></param>
        public override void Init(ActorController actor_controller)
        {
            FreeLookCam camera = Camera.main.GetComponentInParent<FreeLookCam>();
            if (camera != null)
            {
                camera.SetTarget(transform);
            }

            gameObject.tag = "Player";

            var rely_detector_object = new GameObject("RelyDetector");
            rely_detector_object.transform.SetParent(transform);
            rely_detector_ = rely_detector_object.AddComponent<RelyDetector>();

            var drag_detector_object = new GameObject("DragDetector");
            drag_detector_object.transform.SetParent(transform);
            drag_detector_ = drag_detector_object.AddComponent<DragDetector>();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <param name="actor_controller"></param>
        public override void Uninit(ActorController actor_controller)
        {
            Destroy(rely_detector_.gameObject);
            Destroy(drag_detector_.gameObject);
        }

        /// <summary>
        /// 行動処理
        /// </summary>
        /// <param name="actor_controller"></param>
        public override void Act(ActorController actor_controller)
        {
            UpdateInput();
            Move(actor_controller);
            RelyOnTarget(actor_controller);
            DragAndDropItem(actor_controller);
        }

        private void UpdateInput()
        {
            // 入力情報の取得
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            rely_ = Input.GetButtonDown("Fire1");
            drag_and_drop_ = Input.GetButtonDown("Fire2");

            // カメラの方向に向かって移動する
            Camera main_camera = Camera.main;
            var forward = Vector3.Scale(main_camera.transform.forward, new Vector3(1f, 0f, 1f)).normalized;
            direction_ = vertical * forward + horizontal * main_camera.transform.right;
        }

        private void Move(ActorController actor_controller)
        {
            float direction_magnitude = direction_.magnitude;

            if (direction_magnitude == 0.0f)
                return;

            var movement = Vector3.ProjectOnPlane(transform.forward, Vector3.up) * direction_magnitude * actor_controller.MoveSpeed;
            transform.position += movement * Time.deltaTime;

            // 物理演算の時の回転を切ったのため直接にtransformで回転する
            direction_ = transform.InverseTransformDirection(direction_);
            var turn_amount = Mathf.Atan2(direction_.x, direction_.z);
            transform.Rotate(0f, turn_amount * actor_controller.TurnSpeed * Time.deltaTime, 0f);
        }

        // 乗り移る処理
        private void RelyOnTarget(ActorController actor_controller)
        {
            if (!rely_ || rely_detector_.Target == null)
                return;

            // Target
            var target_controller = rely_detector_.Target.GetComponent<ActorController>();
            target_controller.SetBrainType(ActorController.BrainType.kPlayer);
            var target_soul_state = rely_detector_.Target.AddComponent<SoulState>();
            target_soul_state.parameter_ = parameter_;
            target_controller.ChangeState(target_soul_state);

            // This
            gameObject.tag = "Npc";
            if (item_ != null)
            {// Drop
                item_.DropBy(actor_controller);
                item_ = null;
            }
            actor_controller.SetBrainType(ActorController.BrainType.kNPC);
            switch (actor_controller.GetActorType())
            {
                case ActorController.ActorType.kInnsmouth:
                    actor_controller.ChangeState(gameObject.AddComponent<NpcDemoState>());
                    break;
                case ActorController.ActorType.kGolem:
                    actor_controller.ChangeState(gameObject.AddComponent<NpcDemoState>());
                    break;
                case ActorController.ActorType.kCarbuncle:
                    actor_controller.ChangeState(gameObject.AddComponent<NpcDemoState>());
                    break;
                case ActorController.ActorType.kSoul:
                    actor_controller.ChangeState(gameObject.AddComponent<NpcDemoState>());
                    break;
                default:
                    break;
            }
        }

        // アイテム取る処理
        // TODO : soulのstateに書く
        private void DragAndDropItem(ActorController actor_controller)
        {
            if (drag_and_drop_ == false)
                return;

            if (item_ != null)
            {// Drop
                item_.DropBy(actor_controller);
                item_ = null;
            }
            else if (drag_detector_.Target != null)
            {// Drag
                var item_controller = drag_detector_.Target.GetComponent<ItemController>();
                if (item_controller.DragBy(actor_controller))
                {
                    item_ = item_controller;
                }
            }
        }
    }
}
