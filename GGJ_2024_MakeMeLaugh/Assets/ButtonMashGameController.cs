using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ButtonMashGameController : MonoBehaviour
{
    public MiniGameController miniGameController;
    public List<ButtonMashController> buttonMashPlayers = new List<ButtonMashController>();
    public TextMeshProUGUI buttonMashTestCounterUGUI;
    public int countdownAmount;
    public int buttonMashTimerAmount; 
    

    private void Start()
    {
        miniGameController = FindObjectOfType<MiniGameController>();

        StartCoroutine(CountdownForStartTimer(countdownAmount));
        StartCoroutine(ButtonMeshTimer(buttonMashTimerAmount));
    }

    // Replace with Timer from GameController when available
    public IEnumerator CountdownForStartTimer(int countdownAmount)
    {
        int timer = countdownAmount;
        while (timer > 0)
        {
            Debug.Log(timer);
            yield return new WaitForSeconds(1);
            timer--;
        }

        SelectWinner();
    }

    public IEnumerator ButtonMeshTimer(int buttonMashTimerAmount)
    {
        int timer = buttonMashTimerAmount;
        while (timer > 0)
        {
            Debug.Log(timer);
            yield return new WaitForSeconds(1);
            timer--;
        }

        SelectWinner();
    }

    public void SelectWinner()
    {
        // Code that checks which player has the most button mashes
        buttonMashPlayers = buttonMashPlayers.OrderByDescending(player => player.amountOfButtonMashes).ToList();

        buttonMashTestCounterUGUI.text = "Player " + buttonMashPlayers[0] + " Wins, with " + buttonMashPlayers[0].amountOfButtonMashes + " buttons mashes";

        // Add logic for which player gets 3, 2, 1, and 0 points for this round and load scoreboard scene
    }
}
