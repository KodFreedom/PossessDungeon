using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class RelyDetector : MonoBehaviour
    {
        private static GameObject kRelyUiPrefab = null;
        private GameObject target_ = null;
        public GameObject Target { get { return target_; } }

        private int golem_layer_ = 0;
        private int anonymous_layer_ = 0;
        private int carbuncle_layer_ = 0;
        private RelyUiController rely_ui_controller_ = null;

        // Use this for initialization
        void Start()
        {
            if(kRelyUiPrefab == null)
            {
                kRelyUiPrefab = (GameObject)Resources.Load("Prefabs/RelyUi", typeof(GameObject));
            }

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            var collider = gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            collider.center = new Vector3(0.0f, 0.0f, 5.0f);
            collider.size = new Vector3(2.0f, 5.0f, 10.0f);

            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            golem_layer_ = LayerMask.NameToLayer("Golem");
            anonymous_layer_ = LayerMask.NameToLayer("Anonymous");
            carbuncle_layer_ = LayerMask.NameToLayer("Carbuncle");

            // UIの生成
            rely_ui_controller_ = GameObject.Instantiate(kRelyUiPrefab).GetComponent<RelyUiController>();
        }

        private void OnDestroy()
        {
            // UIの破棄
            if (rely_ui_controller_ == null) return;
            Destroy(rely_ui_controller_.gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            if ((other.gameObject.layer != golem_layer_
                && other.gameObject.layer != anonymous_layer_
                && other.gameObject.layer != carbuncle_layer_)
                || target_ == other.gameObject)
                return;

            if (target_ == null)
            {
                target_ = other.gameObject;
            }
            else
            {// 距離を計算し、近いの方をターゲットにする
                target_ = GetClosestTarget(target_, other.gameObject);
            }

            // UI
            rely_ui_controller_.SetTarget(target_.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if ((other.gameObject.layer == golem_layer_
                || other.gameObject.layer == anonymous_layer_
                || other.gameObject.layer == carbuncle_layer_)
                && other.gameObject == target_)
            {
                target_ = null;
                rely_ui_controller_.SetTarget(null);
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