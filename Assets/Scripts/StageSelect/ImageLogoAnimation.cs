using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageLogoAnimation : MonoBehaviour {

    //==============================
    //           マクロ定義
    //==============================
    private const float LOGO_SCALE_X = 0.15f;                   // ロゴ画像の拡大率(X)
    private const float LOGO_SCALE_Y = 0.15f;                   // ロゴ画像の拡大率(Y)
    private const float LOGO_SCALE_Z = 1.0f;                    // ロゴ画像の拡大率(Z)

    private const float LOGO_ANIMATION_MOVE_SPEED = 0.004f;     // Scaleアニメーションの速度

    private const float LOGO_SCALE_ANIMATION_MAX = 0.1f;        // アニメーションのScaleの最大値
    private const float LOGO_SCALE_ANIMATION_MIN = 0.05f;       // アニメーションのScaleの最小値

    private RectTransform afterRectPos;                         // RectTransformの代入用の変数

    private int addScale = 1;                                   // Scalseを拡大するか縮小するか
    public float animationScaleSpeed = 0.0f;                    // カーソルの拡大率の変動率

    private bool addScaleFlag = true;
    public bool animationFlag = false;

    //============================================================
    //                            初期処理
    //============================================================
    void Start () {
		
	}

    //============================================================
    //                 アニメーションのフラグセット
    //============================================================
    public void SetImageLogoAnimationFlag(bool setAnimationFlag)
    {
        animationFlag = setAnimationFlag;
    }

    //============================================================
    //                            更新処理
    //============================================================
    void Update () {

        // 現在座標の保存
        afterRectPos = GetComponent<RectTransform>();

        if (animationFlag)
        {
            // Scaleを拡大するか縮小するかの判定
            if (animationScaleSpeed >= LOGO_SCALE_ANIMATION_MIN && addScaleFlag == true)
            {
                addScaleFlag = false;
                addScale = -1;
            }
            else if (animationScaleSpeed <= 0.0f && addScaleFlag == false)
            {
                addScaleFlag = true;
                addScale = 1;
            }


            // 拡大率の加算
            animationScaleSpeed += LOGO_ANIMATION_MOVE_SPEED * addScale;

            afterRectPos.localScale = new Vector3(LOGO_SCALE_X + animationScaleSpeed,
                                                  LOGO_SCALE_Y + animationScaleSpeed,
                                                  LOGO_SCALE_Z);
        }
        else
        {
            // Scaleを元のサイズに戻す
            afterRectPos.localScale = new Vector3(LOGO_SCALE_X,
                                                  LOGO_SCALE_Y,
                                                  LOGO_SCALE_Z);
        }
	}
}
