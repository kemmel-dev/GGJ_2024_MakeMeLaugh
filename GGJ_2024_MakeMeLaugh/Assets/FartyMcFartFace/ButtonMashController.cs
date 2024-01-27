using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonMashController : MiniGamePlayerController
{
    public int amountOfButtonMashes = 0;
    public bool canMash;
    public Rigidbody2D rigidbody;
    public int fartMultiplier;

    public override void Initialize(PlayerController playerController)
    {
        base.Initialize(playerController);

        SpriteRenderer spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = playerController.PlayerData.color;
        rigidbody = GetComponent<Rigidbody2D>();

        playerController.SouthButton += ButtonMashController_SouthButton;
    }

    private void ButtonMashController_SouthButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            ButtonMashed();
            FartBuildupInflate();
        }

        if (ctx.canceled)
        {
            FartBuildupDeflate();
        }
    }

    public void ButtonMashed()
    {
        amountOfButtonMashes++;
    }

    public void FartBuildupInflate()
    {
        transform.localScale *= 1.05f;
    }
    
    public void FartBuildupDeflate()
    {
        transform.localScale *= 0.975f;
    }

    public void Fart()
    {
        var fartPower = amountOfButtonMashes * fartMultiplier;

        rigidbody.AddForce(new Vector2(0, fartPower));
    }


    private void OnDestroy()
    {
        PlayerControllerReference.SouthButton -= ButtonMashController_SouthButton;
    }
}
