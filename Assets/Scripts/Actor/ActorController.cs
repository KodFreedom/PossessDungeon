using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class ActorController : MonoBehaviour
    {
        /// <summary>
        /// プレイヤーかAIかを示す列挙型
        /// </summary>
        public enum BrainType
        {
            kPlayer = 0,
            kNPC = 1
        }

        /// <summary>
        /// 人のタイプの列挙型
        /// </summary>
        public enum ActorType
        {
            kInnsmouth = 0,
            kGolem,
            kCarbuncle,
            kSoul
        }

        [SerializeField] BrainType brain_type_ = BrainType.kPlayer;
        [SerializeField] ActorType actor_type_ = ActorType.kInnsmouth;
        [SerializeField] float move_speed_ = 1f;
        public float MoveSpeed { get { return move_speed_; } }
        [SerializeField] float turn_speed_ = 360f;
        public float TurnSpeed { get { return turn_speed_; } }
        private ActorState actor_state_ = null;

        public BrainType GetBrainType() { return brain_type_; }
        public void SetBrainType(BrainType new_type) { brain_type_ = new_type; }
        public ActorType GetActorType() { return actor_type_; }
        public void SetActorType(ActorType new_type) { actor_type_ = new_type; }

        public void ChangeState(ActorState next_state)
        {
            actor_state_.Uninit(this);
            Destroy(actor_state_);

            actor_state_ = next_state;
            actor_state_.Init(this);
        }

        // Use this for initialization
        void Start()
        {
            // 初期ステートの生成
            switch (actor_type_)
            {
                case ActorType.kInnsmouth:
                    actor_state_ = gameObject.AddComponent<NpcDemoState>();
                    break;
                case ActorType.kGolem:
                    actor_state_ = gameObject.AddComponent<NpcDemoState>();
                    break;
                case ActorType.kCarbuncle:
                    actor_state_ = gameObject.AddComponent<NpcDemoState>();
                    break;
                case ActorType.kSoul:
                    {
                        var soul_state = gameObject.AddComponent<SoulState>();
                        SoulState.Parameter parameter;
                        parameter.max_soul_amount = 100f;
                        parameter.soul_amount = 100f;
                        soul_state.SetParameter(parameter);
                        actor_state_ = soul_state;
                        break;
                    }
                default:
                    break;
            }

            actor_state_.Init(this);
        }

        // Update is called once per frame
        void Update()
        {
            actor_state_.Act(this);
        }
    }
}