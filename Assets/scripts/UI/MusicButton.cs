using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
	public Sprite enabledSprite;
	public Sprite disabledSprite;

	public void Start()
	{
		GetComponent<Image>().sprite = (PlayerPrefs.GetInt("MusicEnabled", 1) == 1) ? enabledSprite : disabledSprite;
	}

	public void Toggle()
	{
		PlayerPrefs.SetInt("MusicEnabled", 1 - PlayerPrefs.GetInt("MusicEnabled", 1));
		GetComponent<Image>().sprite = (PlayerPrefs.GetInt("MusicEnabled", 1) == 1) ? enabledSprite : disabledSprite;
	}
}
