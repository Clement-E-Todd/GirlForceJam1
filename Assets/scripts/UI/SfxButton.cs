using UnityEngine;
using UnityEngine.UI;

public class SfxButton : MonoBehaviour
{
	public Sprite enabledSprite;
	public Sprite disabledSprite;

	public void Start()
	{
		GetComponent<Image>().sprite = (PlayerPrefs.GetInt("SfxEnabled", 1) == 1) ? enabledSprite : disabledSprite;
	}

	public void Toggle()
	{
		PlayerPrefs.SetInt("SfxEnabled", 1 - PlayerPrefs.GetInt("SfxEnabled", 1));
		GetComponent<Image>().sprite = (PlayerPrefs.GetInt("SfxEnabled", 1) == 1) ? enabledSprite : disabledSprite;
	}
}
