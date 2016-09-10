using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameOverUI : MonoBehaviour 
{
	public List<GameObject> objectsToToggle;
	private bool isGameOver;

	void Start () 
	{
	
	}

	public void GameOver()
	{
		foreach (var obj in objectsToToggle)
		{
			obj.SetActive(true);
		}

		Time.timeScale = 0.0f;
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Time.timeScale = 1.0f;

			foreach (var obj in objectsToToggle)
			{
				obj.SetActive(false);
			}
		}
	}
}
