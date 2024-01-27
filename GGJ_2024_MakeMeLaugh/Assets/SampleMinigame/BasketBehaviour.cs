using UnityEngine;

public class BasketBehaviour : MonoBehaviour
{
	public int playerIndex = -1;
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.root.TryGetComponent<PigMiniGamePlayerController>(out var playerController)
			&& playerController.HasPig
			&& playerController.PlayerControllerReference.PlayerIndex == playerIndex)
		{
			playerController.DestroyPig();
		}
	}
}
