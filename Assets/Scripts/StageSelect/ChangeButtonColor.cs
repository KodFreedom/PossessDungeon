using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonColor : MonoBehaviour {

    public bool changeButtonColorFlag = false;      /*  ボタンが選択されているかどうか
                                                        true  ⇒ ボタンの色を255で表示
                                                        false ⇒ ボタンを薄暗い色で表示 */

    //============================================================
    //                            初期処理
    //============================================================
    void Start () {
		
	}
    //============================================================
    //                 ボタンの色の変更フラグのセット
    //============================================================
    public void SetChangeButtonColorFlag(bool setFlag)
    {
        changeButtonColorFlag = setFlag;
    }

    //============================================================
    //                            更新処理
    //============================================================
    void Update () {
        
        if(changeButtonColorFlag)
        {
            ChangeColor(new Color(255f / 255f,    // R
                                  255f / 255f,    // G
                                  255f / 255f,    // B
                                  255f / 255f));  // アルファ);
        }
        else
        {
            ChangeColor(new Color(100f / 255f,    // R
                                  100f / 255f,    // G
                                  100f / 255f,    // B
                                  100f / 255f));  // アルファ);
        }
        
    }

    //============================================================
    //                        色の変更処理
    //============================================================
    void ChangeColor(Color spriteColor)
    {
        /*
        GameObject obj = Selection.activeGameObject;

        var spriteRenderer = obj.GetComponent<SpriteRenderer>();
        var doorColor = spriteRenderer.color;

        doorColor = spriteColor;
        */
        this.GetComponent<SpriteRenderer>().color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, spriteColor.a);
    }
}
