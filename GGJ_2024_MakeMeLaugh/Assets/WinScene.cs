using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinScene : MonoBehaviour
{
    public List<GameObject> thrones;
    public List<GameObject> players;

    private void Start()
    {
        WinForPlayer(
            GameManager.Instance.Players.OrderByDescending(player => player.PlayerData.points).First().PlayerIndex
        );
    }

    public void WinForPlayer(int num)
    {
        for (int i = 0; i < 4; i++)
        {
            if (num == i) continue;
            thrones[i].gameObject.SetActive(false);
            players[i].gameObject.SetActive(false);
        }
    }
}
