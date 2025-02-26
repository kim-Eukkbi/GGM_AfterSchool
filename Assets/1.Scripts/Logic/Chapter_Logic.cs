using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Chapter_Logic : MonoBehaviour
{
    [SerializeField]
    private Transform playerSpawnPoint;

    [SerializeField]
    private CanvasGroup passUIGroup;

    [SerializeField]
    private TMP_InputField passwordInputField;

    [SerializeField]
    private TextMeshProUGUI wrongPassText;

    protected int password;

    protected UnityEvent correctPasswordEvent = new UnityEvent();

    protected GameObject player;

    public virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.transform.position = playerSpawnPoint.position;

        password = PasswordManager.Instance.GetPassword();
        Debug.Log(password);


        passwordInputField.onSubmit.AddListener((value) =>
        {
            if(value.Equals(password.ToString()))
            {
                // 값 같을때 코드
                ResetPassword();
                SceneChangeManager.Instance.LoadScene(0);
                correctPasswordEvent?.Invoke();
            }
            else
            {
                //값 다를때 코드 == 틀렸다고 text 켜주는거
                StartCoroutine(WrongTextCo());
            }
        });
    }

    public virtual void Update()
    {
        if(InputManager.Instance.KeyDown(KEY_TYPE.ESC))
        {
            PasswordUIControll(false);
        }
    }

    private IEnumerator WrongTextCo()
    {
        wrongPassText.alpha = 1;

        yield return new WaitForSeconds(2f);

        wrongPassText.alpha = 0;
    }

    private void ResetPassword()
    {
        password = PasswordManager.Instance.GetPassword();
        PasswordUIControll(false);
    }

    public void PasswordUIControll(bool isActive)
    {
        passUIGroup.alpha = isActive ? 1 : 0;
        passUIGroup.interactable = isActive;
    }
}
