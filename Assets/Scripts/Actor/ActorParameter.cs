using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class ActorParameter : MonoBehaviour
    {
        [SerializeField] float max_life_ = 0f;
        [SerializeField] float life_ = 0f;
        [SerializeField] float move_speed_ = 0f;
        [SerializeField] float turn_speed_ = 0f;
        [SerializeField] float soul_damage_ = 0f;
        [SerializeField] float fire_damage_ = 0f;
        [SerializeField, Range(0f, 1f)] float push_strength_ = 0f;
        [SerializeField] bool can_swimming_ = false;

        public float MaxLife { get { return max_life_; } }
        public float Life { get { return life_; } }
        public float MoveSpeed { get { return move_speed_; } }
        public float TurnSpeed { get { return turn_speed_; } }
        public float SoulDamage { get { return soul_damage_; } }
        public float FireDamage { get { return fire_damage_; } }
        public float PushStrength { get { return push_strength_; } }
        public bool CanSwimming { get { return can_swimming_; } }
        public void SetLife(float life) { life_ = life; }

        private void Start()
        {
            life_ = max_life_;
        }
    }
}
