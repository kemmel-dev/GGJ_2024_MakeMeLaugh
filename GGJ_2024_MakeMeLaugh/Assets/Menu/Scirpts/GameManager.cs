using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; set; }

	public event Action<PlayerInput> PlayerJoined;

	public List<PlayerController> Players = new List<PlayerController>();

	public CheatActions CheatActions;

	public PlayerInputManager PlayerIM => PlayerInputManager.instance;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(gameObject);
		Instance = this;
		CheatActions = new CheatActions();
		CheatActions.Enable();
		CheatActions.actions.ExtraJoin.performed += ExtraJoinOnPerformed;
		CheatActions.actions.SetAllPlayersToReady.performed += SetAllPlayersToReadyOnPerformed;

		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		var miniGameController = FindObjectOfType<MiniGameController>();
		if (miniGameController == null) return;
		miniGameController.OnMiniGameLoaded();
	}


	private void ExtraJoinOnPerformed(InputAction.CallbackContext ctx)
	{
		if (PlayerIM.playerCount >= PlayerIM.maxPlayerCount) return;
		var playerInput = Instantiate(PlayerIM.playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerInput>();
		playerInput.GetComponent<PlayerController>().PlayerData.isPlaying = false;
		playerInput.DeactivateInput();
	}

	private void SetAllPlayersToReadyOnPerformed(InputAction.CallbackContext ctx)
	{
		for (int i = PlayerIM.playerCount; i < PlayerIM.maxPlayerCount; i++)
		{
			ExtraJoinOnPerformed(ctx);
		}
		DeactivateInput();
		foreach (var player in Players.Where(x => !x.PlayerData.ready))
		{
			player.OnPlayerReady();
		}
	}

	public void DeactivateInput()
	{
		foreach (var player in Players)
		{
			player.GetComponent<PlayerInput>().DeactivateInput();
		}
	}

	public void ActivateInput()
	{
		foreach (var player in Players.Where(x => x.PlayerData.isPlaying))
		{
			player.GetComponent<PlayerInput>().ActivateInput();
		}
	}

	public void OnPlayerJoined(PlayerInput playerInput)
	{
		Players.Add(playerInput.GetComponent<PlayerController>());
		PlayerJoined?.Invoke(playerInput);
	}

	public void SetScorePerPlayer(Dictionary<PlayerController, int> pointPerPlayer)
	{
		Dictionary<int, List<PlayerController>> Scores = new();
		foreach (var playerPointPair in pointPerPlayer)
		{
			var key = playerPointPair.Key;
			var value = playerPointPair.Value;
			if (Scores.ContainsKey(playerPointPair.Value))
			{
				Scores[value].Add(key);
			}
			else
			{
				Scores.Add(value, new List<PlayerController> { key });
			}
		}
		var orderedScores = Scores.OrderByDescending(x => x.Key).ToList();
		int scoreToGive = 3;
		foreach (var kvp in orderedScores)
		{
			foreach (var player in kvp.Value)
			{
				player.PlayerData.pointsThisRound = scoreToGive;
				player.PlayerData.points += scoreToGive;
			}

			scoreToGive -= kvp.Value.Count;
		}
	}

	private void OnDestroy()
	{
		CheatActions.actions.ExtraJoin.performed -= ExtraJoinOnPerformed;
		CheatActions.actions.SetAllPlayersToReady.performed -= SetAllPlayersToReadyOnPerformed;
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}