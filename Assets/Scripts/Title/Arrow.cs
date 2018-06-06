using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBaka;

public class Arrow : MonoBehaviour
{
    private const float ArrowMove = 400.0f;           // 移動量
    private const float DefaultFloating = 1.1f;       // 浮遊速度

    private bool ArrowFlag;             // 矢印の属性
    private RectTransform ArrowRect;    // 矢印の情報
    private bool ButtonReleaseFlag;     // 左右ボタンを離したフラグ
    private bool SceneChangeFlag;       // 画面遷移したフラグ

    private bool FloatingFlag;         // 浮遊フラグ
    public float FloatingVelocity;     // 浮遊速度
    private float DefaultPosY;         // 初期座標Y

    [SerializeField]
    private GameObject TutorialImg;
    [SerializeField]
    private GameObject GameStartImg;

    // Use this for initialization
    void Start()
    {
        ArrowRect = GetComponent<RectTransform>();
        DefaultPosY = ArrowRect.localPosition.y;
        FloatingVelocity = DefaultFloating;
    }

    // Update is called once per frame
    void Update()
    {
        // 矢印の上下移動
        if (FloatingFlag == false)
        {
            ArrowRect.localPosition = new Vector3(ArrowRect.localPosition.x, ArrowRect.localPosition.y - (FloatingVelocity *= 0.94f), ArrowRect.localPosition.z);
        }
        else if (FloatingFlag == true)
        {
            ArrowRect.localPosition = new Vector3(ArrowRect.localPosition.x, ArrowRect.localPosition.y + (FloatingVelocity *= 0.92f), ArrowRect.localPosition.z);
        }

        if (ArrowRect.localPosition.y <= DefaultPosY - 10)
        {
            FloatingFlag = true;
            FloatingVelocity = DefaultFloating;
        }
        else if (ArrowRect.localPosition.y >= DefaultPosY)
        {
            FloatingFlag = false;
            FloatingVelocity = DefaultFloating;
        }

        // 矢印の左右移動
        if (ButtonReleaseFlag == false)
        {
            // 右入力
            if (Input.GetAxisRaw("Horizontal") > 0.3f)
            {
                ArrowRect.localPosition = new Vector3(ArrowRect.localPosition.x * -1, ArrowRect.localPosition.y, ArrowRect.localPosition.z);
                ArrowFlag = !ArrowFlag;
                ButtonReleaseFlag = true;
            }
            // 左入力
            else if (Input.GetAxisRaw("Horizontal") < -0.3f)
            {
                ArrowRect.localPosition = new Vector3(ArrowRect.localPosition.x * -1, ArrowRect.localPosition.y, ArrowRect.localPosition.z);
                ArrowFlag = !ArrowFlag;
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
                    // シーン遷移
                    //SceneManager.Instance.ChangeScene("Tutorial");
                    SceneChangeFlag = true;
                }
                else if (ArrowFlag == true)
                {
                    // シーン遷移
                    SceneManager.Instance.ChangeScene("StageSelect");
                    SceneChangeFlag = true;
                }
            }
        }
    }
}
