using UnityEngine;

public abstract class MiniGamePlayerController : MonoBehaviour
{
	public PlayerController PlayerControllerReference;

	public virtual void Initialize(PlayerController playerController)
	{
		PlayerControllerReference = playerController;
	}
}
