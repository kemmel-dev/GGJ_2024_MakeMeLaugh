using UnityEngine;

public class BasketBehaviour : MonoBehaviour
{
	public int playerIndex = -1;
	public GameObject CreateColorObject;
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.transform.root.name);
		if (other.transform.root.TryGetComponent<PigMiniGamePlayerController>(out var playerController)
			&& playerController.HasPig
			&& playerController.PlayerControllerReference.PlayerIndex == playerIndex)
		{
			playerController.DestroyPig();
		}
	}
}
