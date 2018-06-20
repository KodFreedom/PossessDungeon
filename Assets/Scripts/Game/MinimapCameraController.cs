using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////////
    // シングルトーンインスタンス
    /////////////////////////////////////////////////////////////////////////
    private static MinimapCameraController instance_ = null;
    public static MinimapCameraController Instance { get { return instance_; } }

    private Transform target_ = null;
    private Vector3 offset_ = Vector3.zero;

    /// <summary>
    /// ターゲットの設定
    /// </summary>
    /// <param name="target"></param>
    public void SetTarget(Transform target)
    {
        target_ = target;
    }

    private void Start()
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

        offset_ = transform.position;
    }

    private void LateUpdate()
    {
        if (target_ == null) return;
        transform.position = target_.position + offset_;
    }
}
