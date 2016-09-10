using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    Text scoreText;
    int highScore;

    // Use this for initialization
    void Start () {
        scoreText = GetComponent<Text>();
        PlayerPrefs.SetInt("High Score", 0);
    }
	
	// Update is called once per frame
	void Update () {
        highScore = PlayerPrefs.GetInt("High Score");
        scoreText.text = highScore.ToString();
    }
}
