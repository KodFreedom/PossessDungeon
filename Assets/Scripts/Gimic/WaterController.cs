using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class WaterController : MonoBehaviour
    {
        private int actor_layer_ = 0;

        // Use this for initialization
        private void Start()
        {
            actor_layer_ = LayerMask.NameToLayer("Actor");
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer != actor_layer_) return;
            other.gameObject.GetComponent<ActorController>().Swim();
        }
    }
}