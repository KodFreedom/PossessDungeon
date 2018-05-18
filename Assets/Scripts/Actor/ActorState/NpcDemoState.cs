using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class NpcDemoState : ActorState
    {
        public override void Init(ActorController actor_controller)
        {
        }

        public override void Uninit(ActorController actor_controller)
        {

        }

        public override void Act(ActorController actor_controller)
        {
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
    }
}