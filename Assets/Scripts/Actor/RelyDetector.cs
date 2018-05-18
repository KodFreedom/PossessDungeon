using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class RelyDetector : MonoBehaviour
    {
        private GameObject target_ = null;
        public GameObject Target { get { return target_; } }

        private int actor_layer_ = 0;

        // Use this for initialization
        void Start()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            var collider = gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            collider.center = new Vector3(0.0f, 0.0f, 5.0f);
            collider.size = new Vector3(2.0f, 5.0f, 10.0f);
            actor_layer_ = LayerMask.NameToLayer("Actor");
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer != actor_layer_
                || target_ == other.gameObject) return;

            if (target_ == null)
            {
                target_ = other.gameObject;
            }
            else
            {// 距離を計算し、近いの方をターゲットにする
                target_ = GetClosestTarget(target_, other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == actor_layer_
                && other.gameObject == target_)
            {
                target_ = null;
            }
        }

        private GameObject GetClosestTarget(GameObject target_a, GameObject target_b)
        {
            Vector3 parent_position = transform.parent.position;
            if(Vector3.SqrMagnitude(parent_position - target_a.transform.position)
                <= Vector3.SqrMagnitude(parent_position - target_b.transform.position))
            {
                return target_a;
            }
            return target_b;
        }
    }
}