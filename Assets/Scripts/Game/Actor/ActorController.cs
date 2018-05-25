using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    [RequireComponent(typeof(ActorParameter))]
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
            kAnonymous = 0,
            kGolem,
            kCarbuncle,
            kSoul
        }

        [SerializeField] BrainType brain_type_ = BrainType.kPlayer;
        [SerializeField] ActorType actor_type_ = ActorType.kAnonymous;

        private ActorParameter actor_parameter_ = null;
        private ActorState actor_state_ = null;

        public BrainType GetBrainType() { return brain_type_; }
        public ActorType GetActorType() { return actor_type_; }
        public ActorParameter GetActorParameter() { return actor_parameter_; }
        public void SetBrainType(BrainType new_type) { brain_type_ = new_type; }
        public void SetActorType(ActorType new_type) { actor_type_ = new_type; }

        /// <summary>
        /// ステートの切り替え
        /// </summary>
        /// <param name="next_state"></param>
        public void ChangeState(ActorState next_state)
        {
            actor_state_.Uninit(this);
            Destroy(actor_state_);

            actor_state_ = next_state;
            actor_state_.Init(this);
        }

        /// <summary>
        /// 火に入った処理
        /// </summary>
        public void Burn()
        {
            float life = actor_parameter_.Life;
            life = Mathf.Clamp(life - actor_parameter_.FireDamage * Time.deltaTime, 0f, actor_parameter_.MaxLife);
            actor_parameter_.SetLife(life);
            actor_state_.Burn(this);
        }

        /// <summary>
        /// 水に入った処理
        /// </summary>
        public void Swim()
        {
            if(!actor_parameter_.CanSwimming)
            {
                actor_parameter_.SetLife(0f);
            }
            actor_state_.Swim(this);
        }

        // Use this for initialization
        private void Start()
        {
            actor_parameter_ = GetComponent<ActorParameter>();

            // 初期ステートの生成
            switch (actor_type_)
            {
                case ActorType.kAnonymous:
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
                        soul_state.SetSoulAmount(SoulState.kMaxSoulAmount);
                        actor_state_ = soul_state;
                        break;
                    }
                default:
                    break;
            }

            actor_state_.Init(this);
        }

        // Update is called once per frame
        private void Update()
        {
            actor_state_.Act(this);
        }
    }
}