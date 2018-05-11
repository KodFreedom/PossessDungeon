using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    [RequireComponent(typeof(BoxCollider))]
    public class BoxController : ItemController
    {
        public override bool DragBy(ActorController actor_controller)
        {
            if (actor_controller.GetActorType() == ActorController.ActorType.kGolem)
            {
                transform.SetParent(actor_controller.transform);
                transform.localPosition = Vector3.up * 1f + Vector3.forward * 1f;
                DisablePhysics();
                return true;
            }

            return false;
        }

        public override void DropBy(ActorController actor_controller)
        {
            transform.SetParent(null, true);
            EnablePhysics();
        }

        protected override void Start()
        {
            base.Start();
        }
    }
}