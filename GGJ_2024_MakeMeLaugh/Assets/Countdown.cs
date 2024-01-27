using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public float totalTime = 30f; // Total time in seconds
    private float currentTime;
    private bool isTimerRunning = true;

    private void Start()
    {
        currentTime = totalTime;
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
        Debug.Log("Game Over!");
        Time.timeScale = 0f; // Pause the game (set timeScale to 0 to stop time)
    }
}
