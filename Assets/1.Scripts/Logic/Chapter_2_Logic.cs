using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Chapter_2_Logic : Chapter_Logic
{
    private string docPath = string.Empty;


    public override void Start()
    {
        base.Start();

        docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    }

    public override void Update()
    {
        base.Update();

        if (InputManager.Instance.KeyDown(KEY_TYPE.ACTIVE))
        {
            CheckFile();
            //문서 -> GGM -> password.txt => password 내용 넣어서 
        }
    }

    private void CheckFile()
    {
        if(Directory.Exists(docPath + "/GGM"))
        {
            DelFile();
        }
        else
        {
            Directory.CreateDirectory(docPath + "/GGM");
        }

        SetFile();
    }

    private void SetFile()
    {
        StreamWriter writer = File.CreateText(docPath + "/GGM" + "/PassWord.txt");
        writer.WriteLine(password);
        writer.Close();
    }

    private void DelFile()
    {
        File.Delete(docPath + "/GGM" + "/PassWord.txt");
    }

}
