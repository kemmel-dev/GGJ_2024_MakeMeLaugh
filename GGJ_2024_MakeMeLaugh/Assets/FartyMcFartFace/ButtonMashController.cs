using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonMashController : MiniGamePlayerController
{
    public int amountOfButtonMashes = 0;

    public override void Initialize(PlayerController playerController)
    {
        base.Initialize(playerController);

        playerController.SouthButton += ButtonMashController_SouthButton;
    }

    private void ButtonMashController_SouthButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            ButtonMashed();
        }
    }

    public void ButtonMashed()
    {
        amountOfButtonMashes++;
    }

    private void OnDestroy()
    {
        PlayerControllerReference.SouthButton -= ButtonMashController_SouthButton;
    }
}
