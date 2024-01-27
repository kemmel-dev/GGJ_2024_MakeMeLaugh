using UnityEngine;

public class PigBehaviour : MonoBehaviour
{
	public int Points = 1;
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent<PigMiniGamePlayerController>(out var playerController))
		{
			playerController.GrabbablePigs.Add(this);
		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.TryGetComponent<PigMiniGamePlayerController>(out var playerController))
		{
			playerController.GrabbablePigs.Remove(this);
		}
	}

	public void Collect()
	{
		Destroy(GetComponent<BoxCollider2D>());
	}
}
