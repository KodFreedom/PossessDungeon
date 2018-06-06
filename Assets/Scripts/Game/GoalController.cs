using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class GoalController : MonoBehaviour
    {
        private int golem_layer_ = 0;
        private int anonymous_layer_ = 0;
        private int carbuncle_layer_ = 0;
        private int soul_layer_ = 0;

        // Use this for initialization
        private void Start()
        {
            golem_layer_ = LayerMask.NameToLayer("Golem");
            anonymous_layer_ = LayerMask.NameToLayer("Anonymous");
            carbuncle_layer_ = LayerMask.NameToLayer("Carbuncle");
            soul_layer_ = LayerMask.NameToLayer("Soul");
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer != golem_layer_
                && other.gameObject.layer != anonymous_layer_
                && other.gameObject.layer != carbuncle_layer_
                && other.gameObject.layer != soul_layer_)
                return;

            var actor_controller = other.gameObject.GetComponent<ActorController>();
            if (actor_controller.GetBrainType() != ActorController.BrainType.kPlayer) return;

            GameFlowController.Instance.GameClear();
        }
    }
}