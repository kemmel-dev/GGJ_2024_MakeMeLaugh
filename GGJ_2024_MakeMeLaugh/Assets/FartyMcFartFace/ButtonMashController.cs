using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonMashController : MonoBehaviour
{
    public TextMeshProUGUI buttonMashTestCounterUGUI;
    private int amountOfButtonMashes = 0;

    private void Awake()
    {
        
    }

    public void ButtonMashed()
    {
        amountOfButtonMashes++;
    }
}
