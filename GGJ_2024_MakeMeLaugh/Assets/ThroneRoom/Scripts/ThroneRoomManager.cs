using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
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

        public void Start()
        {
            _thrones.ForEach(throne => throne.Arranger.Arrange(numSteps));
            StartThroneSequence();
        }

        public async void StartThroneSequence()
        {
            var tasks = new List<Task>();
            
            // Wait before advancing
            await Task.Delay(delayBeforeStartingMilliseconds);
            
            // Load all player score data
            foreach (var playerController in GameManager.Instance.Players)
            {
                var player = playerController.PlayerData;
                var index = playerController.PlayerIndex;
                SetPlayerToPoint(index, player.points);
                tasks.Add(AdvancePlayer(index, player.pointsThisRound));
            }

            // Wait until all players have advanced
            await Task.WhenAll(tasks);
            
            // Additional delay
            await Task.Delay(delayBeforeSceneSwitchMilliSeconds);
            LoadNewMinigame();
        }

        private void LoadNewMinigame()
        {
            Debug.LogWarning("Load minigame here!");
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
                await _throneRoomPlayers[playerNum].AdvanceStep();
                _thrones[playerNum].Score.IncrementScore(1);
            }
        }
    }
}

