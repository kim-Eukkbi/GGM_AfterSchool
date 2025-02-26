using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class NativeWindowAlert
{
    [DllImport("user32.dll")]
    private static extern int MessageBoxW(IntPtr hwnd, IntPtr lpText, IntPtr lpCaption, uint flag);


    public static int Alert(string text, string caption,long flag) 
    {
        IntPtr textMarshal = Marshal.StringToHGlobalUni(text);
        IntPtr captionMarshal = Marshal.StringToHGlobalUni(caption);

        int value = 0;

        value = MessageBoxW(WindowHandle.GetWindowHandle(), textMarshal, captionMarshal, (uint)flag);

        Marshal.FreeHGlobal(textMarshal);
        Marshal.FreeHGlobal(captionMarshal);

        return value;
    }
}

[System.Serializable]
public enum NativeTag : long
{
    Error = 0x00000010L,
    Warning = 0x00000030L,
    Info = 0x00000040L,
    Question = 0x00000020L,
    Ok = 0x00000000L,
    OkCancel = 0x00000001L,
    ReCancel = 0x00000005L,
    Yesno = 0x00000004L,
    YesnoCancel = 0x00000003L
}
