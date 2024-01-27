using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonMashController : MiniGamePlayerController
{
    public int amountOfButtonMashes = 0;
    public bool canMash;
  /*  public enum PossiblePlaces
    {
        FIRST,
        SECOND,
        THIRD,
        LAST
    }

    public PossiblePlaces possiblePlaces;
*/
    public override void Initialize(PlayerController playerController)
    {
        base.Initialize(playerController);

        playerController.SouthButton += ButtonMashController_SouthButton;
    }

    private void ButtonMashController_SouthButton(InputAction.CallbackContext ctx)
    {
        if (canMash && ctx.performed)
        {
            ButtonMashed();
        }
    }

    public void ButtonMashed()
    {
        amountOfButtonMashes++;
    }
/*
    public void SetPlace()
    {
            switch (possiblePlaces)
            {
                case PossiblePlaces.FIRST:
                    PlayerControllerReference.PlayerData.pointsThisRound = 3;
                    break;

                case PossiblePlaces.SECOND:
                    PlayerControllerReference.PlayerData.pointsThisRound = 2;
                    break;

                case PossiblePlaces.THIRD:
                    PlayerControllerReference.PlayerData.pointsThisRound = 1;
                    break;

                case PossiblePlaces.LAST:
                    PlayerControllerReference.PlayerData.pointsThisRound = 0;
                    break;
            }
    }
*/
    private void OnDestroy()
    {
        PlayerControllerReference.SouthButton -= ButtonMashController_SouthButton;
    }
}
