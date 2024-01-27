using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	[SerializeField]
	private List<Color> Colors = new()
	{
		Color.red,
		Color.blue,
		Color.green,
		Color.yellow
	};

	[SerializeField] private List<GameObject> PlayerModels = new();
	private PlayerController PlayerControllerReference => GetComponent<PlayerController>();

	public bool ready = false;
	public int points = 0;
	public int pointsThisRound = 0;
	public Color color => Colors[PlayerControllerReference.PlayerIndex];
	public GameObject playerModel => PlayerModels[PlayerControllerReference.PlayerIndex];

	public bool isPlaying = true;
}