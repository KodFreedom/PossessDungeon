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

        public float MaxLife { get { return max_life_; } }
        public float Life { get { return life_; } }
        public float MoveSpeed { get { return move_speed_; } }
        public float TurnSpeed { get { return turn_speed_; } }
        public void SetLife(float life) { life_ = life; }
    }
}
