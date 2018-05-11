using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class DragDetector : MonoBehaviour
    {
        private GameObject target_ = null;
        public GameObject Target { get { return target_; } }

        // Use this for initialization
        void Start()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            var collider = gameObject.AddComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.center = new Vector3(0.0f, 0.0f, 1.0f);
            collider.radius = 1.0f;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
            {
                target_ = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Item")
                && other.gameObject == target_)
            {
                target_ = null;
            }
        }
    }
}