using UnityEngine;

public abstract class MiniGamePlayerController : MonoBehaviour
{
	protected PlayerController PlayerControllerReference;

	public virtual void Initialize(PlayerController playerController)
	{
		PlayerControllerReference = playerController;
	}
}
