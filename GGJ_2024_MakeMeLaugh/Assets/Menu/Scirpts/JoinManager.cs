using System.Collections.Generic;
using System.Linq;
using ThroneRoom.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinManager : MonoBehaviour
{
	public List<ReadyController> ReadyControllerPrefabs = new List<ReadyController>();
	public List<Transform> SpawnPoints = new List<Transform>();

	private List<PlayerInput> _Players = new List<PlayerInput>();

	public MiniGamePreviewPanel PreviewPanel;
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

	private bool gamePicked;

	private void Update()
	{
		if (!gamePicked && _Players.Count >= 4 && _Players.All(x => x.GetComponent<PlayerData>().ready))
		{
			GameManager.Instance.PlayerIM.DisableJoining();
			var pickedIndex = MiniGamePicker.PickMiniGame();
			PreviewPanel.StartMiniGamePicker(pickedIndex);
			gamePicked = true;
		}
	}

	private void OnDestroy()
	{
		GameManager.Instance.PlayerJoined -= OnPlayerJoin;
	}
}

