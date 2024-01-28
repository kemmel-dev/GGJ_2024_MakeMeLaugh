using UnityEngine;

public class BasketBehaviour : MonoBehaviour
{
	public int playerIndex = -1;
	public GameObject CreateColorObject;
	public int pigCount = 0;
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.transform.root.name);
		if (other.transform.root.TryGetComponent<PigMiniGamePlayerController>(out var playerController)
			&& playerController.HasPig
			&& playerController.PlayerControllerReference.PlayerIndex == playerIndex)
		{
			StackPig(playerController.DestroyPig());
		}
	}

	private void StackPig(PigBehaviour pig)
	{
		pig.transform.SetParent(transform);
		pig.transform.localPosition = new Vector3(0, 1, 0) * pigCount;
		pig.transform.right = transform.forward;
		pigCount++;
	}

}
