using UnityEngine;

public class MusicSource : MonoBehaviour
{
	void Update()
	{
		GetComponent<AudioSource>().volume = (PlayerPrefs.GetInt("MusicEnabled", 1) == 1) ? 1f : 0f;
	}
}
