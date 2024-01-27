using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public event Action<PlayerInput> PlayerJoined;


	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
			return;
		}
		Instance = this;
	}

	public void OnPlayerJoined(PlayerInput playerInput)
	{
		PlayerJoined?.Invoke(playerInput);
	}
}