using UnityEngine;

public class ReadyController : MonoBehaviour
{
	public PlayerData PlayerData;

	private void Update()
	{
		if (PlayerData.ready)
		{
			GetComponent<SpriteRenderer>().color = PlayerData.color;
		}
	}
}

