using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartText : MonoBehaviour
{
    private const float AlphaValue = 0.05f;        // アルファの量

    private Text GameStart;         // ゲームスタートテキスト
    private bool FlashingFlag;      // 点滅のフラグ

    // Use this for initialization
    void Start()
    {
        // テキスト情報を取得
        GameStart = GetComponent<Text>();

        // 初期化
        FlashingFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        // エンターテキストの点滅
        if(FlashingFlag == false)
        {
            GameStart.color = new Color(GameStart.color.r, GameStart.color.g, GameStart.color.b, GameStart.color.a - AlphaValue * 0.2f);
        }
        else if(FlashingFlag == true)
        {
            GameStart.color = new Color(GameStart.color.r, GameStart.color.g, GameStart.color.b, GameStart.color.a + AlphaValue);
        }

        if (GameStart.color.a >= 1.0f)
        {
            FlashingFlag = false;
        }
        else if (GameStart.color.a <= 0.0f)
        {
            FlashingFlag = true;
        }
    }
}
