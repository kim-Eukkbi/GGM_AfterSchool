using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chapter_3_Logic : Chapter_Logic
{

    [SerializeField]
    private Camera uiCam;

    [SerializeField]
    private RawImage testRaw;

    [SerializeField]
    private TextMeshProUGUI passwordText;

    private bool isBGmodified = false;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        if(InputManager.Instance.KeyDown(KEY_TYPE.ACTIVE) && !isBGmodified)
        {
            BGModification();
        }

        base.Update();
    }

    public void OnEnable()
    {
        correctPasswordEvent.AddListener(BGManager.Instance.SetDefaultBG);
    }

    public void OnDisable()
    {
        correctPasswordEvent.RemoveListener(BGManager.Instance.SetDefaultBG);
    }

    private void BGModification()
    {
        StartCoroutine(GetPassWordUIImage());
    }

    private IEnumerator GetPassWordUIImage()
    {
        passwordText.text = password.ToString();

        yield return null;

        RenderTexture renderTexture = new RenderTexture(Screen.width,Screen.height,32);
        uiCam.targetTexture = renderTexture;
        uiCam.Render();
        RenderTexture.active = renderTexture;

        //renderTexutre => Texture2D 바꾸는 기능

        Texture2D passwordUI = new Texture2D(renderTexture.width,renderTexture.height,TextureFormat.RGBA32,false);
        Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
        passwordUI.ReadPixels(rect, 0, 0);
        passwordUI.Apply();

        RenderTexture.active = null;

        /* testRaw.texture = BGManager.Instance.GetPcWallpaper();*/


        //testRaw.texture = passwordUI;



       /* testRaw.texture = */BGManager.Instance.SetModifiedBG(BGManager.Instance.GetPcWallpaper(), passwordUI);


        isBGmodified = true;

        //비밀번호 이미지 가져오는 기능
    }
}
