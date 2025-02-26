using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chapter_4_Logic : Chapter_Logic
{
    [SerializeField]
    private Image maskImage;

    [SerializeField]
    private Image realImage;

    [SerializeField]
    private Image fakeImage;

    [SerializeField]
    private TextMeshProUGUI realpass;

    [SerializeField]
    private CanvasGroup logicCanvasGroup;

    [SerializeField]
    private TextMeshProUGUI realpasswordText;

    [SerializeField]
    private TextMeshProUGUI fakepasswordText;

    private bool isLogicEnabled = false;

    public override void Start()
    {
        base.Start();
        ResetFakepassword();
    }

    private void ResetFakepassword()
    {
        List<List<string>> fakeints = new List<List<string>>();
        List<PasswordInfo> realintPos = new List<PasswordInfo>();
        HashSet<PasswordInfo> intpos = new HashSet<PasswordInfo>();
        List<char> passwordlist = password.ToString().ToList();


        Debug.LogError(passwordlist.Count);




        for (int i = 0; i < 9; i++) //72���� ��¥ ���� ����
        {
            List<string> rowInt = new List<string>();
            for (int j = 0; j < 8; j++)
            {
                int value = Random.Range(0, 10);
                rowInt.Add(value.ToString());
            }
            fakeints.Add(rowInt);
        }

        while (intpos.Count < passwordlist.Count) //��¥ ��� �ߺ� ���� ��ǥ���� ����
        {
            intpos.Add(new PasswordInfo { position = new Vector2(Random.Range(0, 8), Random.Range(0, 9)), number = "" });
        }

        realintPos = new List<PasswordInfo>(intpos); // Hash to List
        realintPos = realintPos.OrderBy(x => x.position.x).ThenBy(y => y.position.y).ToList(); //��ǥ�� ���� ������� ����

        for (int i = 0; i < passwordlist.Count; i++) //��¥ ��� ��ġ�� �ִ� ��¥ ���� ����� ��¥ ��� ���� ���� ���
        {
            int x = (int)realintPos[i].position.x;
            int y = (int)realintPos[i].position.y;

            fakeints[y][x] = " ";

            realintPos[i].number = passwordlist[i].ToString();
            // Debug.LogError(realintPos[i].number);
        }

        StringBuilder sb = new StringBuilder(); //��¥ ��ȣ ���

        foreach (List<string> row in fakeints)
        {
            foreach (string vlaue in row)
            {
                sb.Append(vlaue);
            }
            sb.AppendLine();
        }

        fakepasswordText.text = sb.ToString();

        ResetRealPassword(realintPos);
    }

    private void ResetRealPassword(List<PasswordInfo> posList)
    {
        StringBuilder sb = new StringBuilder();


        int maxX = 0;
        int maxY = 0;

        foreach (PasswordInfo info in posList) // X Y �� �ִ� ���ϱ�
        {
            if (info.position.x > maxX)
                maxX = (int)info.position.x;
            if (info.position.y > maxY)
                maxY = (int)info.position.y;
        }


        string[,] grid = new string[maxY + 1, maxX + 1]; // �ִ� ������ ���� �������� ä���
        for (int y = 0; y <= maxY; y++)
        {
            for (int x = 0; x <= maxX; x++)
            {
                grid[y, x] = " ";
            }
        }


        foreach (PasswordInfo info in posList) //��ǥ�� ã�Ƽ� ������ ���ڷ� ä���
        {
            grid[(int)info.position.y, (int)info.position.x] = info.number.ToString();
        }


        for (int y = 0; y <= maxY; y++) // �ؽ�Ʈ �о ����ϱ�
        {
            for (int x = 0; x <= maxX; x++)
            {
                sb.Append(grid[y, x]);
            }
            sb.AppendLine();
        }

        realpasswordText.text = sb.ToString();
    }




    public override void Update()
    {
        fakeImage.transform.position = realImage.transform.position;

        if(InputManager.Instance.KeyDown(KEY_TYPE.ACTIVE))
        {
            ResetMaskOffset();
            isLogicEnabled = true;
            logicCanvasGroup.alpha = 1;
        }

        if(InputManager.Instance.KeyDown(KEY_TYPE.ESC))
        {
            isLogicEnabled = false;
            logicCanvasGroup.alpha = 0;
        }

        if(isLogicEnabled)
        {
            SetMakeImageOffset();
        }

        base.Update();
    }

    private void SetMakeImageOffset()
    {
        WinRect outsideRect = WindowRectManager.Instance.GetWindowOutsideRect();


        Vector2 offSetMin = new Vector2(Mathf.Max(outsideRect.left, maskImage.rectTransform.offsetMin.x),
                                        Mathf.Max(outsideRect.bottom, maskImage.rectTransform.offsetMin.y));

        Vector2 offSetMax = new Vector2(Mathf.Max(outsideRect.right, -maskImage.rectTransform.offsetMax.x),
                                        Mathf.Max(outsideRect.top, -maskImage.rectTransform.offsetMax.y));

        maskImage.rectTransform.offsetMin = offSetMin;
        maskImage.rectTransform.offsetMax = -offSetMax;
    }

    private void ResetMaskOffset()
    {
        maskImage.rectTransform.offsetMin = Vector2.zero;
        maskImage.rectTransform.offsetMax = Vector2.zero;
    }
}


[System.Serializable]
public class PasswordInfo
{
    public Vector2 position;
    public string number;
}
