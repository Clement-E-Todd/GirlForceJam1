using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    Text scoreText;
    int highScore;

    // Use this for initialization
    void Start () {
        scoreText = GetComponent<Text>();

        if (!PlayerPrefs.HasKey("High Score"))
        {
            PlayerPrefs.SetInt("High Score", 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
        highScore = PlayerPrefs.GetInt("High Score");

        Scene currScene = SceneManager.GetActiveScene();

        if(currScene.buildIndex == 0)
            scoreText.text = ("high score: " + highScore.ToString());
        else
            scoreText.text = highScore.ToString();
    }
}
