﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class SoulState : ActorState
    {
        public static readonly float kMaxSoulAmount = 2000f;

        private static ActorController soul_ = null;
        private float soul_amount_ = 0f;
        private RelyDetector rely_detector_ = null;

        // input
        private Vector3 direction_ = Vector3.zero;
        private bool return_ = false;
        private bool rely_ = false;

        public float GetSoulAmount() { return soul_amount_; }
        public void SetSoulAmount(float soul_amount) { soul_amount_ = soul_amount; }

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="actor_controller"></param>
        public override void Init(ActorController actor_controller)
        {
            if(soul_ == null)
            {// 魂のコントローラを保存しておく
                soul_ = actor_controller;
            }

            CameraController camera = Camera.main.GetComponent<CameraController>();
            if (camera != null)
            {
                camera.SetTarget(transform);
            }

            gameObject.tag = "Player";

            var rely_detector_object = new GameObject("RelyDetector");
            rely_detector_object.transform.SetParent(transform);
            rely_detector_ = rely_detector_object.AddComponent<RelyDetector>();
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <param name="actor_controller"></param>
        public override void Uninit(ActorController actor_controller)
        {
            Destroy(rely_detector_.gameObject);
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
            ReturnToSoul(actor_controller);
        }

        /// <summary>
        /// 魂の位置を今モンスターの右隣にする処理
        /// </summary>
        /// <param name="monster_transform">乗り移ってる相手の位置</param>
        public void SetSoulTransform(Transform monster_transform)
        {
            transform.SetPositionAndRotation(
                monster_transform.position + monster_transform.right,
                monster_transform.rotation);
        }

        private void UpdateInput()
        {
            // 入力情報の取得
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            rely_ = Input.GetButtonDown("Rely");
            return_ = Input.GetButtonDown("Return");

            // 前方に向かって移動する
            direction_ = new Vector3(horizontal, 0f, vertical).normalized;
        }

        // 移動処理
        private void Move(ActorController actor_controller)
        {
            float direction_magnitude = direction_.magnitude;

            if (direction_magnitude == 0.0f)
                return;

            ActorParameter parameter = actor_controller.GetActorParameter();

            // TODO : 地面の法線を求める
            var movement = Vector3.ProjectOnPlane(transform.forward, Vector3.up) * direction_magnitude * parameter.MoveSpeed;
            transform.position += movement * Time.deltaTime;

            // 物理演算の時の回転を切ったのため直接にtransformで回転する
            direction_ = transform.InverseTransformDirection(direction_);
            var turn_amount = Mathf.Atan2(direction_.x, direction_.z);
            transform.Rotate(0f, turn_amount * parameter.TurnSpeed * Time.deltaTime, 0f);
        }

        // 乗り移る処理
        private void RelyOnTarget(ActorController actor_controller)
        {
            if (!rely_ || rely_detector_.Target == null) return;

            // Target
            var target_controller = rely_detector_.Target.GetComponent<ActorController>();
            target_controller.SetBrainType(ActorController.BrainType.kPlayer);
            var target_soul_state = rely_detector_.Target.AddComponent<SoulState>();
            target_soul_state.SetSoulAmount(soul_amount_);
            target_controller.ChangeState(target_soul_state);

            // This
            ChangeStateToNpc(actor_controller);
        }

        // 魂に戻る処理
        private void ReturnToSoul(ActorController actor_controller)
        {
            if (return_ == false 
                || actor_controller.GetActorType() == ActorController.ActorType.kSoul)
                return;

            // Soul
            soul_.SetBrainType(ActorController.BrainType.kPlayer);
            var soul_state = soul_.gameObject.AddComponent<SoulState>();
            soul_state.SetSoulAmount(soul_amount_);
            soul_state.SetSoulTransform(transform);
            soul_.ChangeState(soul_state);

            // This
            ChangeStateToNpc(actor_controller);
        }

        // 今の器をNpcに切り替える
        private void ChangeStateToNpc(ActorController actor_controller)
        {
            gameObject.tag = "Npc";
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
                    actor_controller.ChangeState(gameObject.AddComponent<NpcSoulState>());
                    break;
                default:
                    break;
            }
        }
    }
}
