using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class FireController : MonoBehaviour
    {
        [SerializeField] GameObject fire_prefab_ = null;
        //[SerializeField] Vector3 fire_offset = V
        [SerializeField] Vector2 fire_distance_ = new Vector2(0.1f, 0.2f);
        private int actor_layer_ = 0;
        private int soul_layer_ = 0;

        // Use this for initialization
        private void Start()
        {
            actor_layer_ = LayerMask.NameToLayer("Actor");
            soul_layer_ = LayerMask.NameToLayer("Soul");
            ClearEditorMesh();
            CreateFireInErea();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer != actor_layer_
                && other.gameObject.layer != soul_layer_)
                return;

            other.gameObject.GetComponent<ActorController>().Burn();
        }

        // エディタ表示用のメッシュの削除
        private void ClearEditorMesh()
        {
            Destroy(GetComponent<MeshFilter>());
            Destroy(GetComponent<MeshRenderer>());
        }

        // 範囲内に火の生成処理
        private void CreateFireInErea()
        {
            int horizontal_number = (int)(transform.localScale.x / fire_distance_.x);
            int vertical_number = (int)(transform.localScale.z / fire_distance_.y);

            Vector3 offset = transform.position
                        - transform.forward * vertical_number * fire_distance_.y * 0.5f
                        - transform.right * horizontal_number * fire_distance_.x * 0.5f;

            for (int count_vertical = 0; count_vertical < vertical_number; ++count_vertical)
            {
                for(int count_horizontal = 0;count_horizontal < horizontal_number; ++count_horizontal)
                {
                    var fire = Instantiate(fire_prefab_);
                    fire.transform.position = offset
                        + transform.forward * count_vertical * fire_distance_.y
                        + transform.up * 0.5f
                        + transform.right * count_horizontal * fire_distance_.x;
                }
            }
        }
    }
}