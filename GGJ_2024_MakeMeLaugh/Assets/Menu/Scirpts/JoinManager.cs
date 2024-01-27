using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinManager : MonoBehaviour
{
	public List<ReadyController> ReadyControllerPrefabs = new List<ReadyController>();
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
		Instantiate(ReadyControllerPrefabs[playerInput.playerIndex], SpawnPoints[playerInput.playerIndex].position, Quaternion.identity)
			.init(playerInput.GetComponent<PlayerController>());
	}


	private void Update()
	{
		if (_Players.Count >= 4 && _Players.All(x => x.GetComponent<PlayerData>().ready))
		{
			GameManager.Instance.PlayerIM.DisableJoining();


			Debug.LogWarning("TODO: Load correct scene");

			UnityEngine.SceneManagement.SceneManager.LoadScene(firstSceneName);

		}
	}

	private void OnDestroy()
	{
		GameManager.Instance.PlayerJoined -= OnPlayerJoin;
	}
}

