using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBaka;

public class CursorMove : MonoBehaviour {

    public RectTransform moveAfterRectPos;

    public int posIdx = 0;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        moveAfterRectPos = GetComponent<RectTransform>();

        // カーソルの右移動
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            && posIdx < 2)
        {
            posIdx++;
        }

        // カーソルの左移動
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            && 0 < posIdx)
        {
            posIdx--;
        }

        // RectTransformの座標変更
        switch(posIdx)
        {
            case 0:
                moveAfterRectPos.anchoredPosition = new Vector2(-150.0f, 30.0f);
                break;

            case 1:
                moveAfterRectPos.anchoredPosition = new Vector2(0.0f, 30.0f);
                break;

            case 2:
                moveAfterRectPos.anchoredPosition = new Vector2(150.0f, 30.0f);
                break;
        }
        


        // Enter押下時に、インデックス番号からどのステージを選んでいるのか取得
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (posIdx)
            {
                // Stage1シーンに遷移
                case 0:
                    SceneManager.Instance.ChangeScene("GameStage01");
                    break;

                // Stage2シーンに遷移
                case 1:
                    //SceneManager.Instance.ChangeScene("GameStage02");
                    break;

                // Stage3シーンに遷移
                case 2:
                    //SceneManager.Instance.ChangeScene("GameStage03");
                    break;
            }
        }
    }
}
