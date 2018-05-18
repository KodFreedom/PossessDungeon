using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class FireController : MonoBehaviour
    {
        private int actor_layer_ = 0;
        private int soul_layer_ = 0;

        // Use this for initialization
        private void Start()
        {
            actor_layer_ = LayerMask.NameToLayer("Actor");
            soul_layer_ = LayerMask.NameToLayer("Soul");
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer != actor_layer_
                && other.gameObject.layer != soul_layer_)
                return;

            other.gameObject.GetComponent<ActorController>().Burn();
        }
    }
}