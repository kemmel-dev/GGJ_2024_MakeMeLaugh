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
                SceneManager.LoadScene(SceneManager.GetSceneByName(sceneName).buildIndex);
            }
            
            int pickedIndex;

            if (_playedMiniGames.Count >= 5)
            {
                ClearHashSet();
            }
            
            while (true)
            {
                pickedIndex =  Random.Range(3, SceneManager.sceneCountInBuildSettings - 3 + 1);
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