using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnimation : MonoBehaviour {

    //==============================
    //           マクロ定義
    //==============================
    private const float LOGO_SCALE_X = 1.0f;        // ロゴ画像の拡大率(X)
    private const float LOGO_SCALE_Y = 1.0f;        // ロゴ画像の拡大率(Y)
    private const float LOGO_SCALE_Z = 1.0f;        // ロゴ画像の拡大率(Z)

    private const float LOGO_ANIMATION_MOVE_SPEED = 0.1f;

    private RectTransform afterRectPos;             // RectTransformの代入用の変数

    public float animationScaleSpeed = 0.0f;       // カーソルの拡大率の変動率

    //============================================================
    //                            初期処理
    //============================================================
    void Start () {
		
	}

    //============================================================
    //                            更新処理
    //============================================================
    void Update () {

        bool addScaleFlag = true;
        int addScale = 1;

        // 現在座標の保存
        afterRectPos = GetComponent<RectTransform>();

        /*
        if()
        {
            addScale = 1;
        }
        else
        {
            addScale = -1;
        }
        */

        // 拡大率の加算
        animationScaleSpeed += LOGO_ANIMATION_MOVE_SPEED * addScale;

        afterRectPos.localScale = new Vector3();
    }
}
