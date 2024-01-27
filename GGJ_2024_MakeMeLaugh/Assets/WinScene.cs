using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ThroneRoom.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    public List<GameObject> thrones;
    public List<GameObject> players;

    public TextMeshProUGUI winText;

    private void Start()
    {
        var winningPlayer = GameManager.Instance.Players.OrderByDescending(player => player.PlayerData.points).First();
        WinForPlayer(winningPlayer.PlayerIndex, winningPlayer.PlayerData.color);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnReloadGame();
        }
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

    public void OnReloadGame()
    {
        GameManager.Instance.Players.ForEach(controller => Destroy(controller.gameObject));
        Destroy(GameManager.Instance.gameObject);
        GameManager.Instance = null;
        MiniGamePicker.ClearHashSet();
        SceneManager.LoadScene("JoinScene");
    }
}
