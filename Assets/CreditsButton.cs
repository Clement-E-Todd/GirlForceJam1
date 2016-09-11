using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsButton : MonoBehaviour {

	public void LoadCredits()
	{
		SceneManager.LoadScene(2);
	}
}
