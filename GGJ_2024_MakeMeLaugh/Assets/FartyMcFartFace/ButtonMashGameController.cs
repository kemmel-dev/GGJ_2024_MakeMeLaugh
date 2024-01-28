using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ButtonMashGameController : MonoBehaviour
{
    public MiniGameController miniGameController;
    public List<ButtonMashController> buttonMashPlayers = new List<ButtonMashController>();
    public TextMeshProUGUI buttonMashTestCounterUGUI;
    public TextMeshProUGUI countdownUi;
    public int countdownAmount;
    public int buttonMashTimerAmount;


    private void Start()
    {
        miniGameController = FindObjectOfType<MiniGameController>();

        foreach (var buttonMashPlayer in FindObjectsOfType<ButtonMashController>())
        {
            buttonMashPlayers.Add(buttonMashPlayer);
        }

        StartCoroutine(CountdownForStartTimer(countdownAmount));
    }

    // Replace with Timer from GameController when available
    public IEnumerator CountdownForStartTimer(int countdownAmount)
    {
        int timer = countdownAmount;
        while (timer > 0)
        {
            countdownUi.text = timer.ToString();
            yield return new WaitForSeconds(1);
            timer--;
        }

        countdownUi.text = "Go!";
        var audio = GetComponent<AudioSource>();
        audio.Play();
        /*ToggleCanMash();*/
        GameManager.Instance.ActivateInput();
        StartCoroutine(ButtonMeshTimer(buttonMashTimerAmount));

        yield return new WaitForSeconds(0.5f);
        countdownUi.gameObject.SetActive(false);
    }

    public IEnumerator ButtonMeshTimer(int buttonMashTimerAmount)
    {
        int timer = buttonMashTimerAmount;
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }

        SelectWinner();
    }

    public void SelectWinner()
    {
        // Code that checks which player has the most button mashes
        //ToggleCanMash();
        GameManager.Instance.DeactivateInput();


        buttonMashPlayers = buttonMashPlayers.OrderByDescending(player => player.GetComponent<ButtonMashController>().amountOfButtonMashes).ToList();

        Dictionary<PlayerController, int> playerScores = new();
        foreach (var player in GameManager.Instance.Players)
        {
            playerScores.Add(player, buttonMashPlayers[player.PlayerIndex].amountOfButtonMashes);
        }
        GameManager.Instance.SetScorePerPlayer(playerScores);

        buttonMashTestCounterUGUI.text = "Player 1: " + buttonMashPlayers[0].PlayerControllerReference.PlayerData.pointsThisRound +
                                        " Player 2: " + buttonMashPlayers[1].PlayerControllerReference.PlayerData.pointsThisRound +
                                        " Player 3: " + buttonMashPlayers[2].PlayerControllerReference.PlayerData.pointsThisRound +
                                        " Player 4: " + buttonMashPlayers[3].PlayerControllerReference.PlayerData.pointsThisRound;

        // TODO Add logic for the fart visual
        foreach (var player in buttonMashPlayers)
        {
            player.Fart();
        }




        StartCoroutine(GoToScoreScene());
    }

    IEnumerator GoToScoreScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("ThroneRoom");
    }
}
