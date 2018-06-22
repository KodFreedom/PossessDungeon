using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCursorSE : MonoBehaviour {

    public AudioSource soundMoveSE;
    public AudioSource soundEnterSE;

    //============================================================
    //                            初期処理
    //============================================================
    void Start () {
        
        // SE再生のために追加したコンポーネントの情報を取得
        AudioSource[] audioSources = GetComponents<AudioSource>();

        soundMoveSE = audioSources[0];
        soundEnterSE = audioSources[1];
    }

    //============================================================
    //                            更新処理
    //============================================================
    void Update () {
		
        // カーソル移動時のSE再生
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            soundMoveSE.PlayOneShot(soundMoveSE.clip);
        }

        // エンター時のSE再生
        if(Input.GetKeyDown(KeyCode.Return))
        {
            soundEnterSE.PlayOneShot(soundEnterSE.clip);
        }
	}
}
