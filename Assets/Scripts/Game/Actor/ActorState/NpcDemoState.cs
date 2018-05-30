using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class NpcDemoState : ActorState
    {
        private readonly float kArriveDistance = 0.5f;
        private readonly float kWaitTime = 5f;
        private Transform[] patrol_points_;
        private int patrol_counter_ = 0;
        private float wait_counter_ = 0f;
        private Animator animator_ = null;

        public override void Init(ActorController actor_controller)
        {
            patrol_points_ = GetComponent<PatrolPoints>().Points;
            animator_ = GetComponentInChildren<Animator>();
            wait_counter_ = kWaitTime;
        }

        public override void Uninit(ActorController actor_controller)
        {

        }

        public override void Act(ActorController actor_controller)
        {
            Patrol(actor_controller);

            if (actor_controller.GetActorParameter().Life <= 0f)
            {// 肉体が死ぬ
                Destroy(gameObject);
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

        private void Patrol(ActorController actor_controller)
        {
            if (patrol_points_.Length == 0) return;
            if (wait_counter_ > 0f)
            {// しばらく待機
                wait_counter_ -= Time.deltaTime;

                if(animator_)
                {
                    animator_.SetFloat("movement", 0f);
                }
                return;
            }

            // Move and turn
            ActorParameter parameter = actor_controller.GetActorParameter();
            Vector3 direction = Vector3.Scale(patrol_points_[patrol_counter_].position - transform.position,
                new Vector3(1f, 0f, 1f)).normalized;
            Vector3 movement = Vector3.ProjectOnPlane(transform.forward, CheckGroundNormal()) * parameter.MoveSpeed;
            transform.position += movement * Time.deltaTime;

            direction = transform.InverseTransformDirection(direction);
            var turn_amount = Mathf.Atan2(direction.x, direction.z);
            transform.Rotate(0f, turn_amount * parameter.TurnSpeed * Time.deltaTime, 0f);

            // 目標にある程度近づいたら待機して、次の目標に移動する
            if((transform.position - patrol_points_[patrol_counter_].position).sqrMagnitude
                <= kArriveDistance * kArriveDistance)
            {
                wait_counter_ = kWaitTime;
                patrol_counter_ = (patrol_counter_ + 1) % patrol_points_.Length;
            }

            if (animator_)
            {
                animator_.SetFloat("movement", 1f);
            }
        }
    }
}