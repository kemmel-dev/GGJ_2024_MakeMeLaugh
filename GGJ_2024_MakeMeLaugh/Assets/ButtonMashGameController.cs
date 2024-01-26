using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMashGameController : MonoBehaviour
{

    // Replace with Timer from GameController when available
    public IEnumerator ButtonMeshTimer()
    {
        int timer = 5;
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }

        SelectWinner();
    }

    public void SelectWinner()
    {
        // Add code that checks which player has the most button mashes
        Debug.Log("PlayerX Wins");
    }
}
