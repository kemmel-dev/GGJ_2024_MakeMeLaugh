using UnityEngine;

public class PigBehaviour : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent<SampleMiniGamePlayerController>(out var playerController))
		{
			Debug.Log($"collect");
			playerController.GrabbablePigs.Add(this);
		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.TryGetComponent<SampleMiniGamePlayerController>(out var playerController))
		{
			Debug.Log($"collect");
			playerController.GrabbablePigs.Remove(this);
		}
	}

	public void Collect()
	{
		Debug.Log($"collect");
		Destroy(GetComponent<Collider>());
	}
}
