using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectBaka
{
    public class SoulRelyState : SoulState
    {
        [SerializeField] float kLerpSpeed = 5f;
        [SerializeField] float kRelyRange = 0.5f;
        private ActorController target_ = null;

        public void SetTarget(ActorController target)
        {
            target_ = target;
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="actor_controller"></param>
        public override void Init(ActorController actor_controller)
        {
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <param name="actor_controller"></param>
        public override void Uninit(ActorController actor_controller)
        {
            GetComponent<SphereCollider>().enabled = true;
            GetComponent<NavMeshAgent>().enabled = true;
        }

        /// <summary>
        /// 行動処理
        /// </summary>
        /// <param name="actor_controller"></param>
        public override void Act(ActorController actor_controller)
        {
            // targetの位置まで移動する
            transform.position = Vector3.Lerp(transform.position, target_.transform.position, kLerpSpeed * Time.deltaTime);

            // 一定範囲に入ったら乗り移る
            if(Vector3.Distance(transform.position, target_.transform.position) <= kRelyRange)
            {
                target_.SetBrainType(ActorController.BrainType.kPlayer);
                var target_soul_state = target_.gameObject.AddComponent<SoulState>();
                target_soul_state.SetSoulAmount(GetSoulAmount());
                target_.ChangeState(target_soul_state);

                // This
                ChangeStateToNpc(actor_controller);
            }
        }

        /// <summary>
        /// 火に入った処理
        /// </summary>
        /// <param name="actor_controller"></param>
        public override void Burn(ActorController actor_controller)
        {
        }

        /// <summary>
        /// 水に入った処理
        /// </summary>
        /// <param name="actor_controller"></param>
        public override void Swim(ActorController actor_controller)
        {
        }
    }
}