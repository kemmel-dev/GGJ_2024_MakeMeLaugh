using System;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
	public List<SampleMiniGamePlayerController> PlayerObjects = new();
	public SampleMiniGamePlayerController MiniGamePrefab;
	public List<Transform> SpawnPoints = new List<Transform>();

	public event Action MiniGameSetupFinished;

	public void Start()
	{
		if (GameManager.Instance == null)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("JoinScene");
		}
	}

	public void OnMiniGameLoaded()
	{
		for (var i = 0; i < GameManager.Instance.Players.Count; i++)
		{
			PlayerController playerController = GameManager.Instance.Players[i];
			SampleMiniGamePlayerController miniGamePlayerController = Instantiate(MiniGamePrefab, SpawnPoints[i].position, SpawnPoints[i].localRotation);
			miniGamePlayerController.Initialize(playerController);
			PlayerObjects.Add(miniGamePlayerController);
		}

		MiniGameSetupFinished?.Invoke();
	}
}
