using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectBaka
{
    public class WaterController : MonoBehaviour
    {
        private static GameObject kSplashPrefab = null;
        private int golem_layer_ = 0;
        private int anonymous_layer_ = 0;
        private int carbuncle_layer_ = 0;
        private int soul_layer_ = 0;
        private NavMeshObstacle nav_mesh_obstacle_ = null;
        private List<ParticleSystem> splashes_ = null;


        // Use this for initialization
        private void Start()
        {
            golem_layer_ = LayerMask.NameToLayer("Golem");
            anonymous_layer_ = LayerMask.NameToLayer("Anonymous");
            carbuncle_layer_ = LayerMask.NameToLayer("Carbuncle");
            soul_layer_ = LayerMask.NameToLayer("Soul");
            nav_mesh_obstacle_ = GetComponent<NavMeshObstacle>();

            if (kSplashPrefab == null)
            {
                kSplashPrefab = (GameObject)Resources.Load("Prefabs/Splash", typeof(GameObject));
            }
            splashes_ = new List<ParticleSystem>();
        }

        private void Update()
        {
            for(int i = 0; i < splashes_.Count;++i)
            {
                if(splashes_[i].isStopped)
                {
                    Destroy(splashes_[i].gameObject);
                    splashes_.RemoveAt(i);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != golem_layer_
                && other.gameObject.layer != anonymous_layer_
                && other.gameObject.layer != carbuncle_layer_
                && other.gameObject.layer != soul_layer_)
                return;
            var actor_controller = other.gameObject.GetComponent<ActorController>();
            if (actor_controller.GetBrainType() != ActorController.BrainType.kPlayer)
            {
                return;
            }

            if(actor_controller.GetActorParameter().CanSwimming)
            {
                var splash = GameObject.Instantiate(kSplashPrefab).GetComponent<ParticleSystem>();
                splash.transform.position = other.transform.position;
                splashes_.Add(splash);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer != golem_layer_
                && other.gameObject.layer != anonymous_layer_
                && other.gameObject.layer != carbuncle_layer_
                && other.gameObject.layer != soul_layer_)
                return;

            var actor_controller = other.gameObject.GetComponent<ActorController>();
            if(actor_controller.GetBrainType() != ActorController.BrainType.kPlayer)
            {
                return;
            }

            nav_mesh_obstacle_.enabled = !actor_controller.GetActorParameter().CanSwimming;

            other.gameObject.GetComponent<ActorController>().Swim();
        }
    }
}