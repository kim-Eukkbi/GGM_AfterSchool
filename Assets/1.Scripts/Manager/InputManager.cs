using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region ΩÃ±€≈Ê

    public static InputManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    #endregion


    private bool[] m_KeyDown = new bool[(int)KEY_TYPE.MAX];
    private bool[] m_KeyHold = new bool[(int)KEY_TYPE.MAX];
    private bool[] m_KeyUp = new bool[(int)KEY_TYPE.MAX];

    private KeyCode[] Keyboard_Table = new KeyCode[]
    {
        KeyCode.A,
        KeyCode.D,
        KeyCode.W,
        KeyCode.S,
        KeyCode.F,
        KeyCode.Escape,
    };


    private void Update()
    {
        Array.Clear(m_KeyDown, 0, m_KeyDown.Length);
        Array.Clear(m_KeyUp, 0, m_KeyUp.Length);
        for (int i = 0; i < (int)KEY_TYPE.MAX; i++)
        {
            KeyCode code = Keyboard_Table[i];

            if(Input.GetKeyDown(code))
            {

                m_KeyDown[i] = true;
            }

            if (Input.GetKey(code))
            {
                m_KeyHold[i] = true;
            }

            if (Input.GetKeyUp(code))
            {

                m_KeyUp[i] = true;

                m_KeyHold[i] = false;
            }
        }
    }

    public void ClearAllValues()
    {
        Array.Clear(m_KeyDown, 0, m_KeyDown.Length);
        Array.Clear(m_KeyUp, 0, m_KeyUp.Length);
        Array.Clear(m_KeyHold, 0, m_KeyHold.Length);
    }

    public bool KeyHold(KEY_TYPE type)
    {
        return m_KeyHold[(int)type];
    }

    public bool KeyUp(KEY_TYPE type)
    {
        return m_KeyUp[(int)type];
    }

    public bool KeyDown(KEY_TYPE type)
    {
        return m_KeyDown[(int)type];
    }
}


public enum KEY_TYPE : int
{
    LEFT = 0,
    RIGHT = 1,
    UP = 2,
    DOWN = 3,
    ACTIVE = 4,
    ESC,

    MAX = ESC +1
}
