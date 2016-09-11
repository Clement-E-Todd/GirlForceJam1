using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameOverUI : MonoBehaviour 
{
	public List<GameObject> objectsToToggle;
    PointsManager playerPoints; 
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
        playerPoints = FindObjectOfType<PointsManager>();
    }

	public void GameOver()
	{
		foreach (var obj in objectsToToggle)
		{
			obj.SetActive(true);
		}
        isGameOver = true;
		Time.timeScale = 0.0f;
        
        if(playerPoints.GetPoints() > PlayerPrefs.GetInt("High Score"))
            PlayerPrefs.SetInt("High Score", playerPoints.GetPoints());

		FindObjectOfType<PlayerAnimation>().enabled = false;
		foreach (var ski in FindObjectsOfType<SkiMovement>())
		{
			ski.enabled = false;
		}
	}

	void Update () 
	{
		bool isTapping = false;

		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				isTapping = true;
				break;
			}
		} 

		if ((Input.GetKeyDown(KeyCode.Space) || isTapping) && isGameOver)
		{
			Reset();
		}
	}

	private void Reset()
	{
		GameObject[] rocks = GameObject.FindGameObjectsWithTag("Rock");
		foreach (GameObject rock in rocks)
			Destroy(rock);

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

		FindObjectOfType<SpawnController>().Reset();
	}
}
