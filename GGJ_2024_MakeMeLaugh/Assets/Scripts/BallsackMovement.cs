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
    public int score = 0;
    Vector2 screenBounds;
    float objectWidth;
    float objectHeight;
    public Color color;


    public override void Initialize(PlayerController playerController)
    {
        base.Initialize(playerController);
        playerController.LeftStick += PlayerControllerOnLeftStick;
        playerController.SouthButton += PlayerControllerOnSouthButton;

        manager = FindAnyObjectByType<ApesHaveBallsacksManager>().GetComponent<ApesHaveBallsacksManager>();
        rigidbody = GetComponent<Rigidbody2D>();
        objectWidth = GetComponent<Collider2D>().bounds.extents.x;
        objectHeight = GetComponent<Collider2D>().bounds.extents.y;

        transform.GetComponent<SpriteRenderer>().color = playerController.PlayerData.color;
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        Debug.Log(spriteRenderers.Length);
        foreach(SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = playerController.PlayerData.color;
            color = playerController.PlayerData.color;
        }

        setupPlayer();
    }

    private void Start()
    {
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    public void setupPlayer()
    {
        var ballsack = Instantiate(PlayerControllerReference.PlayerData.playerBallsackModel, transform.GetChild(0));
        /*var coll = jester.GetComponentInChildren<MeshFilter>().AddComponent<BoxCollider2D>();
        objectWidth = coll.bounds.size.x;
        objectHeight = coll.bounds.size.y;*/

        ballsack.transform.localPosition = Vector3.zero;
        ballsack.transform.localScale = Vector3.one * 1.5f;
        ballsack.transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Transform>().parent.transform == manager.Ape.transform)
        {
            inBallsackArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Transform>().parent.transform == manager.Ape.transform)
        {
            inBallsackArea = false;
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
            score += 1;
        }
    }

    void Update()
    {
        rigidbody.velocity = _LeftStickInput * speed;
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }

    private void OnDestroy()
    {
        PlayerControllerReference.LeftStick -= PlayerControllerOnLeftStick;
        PlayerControllerReference.SouthButton -= PlayerControllerOnSouthButton;
    }
}
