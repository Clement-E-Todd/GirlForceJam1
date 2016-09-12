using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour {

	public void LoadTitle()
	{
		foreach (Button button in FindObjectsOfType<Button>())
		{
			button.gameObject.SetActive(false);
		}

		SceneManager.LoadScene(0);
	}
}
