using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject GameStartText;       // ゲームスタートテキスト
    [SerializeField]
    private GameObject TutorialImg;         // チュートリアル
    [SerializeField]
    private GameObject StageSelectImg;      // ステージセレクト
    [SerializeField]
    private GameObject ArrowSelectImg;      // 矢印セレクト


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit") == true)
        {
            // ゲームスタートテキスト削除
            Destroy(GameStartText);

            TutorialImg.SetActive(true);
            StageSelectImg.SetActive(true);
            ArrowSelectImg.SetActive(true);
        }
    }
}
