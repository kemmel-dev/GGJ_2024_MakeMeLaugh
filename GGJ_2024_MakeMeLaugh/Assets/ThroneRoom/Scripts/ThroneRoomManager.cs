using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace ThroneRoom.Scripts
{
    public class ThroneRoomManager : MonoBehaviour
    {
        [SerializeField] private int numSteps;
        [SerializeField] private List<Throne> _thrones;
        [SerializeField] private List<ThroneRoomPlayer> _throneRoomPlayers;
        [SerializeField] private int delayBeforeStartingMilliseconds = 1000;
        [SerializeField] private int delayBeforeSceneSwitchMilliSeconds = 3000;
        [SerializeField] private MiniGamePreviewPanel _previewPanel;

        public void Start()
        {
            _thrones.ForEach(throne => throne.Arranger.Arrange(numSteps));
            StartThroneSequence();
        }

        public async void StartThroneSequence()
        {

            // Load all player score data
            foreach (var playerController in GameManager.Instance.Players)
            {
                SetPlayerToPoint(playerController.PlayerIndex, playerController.PlayerData.points - playerController.PlayerData.pointsThisRound);
            }
            
            // Wait before advancing
            await Task.Delay(delayBeforeStartingMilliseconds);
            
            var tasks = new List<Task>();
            // Load all player score data
            foreach (var playerController in GameManager.Instance.Players)
            {
                tasks.Add(AdvancePlayer(playerController.PlayerIndex, playerController.PlayerData.pointsThisRound));
            }

            // Wait until all players have advanced
            await Task.WhenAll(tasks);
            
            // Additional delay
            await Task.Delay(delayBeforeSceneSwitchMilliSeconds);


            if (GameManager.Instance.Players.Any(player => player.PlayerData.points >= 15))
            {
                SceneManager.LoadScene("WinScene");
            }
            else
            {
                _previewPanel.StartMiniGamePicker(MiniGamePicker.PickMiniGame());
            }
        }

        private void SetPlayerToPoint(int playerNum, int points)
        {
            // Assign an already existing score to a player
            _throneRoomPlayers[playerNum].SetToStep(points);
            _thrones[playerNum].Score.SetScore(points);
        }

        private async Task AdvancePlayer(int playerNum, int amountOfSteps)
        {
            // Show emoji
            _thrones[playerNum].Emotion.ShowEmotion(amountOfSteps);
            // Take number of steps
            for (int i = 0; i < amountOfSteps; i++)
            {
                if (_thrones[playerNum].Score.IncrementScore(1))
                {
                    // Player won!
                    await Task.Delay(1500);
                    continue;
                }
                await _throneRoomPlayers[playerNum].AdvanceStep();
            }
        }
    }
}

