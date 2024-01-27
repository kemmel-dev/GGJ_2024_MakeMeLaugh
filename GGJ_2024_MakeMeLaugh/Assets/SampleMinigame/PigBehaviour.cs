using System.Linq;
using UnityEngine;

public class PigBehaviour : MonoBehaviour
{
	public int Points = 1;
	public PigMiniGameController pigMiniGameController;
	public BoxCollider ModelCollider;
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.root.TryGetComponent<PigMiniGamePlayerController>(out var playerController))
		{
			playerController.GrabbablePigs.Add(this);
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.transform.root.TryGetComponent<PigMiniGamePlayerController>(out var playerController))
		{
			playerController.GrabbablePigs.Remove(this);
		}
	}

	public void Collect()
	{
		foreach (var pigPlayerController in pigMiniGameController.PlayerObjects.Select(x => x as PigMiniGamePlayerController))
		{
			if (pigPlayerController != null && pigPlayerController.GrabbablePigs.Contains(this))
			{
				pigPlayerController.GrabbablePigs.Remove(this);
			}
		}
		Destroy(GetComponent<BoxCollider>());
		ModelCollider.enabled = true;
	}
}
