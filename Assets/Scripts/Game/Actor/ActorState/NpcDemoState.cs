using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectBaka
{
    public class NpcDemoState : ActorState
    {
        private readonly float kArriveDistance = 0.5f;
        private readonly float kWaitTime = 5f;
        private PatrolPoints patrol_points_;
        private int patrol_counter_ = 0;
        private float wait_counter_ = 0f;
        private Animator animator_ = null;
        private NavMeshAgent nav_mesh_agent_ = null;

        public override void Init(ActorController actor_controller)
        {
            patrol_points_ = GetComponent<PatrolPoints>();
            patrol_points_.RecalculatePoints();

            animator_ = GetComponentInChildren<Animator>();
            wait_counter_ = kWaitTime;

            var parameter = actor_controller.GetActorParameter();
            nav_mesh_agent_ = GetComponent<NavMeshAgent>();
            nav_mesh_agent_.speed = parameter.MoveSpeed;
            nav_mesh_agent_.angularSpeed = parameter.TurnSpeed;
            nav_mesh_agent_.updatePosition = true;
            nav_mesh_agent_.updateRotation = true;
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
            if (patrol_points_.Points.Length == 0) return;
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
            nav_mesh_agent_.isStopped = false;
            nav_mesh_agent_.SetDestination(patrol_points_.Points[patrol_counter_]);

            // 目標にある程度近づいたら待機して、次の目標に移動する
            if((transform.position - patrol_points_.Points[patrol_counter_]).sqrMagnitude
                <= kArriveDistance * kArriveDistance)
            {
                nav_mesh_agent_.isStopped = true;
                wait_counter_ = kWaitTime;
                patrol_counter_ = (patrol_counter_ + 1) % patrol_points_.Points.Length;
            }

            if (animator_)
            {
                animator_.SetFloat("movement", 1f);
            }
        }
    }
}