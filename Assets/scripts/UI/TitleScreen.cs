﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

	public void loadScene()
    {
		Time.timeScale = 1f;

		foreach (Button button in FindObjectsOfType<Button>())
		{
			button.gameObject.SetActive(false);
		}

        SceneManager.LoadScene(1);
    }
}
