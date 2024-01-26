using UnityEngine;
using UnityEngine.InputSystem;

public class SampleInput : MonoBehaviour
{
	private PlayerContoller _PlayerControler;

	private void Awake()
	{
		_PlayerControler = GetComponent<PlayerContoller>();
		_PlayerControler.LeftStick += _PlayerControler_LeftStick;
		_PlayerControler.NorthButton += _PlayerControler_NorthButton;
		_PlayerControler.SouthButton += _PlayerControler_SouthButton;
		_PlayerControler.WestButton += _PlayerControler_WestButton;
		_PlayerControler.EastButton += _PlayerControler_EastButton;
		_PlayerControler.RightTrigger += _PlayerControler_RightTrigger;
		_PlayerControler.LeftTrigger += _PlayerControler_LeftTrigger;
	}
	private void _PlayerControler_LeftStick(InputAction.CallbackContext obj)
	{
		Debug.Log($"{name}, leftStick");
	}
	private void _PlayerControler_NorthButton(InputAction.CallbackContext obj)
	{
		Debug.Log($"{name}, NorthButton");
	}
	private void _PlayerControler_SouthButton(InputAction.CallbackContext obj)
	{
		Debug.Log($"{name}, SouthButton");
	}
	private void _PlayerControler_WestButton(InputAction.CallbackContext obj)
	{
		Debug.Log($"{name}, WestButton");
	}
	private void _PlayerControler_EastButton(InputAction.CallbackContext obj)
	{
		Debug.Log($"{name}, EastButton");
	}
	private void _PlayerControler_RightTrigger(InputAction.CallbackContext obj)
	{
		Debug.Log($"{name}, RightTrigger");
	}
	private void _PlayerControler_LeftTrigger(InputAction.CallbackContext obj)
	{
		Debug.Log($"{name}, leftTrigger");
	}

	private void OnDestroy()
	{
		_PlayerControler.LeftStick -= _PlayerControler_LeftStick;
		_PlayerControler.NorthButton -= _PlayerControler_NorthButton;
		_PlayerControler.SouthButton -= _PlayerControler_SouthButton;
		_PlayerControler.WestButton -= _PlayerControler_WestButton;
		_PlayerControler.EastButton -= _PlayerControler_EastButton;
		_PlayerControler.RightTrigger -= _PlayerControler_RightTrigger;
		_PlayerControler.LeftTrigger -= _PlayerControler_LeftTrigger;
	}
}
