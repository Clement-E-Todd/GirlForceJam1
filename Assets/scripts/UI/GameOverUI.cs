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
        isGameOver = true;
		Time.timeScale = 0.0f;
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Space) && isGameOver)
		{
            isGameOver = false;
			Time.timeScale = 1.0f;
            GameObject[] rocks = GameObject.FindGameObjectsWithTag("Rock");

            PointsManager playerPoints = FindObjectOfType<PointsManager>();
            playerPoints.ResetPoints();

            foreach (GameObject rock in rocks)
                Destroy(rock);

			foreach (var obj in objectsToToggle)
			{
				obj.SetActive(false);
			}
		}
	}
}
