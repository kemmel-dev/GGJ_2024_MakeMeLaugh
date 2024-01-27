using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinManager : MonoBehaviour
{
	public ReadyController readyControllerPrefab;
	public List<Transform> spawnPoints = new List<Transform>();

	private List<PlayerInput> players = new List<PlayerInput>();

	private void Start()
	{
		GameManager.Instance.PlayerJoined += OnPlayerJoin;
	}

	public void OnPlayerJoin(PlayerInput playerInput)
	{
		players.Add(playerInput);
		Instantiate(readyControllerPrefab, spawnPoints[playerInput.playerIndex].position, Quaternion.identity).playerData = playerInput.GetComponent<PlayerData>();
	}


	private void Update()
	{
		if (players.Count >= 2 && players.All(x => x.GetComponent<PlayerData>().ready))
		{
			PlayerInputManager.instance.DisableJoining();
			//TODO:
			//UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
			Debug.Log("Load scene");
		}
	}

	private void OnDestroy()
	{
		GameManager.Instance.PlayerJoined -= OnPlayerJoin;
	}
}

