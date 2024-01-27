using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WinScene : MonoBehaviour
{
    public List<GameObject> thrones;
    public List<GameObject> players;

    public TextMeshProUGUI winText;

    private void Start()
    {
        // var winningPlayer = GameManager.Instance.Players.OrderByDescending(player => player.PlayerData.points).First();
        var winningPlayer = GameManager.Instance.Players[1];
        Debug.Log(winningPlayer.PlayerIndex);
        Debug.Log(winningPlayer.PlayerData.color);
        WinForPlayer(winningPlayer.PlayerIndex, winningPlayer.PlayerData.color);
    }

    public void WinForPlayer(int num, Color color)
    {
        winText.color = color;
        for (int i = 0; i < 4; i++)
        {
            if (num == i) continue;
            thrones[i].gameObject.SetActive(false);
            players[i].gameObject.SetActive(false);
        }
    }
}
