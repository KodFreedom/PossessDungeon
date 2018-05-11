using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class ItemController : MonoBehaviour
    {
        private Rigidbody rigidbody_ = null;
        private Collider collider_ = null;

        public abstract bool DragBy(ActorController actor_controller);
        public abstract void DropBy(ActorController actor_controller);

        protected void DisablePhysics()
        {
            rigidbody_.isKinematic = true;
            rigidbody_.useGravity = false;
            collider_.enabled = false;
        }

        protected void EnablePhysics()
        {
            rigidbody_.isKinematic = false;
            rigidbody_.useGravity = true;
            collider_.enabled = true;
        }

        protected virtual void Start()
        {
            rigidbody_ = gameObject.GetComponent<Rigidbody>();
            collider_ = gameObject.GetComponent<Collider>();
        }
    }
}