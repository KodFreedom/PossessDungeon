using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private const float ArrowMove = 400.0f; // 移動量

    public bool ArrowFlag;              // 矢印の属性
    private RectTransform ArrowRect;    // 矢印の情報
    private bool ButtonReleaseFlag;     // 左右ボタンを離したフラグ
    private bool SceneChangeFlag;       // 画面遷移したフラグ

    [SerializeField]
    private GameObject TutorialImg;
    [SerializeField]
    private GameObject GameStartImg;

    // Use this for initialization
    void Start()
    {
        ArrowRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // 矢印の左右移動
        if (ButtonReleaseFlag == false)
        {
            // 右入力
            if (Input.GetAxisRaw("Horizontal") > 0.3f)
            {
                ArrowRect.localPosition = new Vector3(ArrowRect.localPosition.x * -1, ArrowRect.localPosition.y, ArrowRect.localPosition.z);
                ArrowFlag = true;
                ButtonReleaseFlag = true;
            }
            // 左入力
            else if (Input.GetAxisRaw("Horizontal") < -0.3f)
            {
                ArrowRect.localPosition = new Vector3(ArrowRect.localPosition.x * -1, ArrowRect.localPosition.y, ArrowRect.localPosition.z);
                ArrowFlag = false;
                ButtonReleaseFlag = true;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 && ButtonReleaseFlag == true)
        {
            ButtonReleaseFlag = false;
        }

        // 選択していないほうを点滅させない
        if (ArrowFlag == false)
        {
            TutorialImg.GetComponent<ImgScaling>().enabled = true;
            GameStartImg.GetComponent<ImgScaling>().enabled = false;

        }
        else if (ArrowFlag == true)
        {
            TutorialImg.GetComponent<ImgScaling>().enabled = false;
            GameStartImg.GetComponent<ImgScaling>().enabled = true;
        }

        // 決定されたら画面遷移
        if (SceneChangeFlag == false)
        {
            if (Input.GetButtonDown("Submit") == true)
            {
                if (ArrowFlag == false)
                {
                    Debug.Log("チュートリアル");   // ここにシーン遷移
                    SceneChangeFlag = true;
                }
                else if (ArrowFlag == true)
                {
                    Debug.Log("ゲームスタート");   // ここにシーン遷移
                    SceneChangeFlag = true;
                }
            }
        }
    }
}
