using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ThroneRoom.Scripts
{
    public class ThroneRoomManager : MonoBehaviour
    {
        [SerializeField] private int numSteps;
        [SerializeField] private List<Throne> _thrones;
        [SerializeField] private List<ThroneRoomPlayer> _throneRoomPlayers;

        public void Start()
        {
            _thrones.ForEach(throne => throne.Arranger.Arrange(numSteps));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AdvancePlayer(0, 3);
            }
        }

        private async Task AdvancePlayer(int playerNum, int amountOfSteps)
        {
            _thrones[playerNum].Emotion.ShowEmotion(amountOfSteps);
            for (int i = 0; i < amountOfSteps; i++)
            {
                await _throneRoomPlayers[playerNum].AdvanceStep();
                _thrones[playerNum].Score.IncrementScore(1);
            }
        }
    }
}

