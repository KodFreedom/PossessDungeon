using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class NpcSoulState : ActorState
    {
        public override void Init(ActorController actor_controller)
        {
            gameObject.SetActive(false);
        }

        public override void Uninit(ActorController actor_controller)
        {
            gameObject.SetActive(true);
        }

        public override void Act(ActorController actor_controller)
        {

        }
    }
}