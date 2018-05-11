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
    }

}