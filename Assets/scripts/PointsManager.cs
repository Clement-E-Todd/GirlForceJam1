using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour {

    private int playerPoints;
    private Text pointText;

    private void Start()
    {
        pointText = GetComponent<Text>();
    }

	public void SetPoints(int points)
    {
        playerPoints += points;
        pointText.text = playerPoints.ToString();
    }

    public void ResetPoints()
    {
        playerPoints = 0;
        pointText.text = playerPoints.ToString();
    }
}
