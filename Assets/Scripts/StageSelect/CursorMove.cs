using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBaka;
//using UnityEditor;
//using UnityEngine.UI;

public class CursorMove : MonoBehaviour {

    //==============================
    //           マクロ定義
    //==============================
    private const float CURSOR_POS_X_LEFT = -260.0f;            // カーソルのX座標(左)
    private const float CURSOR_POS_X_CENTER = 0.0f;             // カーソルのX座標(中央)
    private const float CURSOR_POS_X_RIGHT = 260.0f;            // カーソルのX座標(右)
    private const float CURSOR_POS_Y = 30.0f;                   // カーソルのY座標

    private const float CURSOR_SCALE_X = 1.5f;
    private const float CURSOR_SCALE_Y = 1.5f;
    private const float CURSOR_SCALE_Z = 1.0f;

    private const float CURSOR_ANIMATION_TIME_MAX = 1.5f;       // カーソルのアニメーションが完了するまでの時間
    private const float CURSOR_ANIMATION_MOVE_SPEED = 1.5f;     // カーソルのアニメーションの移動速度の加算値
    private const float CURSOR_ANIMATION_SCALE_SPEED = 0.02f;   // カーソルのアニメーションの拡大率の加算値 

    //==============================
    //             列挙型
    //==============================
    private enum POS_IDX
    {
        LEFT = 0,
        CENTER,
        RIGHT
    }
    POS_IDX pos_idx;

    private RectTransform moveAfterRectPos;             // RectTransformの代入用の変数

    private int posIdx = 0;                             // 座標のインデックス番号(Idxで現在座標を管理)

    private bool animationFlg = false;                  // カーソルのアニメーションフラグ

    private float animationScaleSpeed = 0.0f;           // カーソルの拡大率の変動率
    private float animationMoveSpeed = 0.0f;            // カーソルの座標移動の変動率

    public GameObject leftDoor;                         // 左のドアのGameObjject宣言 
    ChangeDoorColor leftDoorColor;                      // 左のドアのスクリプト実行用の変数

    public GameObject centerDoor;                       // 中央のドアのGameObjject宣言
    ChangeDoorColor centerDoorColor;                    // 中央のドアのスクリプト実行用の変数

    public GameObject rightDoor;                        // 右のドアのGameObjject宣言
    ChangeDoorColor rightDoorColor;                     // 右のドアのスクリプト実行用の変数

    //============================================================
    //                            初期処理
    //============================================================
    void Start () {
        // コンポーネント取得
        leftDoorColor = leftDoor.GetComponent<ChangeDoorColor>();
        centerDoorColor = centerDoor.GetComponent<ChangeDoorColor>();
        rightDoorColor = rightDoor.GetComponent<ChangeDoorColor>();
    }

    //============================================================
    //                            更新処理
    //============================================================
    void Update()
    {
        // 現在座標の保存
        moveAfterRectPos = GetComponent<RectTransform>();

        // アニメーション中はステージ選択出来ないようにifで弾く
        if (animationFlg)
        {
            AnimationCursor();
        }

        else
        {
            // カーソルの右移動
            if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                && posIdx < (int)POS_IDX.RIGHT)
            {
                posIdx++;
            }

            // カーソルの左移動
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                && (int)POS_IDX.LEFT < posIdx)
            {
                posIdx--;
            }

            // インデックス番号を参照してRectTransformの座標変更
            switch (posIdx)
            {
                case (int)POS_IDX.LEFT:
                    // 座標変更、選択ステージの画像の色変更
                    moveAfterRectPos.anchoredPosition = new Vector2(CURSOR_POS_X_LEFT, CURSOR_POS_Y);
                    SetChangeDoorColorFlag(true, false, false);
                    break;

                case (int)POS_IDX.CENTER:
                    // 座標変更、選択ステージの画像の色変更
                    moveAfterRectPos.anchoredPosition = new Vector2(CURSOR_POS_X_CENTER, CURSOR_POS_Y);
                    SetChangeDoorColorFlag(false, true, false);
                    break;

                case (int)POS_IDX.RIGHT:
                    // 座標変更、選択ステージの画像の色変更
                    moveAfterRectPos.anchoredPosition = new Vector2(CURSOR_POS_X_RIGHT, CURSOR_POS_Y);
                    SetChangeDoorColorFlag(false, false, true);
                    break;
            }



            // Enter押下時にカーソルのアニメーション開始
            if (Input.GetKeyDown(KeyCode.Return))
            {
                animationFlg = true;
            }
        }
    }

    //============================================================
    //                  カーソルのアニメーション処理
    //============================================================
    void AnimationCursor()
    {
        // アニメーション中
        if (animationScaleSpeed < CURSOR_ANIMATION_TIME_MAX)
        {
            // カーソルの拡大率と移動速度の加算
            animationMoveSpeed += CURSOR_ANIMATION_MOVE_SPEED;
            animationScaleSpeed += CURSOR_ANIMATION_SCALE_SPEED;


            moveAfterRectPos.localScale = new Vector3(  CURSOR_SCALE_X - animationScaleSpeed,
                                                        CURSOR_SCALE_Y - animationScaleSpeed,
                                                        CURSOR_SCALE_Z);

            moveAfterRectPos.anchoredPosition = new Vector2(CURSOR_POS_X_RIGHT * posIdx - CURSOR_POS_X_RIGHT,
                                                            CURSOR_POS_Y - animationMoveSpeed);
        }

        // アニメーション完了後にシーン遷移
        else
        {
            ChangeScene();
        }
    }

    //============================================================
    //               ステージ選択時の色の変更
    //============================================================
    void SetChangeDoorColorFlag(bool doorFlag1, bool doorFlag2, bool doorFlag3)
    {
        // ドア画像の色をtrueで完全表示、falseで半透明表示するメソッドを実行
        leftDoorColor.SetChangeDoorColorFlag(doorFlag1);
        centerDoorColor.SetChangeDoorColorFlag(doorFlag2);
        rightDoorColor.SetChangeDoorColorFlag(doorFlag3);
    }


    //============================================================
    //               ゲームステージへのシーン遷移処理
    //============================================================
    void ChangeScene()
    {
        switch (posIdx)
        {
            // Stage1シーンに遷移
            case (int)POS_IDX.LEFT:
                SceneManager.Instance.ChangeScene("GameStage01");
                break;

            // Stage2シーンに遷移
            case (int)POS_IDX.CENTER:
                SceneManager.Instance.ChangeScene("GameStage02");
                break;

            // Stage3シーンに遷移
            case (int)POS_IDX.RIGHT:
                SceneManager.Instance.ChangeScene("GameStage03");
                break;
        }
    }


}
