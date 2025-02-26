using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter_1_Logic : Chapter_Logic
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        if(InputManager.Instance.KeyDown(KEY_TYPE.ACTIVE))
        {
            //������ ����Ƽ�� �� ����
            ActiveAlert();
        }

        base.Update();
    }

    private void ActiveAlert()
    {
        int value = 0;
        value = NativeWindowAlert.Alert("��й�ȣ�� :" + password.ToString(), Environment.UserName, (long)(NativeTag.Error | NativeTag.Yesno));

        if(value == 7)
        {
            ActiveAlert();
        }
    }
}
