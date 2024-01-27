using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ButtonMashGameController : MonoBehaviour
{
    public MiniGameController miniGameController;
    public List<GameObject> buttonMashPlayers = new List<GameObject>();
    public TextMeshProUGUI buttonMashTestCounterUGUI;
    public TextMeshProUGUI countdownUi;
    public int countdownAmount;
    public int buttonMashTimerAmount;

    private int previousPlayerScore;
    private ButtonMashController previousPlayer;

    private void Start()
    {
        miniGameController = FindObjectOfType<MiniGameController>();

        foreach (var buttonMashPlayer in miniGameController.PlayerObjects/*FindObjectsOfType<ButtonMashController>()*/)
        {
            buttonMashPlayers.Add(buttonMashPlayer.gameObject);
        }

        StartCoroutine(CountdownForStartTimer(countdownAmount));
    }

    // Replace with Timer from GameController when available
    public IEnumerator CountdownForStartTimer(int countdownAmount)
    {
        int timer = countdownAmount;
        while (timer > 0)
        {
            Debug.Log(timer);
            countdownUi.text = timer.ToString();
            yield return new WaitForSeconds(1);
            timer--;
        }

        countdownUi.text = "Go!";

        ToggleCanMash();
        StartCoroutine(ButtonMeshTimer(buttonMashTimerAmount));

        yield return new WaitForSeconds(0.5f);
        countdownUi.gameObject.SetActive(false);
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
        ToggleCanMash();

        buttonMashPlayers = buttonMashPlayers.OrderByDescending(player => player.GetComponent<ButtonMashController>().amountOfButtonMashes).ToList();

        buttonMashTestCounterUGUI.text = "Player 1: " + buttonMashPlayers[0].GetComponent<ButtonMashController>().amountOfButtonMashes + 
                                         " Player 2: " + buttonMashPlayers[1].GetComponent<ButtonMashController>().amountOfButtonMashes +
                                         " Player 3: " + buttonMashPlayers[2].GetComponent<ButtonMashController>().amountOfButtonMashes +
                                         " Player 4: " + buttonMashPlayers[3].GetComponent<ButtonMashController>().amountOfButtonMashes;



       /* // Add logic for which player gets 3, 2, 1, and 0 points for this round and load scoreboard scene
        foreach (var player in GameManager.Instance.Players) 
        {
            //int playerScore = buttonMashPlayers[i].GetComponent;
        

        }

        for (int i = 0; i < buttonMashPlayers.Count; i++)
        {
            buttonMashPlayers[i].GetComponent<ButtonMashController>().possiblePlaces = (ButtonMashController.PossiblePlaces)i;

            int playerScore = buttonMashPlayers[i].GetComponent<ButtonMashController>().amountOfButtonMashes;

            if (playerScore == previousPlayerScore)
            {
                buttonMashPlayers[i].GetComponent<ButtonMashController>().possiblePlaces = previousPlayer.possiblePlaces;
            }

            previousPlayer = buttonMashPlayers[i].GetComponent<ButtonMashController>();
            previousPlayerScore = playerScore;



            buttonMashPlayers[i].GetComponent<ButtonMashController>().SetPlace();
        }*/

    }


    public void ToggleCanMash()
    {
        foreach (var buttonMashPlayer in buttonMashPlayers)
        {
            buttonMashPlayer.GetComponent<ButtonMashController>().canMash = !buttonMashPlayer.GetComponent<ButtonMashController>().canMash;
        }
    }
}
