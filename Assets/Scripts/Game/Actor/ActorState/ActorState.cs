using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public abstract class ActorState : MonoBehaviour
    {
        public abstract void Init(ActorController actor_controller);

        public abstract void Uninit(ActorController actor_controller);

        public abstract void Act(ActorController actor_controller);

        public abstract void Burn(ActorController actor_controller);

        public abstract void Swim(ActorController actor_controller);

        public virtual void OnPushEnter(ActorController actor_controller) { }

        public virtual void OnPushExit(ActorController actor_controller) { }

        // 地面法線取得
        protected Vector3 CheckGroundNormal()
        {
            RaycastHit hit_info;

            // 着地した
            // 速度を地面速度にして、地面法線を返す
            if (Physics.Raycast(transform.position, Vector3.down, out hit_info, 0.1f))
            {
                //IsGrounded = true;
                return hit_info.normal;
            }

            // 空中にいる
            // 速度を空中速度にして、上方向を返す
            else
            {
                //IsGrounded = false;
                return Vector3.up;
            }
        }
    }
}