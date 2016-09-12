using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsButton : MonoBehaviour {

	public void LoadCredits()
	{
		foreach (Button button in FindObjectsOfType<Button>())
		{
			button.gameObject.SetActive(false);
		}

		SceneManager.LoadScene(2);
	}
}
