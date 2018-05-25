using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class RelyUiController : MonoBehaviour
    {
        [SerializeField] Vector3 kAnonymousOffset = Vector3.zero;
        [SerializeField] Vector3 kGolemOffset = Vector3.zero;
        [SerializeField] Vector3 kCarbuncleOffset = Vector3.zero;
        private Vector3 offset_ = Vector3.zero;
        private Transform target_ = null;
        private MeshRenderer renderer_ = null;

        public void SetTarget(Transform target)
        {
            if (target_ == target) return;

            target_ = target;
            if(target_ != null)
            {
                renderer_.enabled = true;

                var type = target_.GetComponent<ActorController>().GetActorType();
                if (type == ActorController.ActorType.kAnonymous)
                {
                    offset_ = kAnonymousOffset;
                }
                else if (type == ActorController.ActorType.kCarbuncle)
                {
                    offset_ = kCarbuncleOffset;
                }
                else
                {
                    offset_ = kGolemOffset;
                }
            }
            else
            {
                renderer_.enabled = false;
            }
        }

        // Use this for initialization
        void Start()
        {
            renderer_ = GetComponent<MeshRenderer>();
            renderer_.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(target_)
            {
                transform.position = target_.position + offset_;
                //transform.LookAt(Camera.main.transform.position, -Vector3.up);
            }
        }
    }
}