using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallsackMovement : MiniGamePlayerController
{
    [SerializeField]
    private int speed = 10;
    public Transform player;
    public ApesHaveBallsacksManager manager;
    private bool inBallsackArea = false;
    private Vector2 _LeftStickInput;
    private Rigidbody2D rigidbody;


    public override void Initialize(PlayerController playerController)
    {
        base.Initialize(playerController);
        playerController.LeftStick += PlayerControllerOnLeftStick;
        playerController.SouthButton += PlayerControllerOnSouthButton;

        manager = FindAnyObjectByType<ApesHaveBallsacksManager>().GetComponent<ApesHaveBallsacksManager>();
        rigidbody =  GetComponent<Rigidbody2D>();
        Debug.Log(rigidbody.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Transform>().parent.transform == manager.Ape.transform)
        {
            inBallsackArea = true;
            Debug.Log("in");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Transform>().parent.transform == manager.Ape.transform)
        {
            inBallsackArea = false;
            Debug.Log("gone");
        }
    }

 
    private void PlayerControllerOnLeftStick(InputAction.CallbackContext ctx)
    {
        if (ctx.started || ctx.performed)
        {
            _LeftStickInput = ctx.ReadValue<Vector2>();
        }

        if (ctx.canceled)
        {
            _LeftStickInput = Vector2.zero;
        }
    }

    private void PlayerControllerOnSouthButton(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (inBallsackArea)
        {
            manager.m_MyEvent.Invoke();
        }
    }

    void Update()
    {
        Debug.Log(_LeftStickInput.x);
        rigidbody.velocity = _LeftStickInput * speed;
    }

    private void OnDestroy()
    {
        PlayerControllerReference.LeftStick -= PlayerControllerOnLeftStick;
        PlayerControllerReference.SouthButton -= PlayerControllerOnSouthButton;
    }
}
