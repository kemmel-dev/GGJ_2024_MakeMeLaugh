using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonMashController : MonoBehaviour
{
    public int amountOfButtonMashes = 0;

    private void Awake()
    {
        FindObjectOfType<PlayerContoller>().SouthButton += ButtonMashController_SouthButton; 
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
        FindObjectOfType<PlayerContoller>().SouthButton -= ButtonMashController_SouthButton;
    }
}
