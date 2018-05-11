using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class NpcSoulState : ActorState
    {
        MeshRenderer mesh_renderer_ = null;


        public override void Init(ActorController actor_controller)
        {
            mesh_renderer_ = gameObject.GetComponentInChildren<MeshRenderer>();
            mesh_renderer_.enabled = false;
        }

        public override void Uninit(ActorController actor_controller)
        {

        }

        public override void Act(ActorController actor_controller)
        {

        }
    }
}