using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgScaling : MonoBehaviour
{
    private RectTransform ImgRect;      // イメージの情報
    private bool ScaleFlag;             // 拡縮のフラグ

    [SerializeField]
    private float MaxScale;     // スケールの最大値
    [SerializeField]
    private float MinScale;     // スケールの最小値
    [SerializeField]
    private float ScaleSpeed;   // スケールのスピード

    // Use this for initialization
    void Start()
    {
        // ロゴの情報を取得
        ImgRect = GetComponent<RectTransform>();

        // 初期化
        ScaleFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ロゴの拡縮
        if(ScaleFlag == false)
        {
            ImgRect.localScale = new Vector3(ImgRect.localScale.x + ScaleSpeed, ImgRect.localScale.y + ScaleSpeed, ImgRect.localScale.z);
        }
        else if(ScaleFlag == true)
        {
            ImgRect.localScale = new Vector3(ImgRect.localScale.x - ScaleSpeed, ImgRect.localScale.y - ScaleSpeed, ImgRect.localScale.z);
        }

        if (ImgRect.localScale.x >= MaxScale)
        {
            ScaleFlag = true;
        }
        else if(ImgRect.localScale.x <= MinScale)
        {
            ScaleFlag = false;
        }
    }
}
