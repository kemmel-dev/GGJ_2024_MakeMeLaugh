using UnityEngine;

public class ReadyController : MonoBehaviour
{
	public PlayerData PlayerData;

	private void Update()
	{
		if (PlayerData.ready)
		{
			//TODO: set some sort of sprite to checkmark
			//GetComponent<SpriteRenderer>().color = PlayerData.color;
		}
	}
}

