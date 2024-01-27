using UnityEngine;

public class ReadyController : MonoBehaviour
{
	public PlayerData playerData;
	public Color ReadyColor = Color.green;

	private void Update()
	{
		if (playerData.ready)
		{
			GetComponent<SpriteRenderer>().color = ReadyColor;
		}
	}
}

