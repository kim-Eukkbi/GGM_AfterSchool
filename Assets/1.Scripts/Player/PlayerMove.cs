using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 0f;

    private Animator playerAnimator;

    private Vector2 input;

    private Vector2 moveDir;

    private Stack<KEY_TYPE> keyStack = new Stack<KEY_TYPE>();

    private Vector2[] moveVectors = new Vector2[4] { Vector2.left, Vector2.right, Vector2.up, Vector2.down };

    private bool[] isMovingDir = new bool[4];

    private Rigidbody2D body;

    private bool canPlayerMove = true;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(canPlayerMove)
        {
            PushInputs();
            InputCheck();
        }
    }


    private void FixedUpdate()
    {
        if(canPlayerMove)
        {
            body.velocity = moveDir * speed;
        }
    }


    private void PushInputs()
    {
        for (int i = 0; i < 4; i++)
        {
            if(InputManager.Instance.KeyDown((KEY_TYPE)i))
            {
               // Debug.Log("asdsa");
                keyStack.Push((KEY_TYPE)i);
            }
        }
    }

    private void InputCheck()
    {
        if(keyStack.Count != 0)
        {
            int lastInput = (int)keyStack.Peek();

            isMovingDir[lastInput] = InputManager.Instance.KeyHold((KEY_TYPE)lastInput);


            input.x = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
            input.y = Mathf.Abs(Input.GetAxisRaw("Vertical"));
            input *= moveVectors[lastInput];


            if(isMovingDir[lastInput])
            {
                SetAnimationParameters(moveVectors[lastInput]);
            }
            else
            {
                keyStack.Pop();
                input = Vector2.zero;
            }

            moveDir = input.normalized;
            playerAnimator.SetBool("IsMove", input != Vector2.zero);
        }
    }

    private void SetAnimationParameters(Vector2 idle)
    {
        playerAnimator.SetFloat("MoveX", idle.x);
        playerAnimator.SetFloat("MoveY", idle.y);

        playerAnimator.SetFloat("IdleX", idle.x);
        playerAnimator.SetFloat("IdleY", idle.y);
    }
}
