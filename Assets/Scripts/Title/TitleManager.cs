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

    private AudioSource[] Audio;
    private bool GameStartFlag;

    // Use this for initialization
    void Start()
    {
        // スクリーンサイズを設定
        Screen.SetResolution(1920, 1080, true, 60);

        Audio = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit") == true)
        {
            // 決定音再生
            if(GameStartFlag == false)
            {
                Audio[0].PlayOneShot(Audio[0].clip);
                GameStartFlag = true;
            }

            // ゲームスタートテキスト削除
            Destroy(GameStartText);

            TutorialImg.SetActive(true);
            StageSelectImg.SetActive(true);
            ArrowSelectImg.SetActive(true);
        }
    }
}
