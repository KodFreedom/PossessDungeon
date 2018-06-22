using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectBaka
{
    public class StoneController : MonoBehaviour
    {
        private int golem_layer_ = 0;
        private int anonymous_layer_ = 0;
        private int carbuncle_layer_ = 0;
        private int soul_layer_ = 0;
        private NavMeshAgent nav_mesh_agent_ = null;
        private NavMeshObstacle nav_mesh_obstacle_ = null;
        private AudioSource drag_se_ = null;

        private void Start()
        {
            golem_layer_ = LayerMask.NameToLayer("Golem");
            anonymous_layer_ = LayerMask.NameToLayer("Anonymous");
            carbuncle_layer_ = LayerMask.NameToLayer("Carbuncle");
            soul_layer_ = LayerMask.NameToLayer("Soul");
            nav_mesh_agent_ = GetComponent<NavMeshAgent>();
            nav_mesh_obstacle_ = GetComponent<NavMeshObstacle>();
            nav_mesh_agent_.enabled = false;
            nav_mesh_obstacle_.enabled = true;
            drag_se_ = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == golem_layer_
                || other.gameObject.layer == anonymous_layer_
                || other.gameObject.layer == carbuncle_layer_)
            {
                var actor_controller = other.gameObject.GetComponent<ActorController>();
                if (actor_controller.GetBrainType() != ActorController.BrainType.kPlayer) return;

                float push_strength = actor_controller.GetActorParameter().PushStrength;

                if(push_strength > 0f)
                {
                    nav_mesh_agent_.enabled = true;
                    nav_mesh_obstacle_.enabled = false;
                    drag_se_.Play();
                }
                else
                {
                    nav_mesh_agent_.enabled = false;
                    nav_mesh_obstacle_.enabled = true;
                }
                actor_controller.OnPushEnter();
            }
            else if (other.gameObject.layer == soul_layer_)
            {
                nav_mesh_agent_.enabled = false;
                nav_mesh_obstacle_.enabled = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer != golem_layer_)
                return;

            var actor_controller = other.gameObject.GetComponent<ActorController>();
            if (actor_controller.GetBrainType() == ActorController.BrainType.kPlayer)
                return;

            drag_se_.Stop();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer != golem_layer_
                && other.gameObject.layer != anonymous_layer_
                && other.gameObject.layer != carbuncle_layer_)
                return;

            drag_se_.Stop();

            var actor_controller = other.gameObject.GetComponent<ActorController>();
            if (actor_controller.GetBrainType() != ActorController.BrainType.kPlayer)
                return;

            nav_mesh_agent_.enabled = false;
            nav_mesh_obstacle_.enabled = true;
            actor_controller.OnPushExit();
        }
    }
}