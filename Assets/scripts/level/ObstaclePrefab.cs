using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum ObstaclePrefabDifficulty
{
	Easy,
	Medium,
	Hard
}

public class ObstaclePrefab : MonoBehaviour 
{
	[SerializeField]
	private ObstaclePrefabDifficulty difficulty;

	public float speed = 30;
	public float yLocationToDelete;
    private float yLocationToAddPoint;

	public ObstaclePrefabDifficulty Difficulty
	{
		get 
		{
			return difficulty;
		}
	}

    private bool collectedPoints;

	public void Update()
	{
		transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        yLocationToAddPoint = GameObject.FindGameObjectWithTag("Player").transform.position.y;

		if (transform.position.y > yLocationToDelete)
		{
			Destroy(gameObject);
		}

        if (transform.position.y > yLocationToAddPoint && !collectedPoints)
        {
   
            collectedPoints = true;
            
            PointsManager playerPoints = FindObjectOfType<PointsManager>();
            playerPoints.SetPoints(1);

        }
	}

}
