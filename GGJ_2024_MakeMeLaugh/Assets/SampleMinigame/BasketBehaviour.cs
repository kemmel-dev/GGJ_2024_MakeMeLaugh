using UnityEngine;

public class BasketBehaviour : MonoBehaviour
{
	public int playerIndex = -1;
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent<PigMiniGamePlayerController>(out var playerController)
			&& playerController.HasPig
			&& playerController.PlayerControllerReference.PlayerIndex == playerIndex)
		{
			playerController.DestroyPig();
		}
	}
}
