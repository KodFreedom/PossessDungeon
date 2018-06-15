using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class ActorLifeUiController : MonoBehaviour
    {
        [SerializeField] Vector3 kAnonymousOffset = Vector3.zero;
        [SerializeField] Vector3 kGolemOffset = Vector3.zero;
        [SerializeField] Vector3 kCarbuncleOffset = Vector3.zero;
        private Vector3 offset_ = Vector3.zero;
        private Transform target_ = null;
        private ActorController actor_controller_ = null;
        private Transform cover_ = null;

        public void SetTarget(Transform target)
        {
            if (target_ == target) return;

            target_ = target;
            if (target_ != null)
            {
                actor_controller_ = target_.GetComponent<ActorController>();
                var type = actor_controller_.GetActorType();
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
        }

        // Use this for initialization
        void Start()
        {
            cover_ = transform.Find("Cover");
        }

        // Update is called once per frame
        void Update()
        {
            if (target_)
            {
                transform.position = target_.position + offset_;
                UpdateLife();
            }
        }

        private void UpdateLife()
        {
            float life_rate = actor_controller_.GetActorParameter().Life / actor_controller_.GetActorParameter().MaxLife;
            var scale = cover_.localScale;
            var position = cover_.localPosition;
            scale.x = life_rate;
            position.x = (life_rate - 1f) * 0.5f;
            cover_.localScale = scale;
            cover_.localPosition = position;
        }
    }
}