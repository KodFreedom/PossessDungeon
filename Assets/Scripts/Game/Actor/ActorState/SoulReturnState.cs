using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectBaka
{
    public class SoulReturnState : SoulState
    {
        [SerializeField] float kLerpSpeed = 5f;
        [SerializeField] float kReturnRange = 0.5f;
        private Vector3 point_ = Vector3.zero;
        public void SetReturnPoint(Vector3 point)
        {
            point_ = point;
        }

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
            transform.position = Vector3.Lerp(transform.position, point_, kLerpSpeed * Time.deltaTime);

            // 一定範囲に入ったら操作できる様になる
            if (Vector3.Distance(transform.position, point_) <= kReturnRange)
            {
                // Soul
                var soul_state = gameObject.AddComponent<SoulState>();
                soul_state.SetSoulAmount(GetSoulAmount());
                actor_controller.ChangeState(soul_state);
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