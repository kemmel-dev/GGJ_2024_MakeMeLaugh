using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace ThroneRoom.Scripts
{
    public static class MiniGamePicker
    {
        private static HashSet<int> _playedMiniGames = new ();

        public static int PickMiniGame(string sceneName = null)
        {
            if (sceneName != null)
            {
                SceneManager.LoadScene(sceneName);
            }
            
            int pickedIndex = -500;

            if (_playedMiniGames.Count >= 5)
            {
                ClearHashSet();
            }

            for (int i = 0; i < 10000; i++)
            {
                pickedIndex =  Random.Range(3, 7 + 1);
                
                if (_playedMiniGames.Contains(pickedIndex)) continue;
                _playedMiniGames.Add(pickedIndex);
                break;
            }

            return pickedIndex;
        }

        public static void ClearHashSet()
        {
            _playedMiniGames = new();
        }
    }
}