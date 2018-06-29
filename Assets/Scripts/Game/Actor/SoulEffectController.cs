using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class SoulEffectController : MonoBehaviour
    {
        [SerializeField] float kFloatingAmount = 1f;
        [SerializeField] float kFloatingSpeed = 1f;
        private float origin_position_y_ = 0f;
        private float theta_ = 0f;

        // Use this for initialization
        void Start()
        {
            origin_position_y_ = transform.localPosition.y;
        }

        // Update is called once per frame
        void Update()
        {
            theta_ += kFloatingSpeed * Time.deltaTime;
            if (theta_ >= Mathf.PI * 2f) theta_ -= Mathf.PI * 2f;

            float amount = Mathf.Sin(theta_) * kFloatingAmount;
            transform.localPosition = new Vector3(0f, origin_position_y_ + amount, 0f);
        }
    }
}