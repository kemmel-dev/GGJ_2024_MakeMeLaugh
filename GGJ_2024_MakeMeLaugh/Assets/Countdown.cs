using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public float totalTime = 30f; // Total time in seconds
    private float currentTime;
    private bool isTimerRunning = true;
    public MiniGameController miniGameController;
    private void Start()
    {
        currentTime = totalTime;
        GameManager.Instance.ActivateInput();
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isTimerRunning = false;
                // Call a function to handle game over or other actions
                GameOver();
            }

            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        int seconds = Mathf.FloorToInt(currentTime);
        timerText.text = seconds.ToString();
    }

    private void GameOver()
    {

        // Perform game over actions here, such as stopping gameplay or showing a game over screen.
        

     
        Dictionary<PlayerController, int> playerScores = new();
        foreach (MiniGamePlayerController player in miniGameController.PlayerObjects)
        {
            TomatoJusticePlayerController tomatoJusticePlayerController = player.GetComponent<TomatoJusticePlayerController>();
            playerScores.Add(player.PlayerControllerReference, tomatoJusticePlayerController.score);
        }

        GameManager.Instance.SetScorePerPlayer(playerScores);
        
        GameManager.Instance.DeactivateInput();
        SceneManager.LoadScene("ThroneRoom");
    }
}
