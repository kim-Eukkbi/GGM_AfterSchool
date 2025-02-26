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




        for (int i = 0; i < 9; i++) //72개의 가짜 숫자 생성
        {
            List<string> rowInt = new List<string>();
            for (int j = 0; j < 8; j++)
            {
                int value = Random.Range(0, 10);
                rowInt.Add(value.ToString());
            }
            fakeints.Add(rowInt);
        }

        while (intpos.Count < passwordlist.Count) //진짜 비번 중복 없이 좌표정보 생성
        {
            intpos.Add(new PasswordInfo { position = new Vector2(Random.Range(0, 8), Random.Range(0, 9)), number = "" });
        }

        realintPos = new List<PasswordInfo>(intpos); // Hash to List
        realintPos = realintPos.OrderBy(x => x.position.x).ThenBy(y => y.position.y).ToList(); //좌표값 작은 순서대로 정렬

        for (int i = 0; i < passwordlist.Count; i++) //진짜 비번 위치에 있는 가짜 숫자 지우고 진짜 비번 숫자 정보 등록
        {
            int x = (int)realintPos[i].position.x;
            int y = (int)realintPos[i].position.y;

            fakeints[y][x] = " ";

            realintPos[i].number = passwordlist[i].ToString();
            // Debug.LogError(realintPos[i].number);
        }

        StringBuilder sb = new StringBuilder(); //가짜 번호 출력

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

        foreach (PasswordInfo info in posList) // X Y 값 최대 구하기
        {
            if (info.position.x > maxX)
                maxX = (int)info.position.x;
            if (info.position.y > maxY)
                maxY = (int)info.position.y;
        }


        string[,] grid = new string[maxY + 1, maxX + 1]; // 최댓값 안으로 전부 공백으로 채우기
        for (int y = 0; y <= maxY; y++)
        {
            for (int x = 0; x <= maxX; x++)
            {
                grid[y, x] = " ";
            }
        }


        foreach (PasswordInfo info in posList) //좌표값 찾아서 공백을 숫자로 채우기
        {
            grid[(int)info.position.y, (int)info.position.x] = info.number.ToString();
        }


        for (int y = 0; y <= maxY; y++) // 텍스트 읽어서 출력하기
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
