using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class JoinManager : MonoBehaviour
{
	public ReadyController ReadyControllerPrefab;
	public List<Transform> SpawnPoints = new List<Transform>();

	private List<PlayerInput> _Players = new List<PlayerInput>();

	[SerializeField] private string firstSceneName;

	private void Start()
	{
		GameManager.Instance.PlayerJoined += OnPlayerJoin;
	}

	public void OnPlayerJoin(PlayerInput playerInput)
	{
		_Players.Add(playerInput);
		Instantiate(ReadyControllerPrefab, SpawnPoints[playerInput.playerIndex].position, Quaternion.identity).PlayerData = playerInput.GetComponent<PlayerData>();
	}


	private void Update()
	{
		if (_Players.Count >= 4 && _Players.All(x => x.GetComponent<PlayerData>().ready))
		{
			GameManager.Instance.PlayerIM.DisableJoining();
			UnityEngine.SceneManagement.SceneManager.LoadScene(firstSceneName);
		}
	}

	private void OnDestroy()
	{
		GameManager.Instance.PlayerJoined -= OnPlayerJoin;
	}
}

