using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour 
{
	public List<GameObject> objectsToToggle;
    PointsManager playerPoints; 
    private bool isGameOver;
	public Text yourScore;
	public Text highScore;

	public Tutorial tutorial;

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

		if (PlayerPrefs.GetInt("SfxEnabled", 1) == 1)
		{
			GetComponent<AudioSource>().Play();

			foreach (SkiMovement ski in FindObjectsOfType<SkiMovement>())
			{
				ski.GetComponent<AudioSource>().Pause();
			}
		}
        
        if(playerPoints.GetPoints() > PlayerPrefs.GetInt("High Score"))
            PlayerPrefs.SetInt("High Score", playerPoints.GetPoints());

		FindObjectOfType<PlayerAnimation>().enabled = false;
		foreach (var ski in FindObjectsOfType<SkiMovement>())
		{
			ski.enabled = false;
		}

		FindObjectOfType<ScoreScreen>().ToggleScoreScreen(true);

		yourScore.text = FindObjectOfType<PointsManager>().GetPoints().ToString();
		highScore.text = PlayerPrefs.GetInt("High Score").ToString();

		tutorial.Hide();
	}

	public void Reset()
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

		if (PlayerPrefs.GetInt("SfxEnabled", 1) == 1)
		{
			foreach (SkiMovement ski in FindObjectsOfType<SkiMovement>())
			{
				ski.GetComponent<AudioSource>().UnPause();
			}
		}

		tutorial.Show();

		FindObjectOfType<SpawnController>().Reset();
	}
}
