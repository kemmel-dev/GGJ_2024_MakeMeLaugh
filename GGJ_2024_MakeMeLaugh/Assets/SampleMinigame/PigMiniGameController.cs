using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PigMiniGameController : MiniGameController
{
	public int CountDownTime = 5;
	public int GameTime = 30;
	public TextMeshProUGUI TimerText;
	public List<TextMeshProUGUI> PlayerScoreTexts;


	private void Awake()
	{
		MiniGameSetupFinished += OnMiniGameSetupFinished;
		TimerText.text = CountDownTime.ToString();
	}

	private void OnMiniGameSetupFinished()
	{
		StartCoroutine(CountDown());
	}

	private void Update()
	{
		if (GameManager.Instance == null) return;
		foreach (var player in GameManager.Instance.Players)
		{

			PlayerScoreTexts[player.PlayerIndex].text = $"Player {player.PlayerIndex + 1}: {((PigMiniGamePlayerController)PlayerObjects[player.PlayerIndex]).Score}";
		}
	}

	private IEnumerator CountDown()
	{
		for (int i = 0; i < CountDownTime; i++)
		{
			yield return new WaitForSeconds(1);
			TimerText.text = (CountDownTime - i).ToString();
		}
		yield return new WaitForSeconds(1);
		TimerText.text = "GO";
		GameManager.Instance.ActivateInput();
		for (int i = 0; i < GameTime - 1; i++)
		{
			yield return new WaitForSeconds(1);
			TimerText.text = (GameTime - i).ToString();
		}
		yield return new WaitForSeconds(1);
		TimerText.text = "END";
		GameManager.Instance.DeactivateInput();
		Dictionary<PlayerController, int> playerScores = new();
		foreach (var player in GameManager.Instance.Players)
		{
			playerScores.Add(player, ((PigMiniGamePlayerController)PlayerObjects[player.PlayerIndex]).Score);
		}
		GameManager.Instance.SetScorePerPlayer(playerScores);
	}

	private void OnDestroy()
	{
		MiniGameSetupFinished -= OnMiniGameSetupFinished;
	}
}
