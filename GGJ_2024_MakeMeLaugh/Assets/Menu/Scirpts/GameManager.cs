using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

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
		playerInput.DeactivateInput();
	}

	private void SetAllPlayersToReadyOnPerformed(InputAction.CallbackContext ctx)
	{
		for (int i = PlayerIM.playerCount; i < PlayerIM.maxPlayerCount; i++)
		{
			ExtraJoinOnPerformed(ctx);
		}
		foreach (var player in Players)
		{
			player.PlayerData.ready = true;
		}
	}

	public void OnPlayerJoined(PlayerInput playerInput)
	{
		Players.Add(playerInput.GetComponent<PlayerController>());
		PlayerJoined?.Invoke(playerInput);
	}
}