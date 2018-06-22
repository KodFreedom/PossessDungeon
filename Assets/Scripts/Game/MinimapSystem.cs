using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBaka
{
    public class MinimapSystem : MonoBehaviour
    {
        /////////////////////////////////////////////////////////////////////////
        // シングルトーンインスタンス
        /////////////////////////////////////////////////////////////////////////
        private static MinimapSystem instance_ = null;
        public static MinimapSystem Instance { get { return instance_; } }

        private MinimapCameraController camera_ = null;
        public MinimapCameraController Camera { get { return camera_; } }

        [SerializeField] Transform kMinimapMask = null;
        [SerializeField] Color kGolemIconColor = Color.red;
        [SerializeField] Color kAnonymousIconColor = Color.blue;
        [SerializeField] Color kCarbuncleIconColor = Color.yellow;
        [SerializeField] GameObject kMinimapActorPrefab = null;
        private Hashtable hashtable_ = null;
        private float map_scale_ = 0f;

        public void RegisterActor(ActorController actor_controller)
        {
            var minimap_actor = GameObject.Instantiate(kMinimapActorPrefab, kMinimapMask);
            var minimap_actor_image = minimap_actor.GetComponent<Image>();

            // 色
            switch (actor_controller.GetActorType())
            {
                case ActorController.ActorType.kAnonymous:
                    minimap_actor_image.color = kAnonymousIconColor;
                    break;
                case ActorController.ActorType.kGolem:
                    minimap_actor_image.color = kGolemIconColor;
                    break;
                case ActorController.ActorType.kCarbuncle:
                    minimap_actor_image.color = kCarbuncleIconColor;
                    break;
                default:
                    break;
            }

            hashtable_[actor_controller] = minimap_actor;
        }

        public void DeregisterActor(ActorController actor_controller)
        {
            var minimap_actor = (GameObject)hashtable_[actor_controller];
            if(minimap_actor)
            {
                Destroy(minimap_actor);
                hashtable_.Remove(actor_controller);
            }
        }

        // Use this for initialization
        void Awake()
        {
            // インスタンスが生成されてるかどうかをチェックする
            if (null == instance_)
            {
                // ないなら自分を渡す
                instance_ = this;
            }
            else
            {
                // すでにあるなら自分を破棄する
                DestroyImmediate(gameObject);
            }

            camera_ = GetComponentInChildren<MinimapCameraController>();
            map_scale_ = camera_.GetComponent<Camera>().orthographicSize;
            hashtable_ = new Hashtable();
        }

        private void OnDestroy()
        {
            if(instance_ == this)
            {
                instance_ = null;
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            Vector3 camera_position = camera_.transform.position;

            foreach(DictionaryEntry pair in hashtable_)
            {
                var actor_controller = (ActorController)pair.Key;
                var minimap_actor = (GameObject)pair.Value;
                var rect_transform = minimap_actor.GetComponent<RectTransform>();

                Vector3 actor_position = actor_controller.transform.position;
                actor_position -= camera_position;
                actor_position *= map_scale_;

                rect_transform.localPosition = new Vector3(actor_position.x, actor_position.z, 0f);
            }
        }
    }
}