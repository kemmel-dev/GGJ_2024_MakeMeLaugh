using UnityEngine;

public class PigBehaviour : MonoBehaviour
{
	public int Points = 1;
	public PigMiniGameController pigMiniGameController;
	public BoxCollider ModelCollider;
	public Rigidbody ModelRigidBody;
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

	public void Collect(PlayerController pc)
	{
		foreach (var player in pigMiniGameController.PlayerObjects)
		{
			if (player is PigMiniGamePlayerController pmgpc)
			{
				if (pmgpc.GrabbablePigs.Contains(this))
				{
					pmgpc.GrabbablePigs.Remove(this);
				}
			}
		}
		Destroy(GetComponent<BoxCollider>());
		ModelCollider.enabled = true;
		ModelCollider.gameObject.layer = LayerMask.NameToLayer($"p{pc.PlayerIndex + 1}");
		ModelCollider.excludeLayers = LayerMask.GetMask($"p{pc.PlayerIndex + 1}");
		ModelRigidBody.excludeLayers = LayerMask.GetMask($"p{pc.PlayerIndex + 1}");
	}
}
