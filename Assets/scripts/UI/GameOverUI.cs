using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameOverUI : MonoBehaviour 
{
	public List<GameObject> objectsToToggle;
	private bool isGameOver;

	public bool IsGameOver
	{
		get
		{
			return isGameOver;
		}
	}

	void Start () 
	{
	
	}

	public void GameOver()
	{
		foreach (var obj in objectsToToggle)
		{
			obj.SetActive(true);
		}
        isGameOver = true;
		Time.timeScale = 0.0f;

		FindObjectOfType<PlayerAnimation>().enabled = false;
		foreach (var ski in FindObjectsOfType<SkiMovement>())
		{
			ski.enabled = false;
		}
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Space) && isGameOver)
		{
			GameObject[] rocks = GameObject.FindGameObjectsWithTag("Rock");
			foreach (GameObject rock in rocks)
				Destroy(rock);

			PointsManager playerPoints = FindObjectOfType<PointsManager>();
			playerPoints.ResetPoints();

            isGameOver = false;
			Time.timeScale = 1.0f;


			FindObjectOfType<PlayerAnimation>().enabled = true;
			foreach (var ski in FindObjectsOfType<SkiMovement>())
			{
				ski.enabled = true;
			}

			foreach (var obj in objectsToToggle)
			{
				obj.SetActive(false);
			}
		}
	}
}
