using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBGM : MonoBehaviour {

    private bool fadeBGMFlag = false;

    public AudioSource audioSource;

    private const float FADE_BGM_VALUE = 0.01f;
    private float bgmVolume = 1.0f;

    //============================================================
    //                            初期処理
    //============================================================
    void Start () {

        audioSource = GetComponent<AudioSource>();

    }

    //============================================================
    //                            更新処理
    //============================================================
    void Update () {

        if(Input.GetKeyDown(KeyCode.Return))
        {
            fadeBGMFlag = true;
        }

        // BGMのフェード処理
		if(fadeBGMFlag)
        {
            bgmVolume -= FADE_BGM_VALUE;

            audioSource.volume = bgmVolume;
        }
	}
}
