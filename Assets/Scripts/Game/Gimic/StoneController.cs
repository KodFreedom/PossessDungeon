using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class StoneController : MonoBehaviour
    {
        [SerializeField] float kMaxMass = 2f;
        private Rigidbody rigidbody_ = null;
        private int actor_layer_ = 0;
        private int soul_layer_ = 0;

        private void Start()
        {
            rigidbody_ = gameObject.GetComponent<Rigidbody>();
            rigidbody_.isKinematic = true;
            actor_layer_ = LayerMask.NameToLayer("Actor");
            soul_layer_ = LayerMask.NameToLayer("Soul");
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == actor_layer_)
            {
                var actor_controller = collision.gameObject.GetComponent<ActorController>();
                if (actor_controller.GetBrainType() != ActorController.BrainType.kPlayer) return;

                float push_strength = actor_controller.GetActorParameter().PushStrength;
                rigidbody_.isKinematic = push_strength == 0f ? true : false;
                rigidbody_.mass = kMaxMass * (1f - push_strength);
                actor_controller.OnPushEnter();
            }
            else if (collision.gameObject.layer == soul_layer_)
            {
                rigidbody_.isKinematic = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.layer != actor_layer_) return;
            var actor_controller = collision.gameObject.GetComponent<ActorController>();
            if (actor_controller.GetBrainType() != ActorController.BrainType.kPlayer) return;
            rigidbody_.isKinematic = true;
            actor_controller.OnPushExit();
        }
    }
}