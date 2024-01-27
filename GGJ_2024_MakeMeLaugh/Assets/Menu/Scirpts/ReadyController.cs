using UnityEngine;

public class ReadyController : MonoBehaviour
{
	private PlayerController playerController;

	[SerializeField] private Transform GFX;
	private Animator _animator;

	public void init(PlayerController playerController)
	{
		_animator = GetComponent<Animator>();
		this.playerController = playerController;
		this.playerController.ReadyAction += OnReadyAction;
	}

	public void SavePosition()
	{
		_animator.enabled = false;
		transform.position = GFX.position;
		GFX.localPosition = Vector3.zero;
		GFX.rotation = Quaternion.Euler(0, 0, 0);
		playerController.PlayerData.ready = true;
	}

	public void OnReadyAction()
	{
		_animator.SetTrigger("StartAnim");
	}

	private void OnDestroy()
	{
		playerController.ReadyAction -= OnReadyAction;
	}
}

