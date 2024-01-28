using UnityEngine;

public class BackgroundTune : MonoBehaviour
{
	public AudioSource source;


	private void OnDestroy()
	{
		source.Stop();
		source.clip = null;

	}
}
