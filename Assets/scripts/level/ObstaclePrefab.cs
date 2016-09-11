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

	public float baseSpeed = 7f;
	public float yLocationToDelete;
    private float yLocationToAddPoint;

	private const float speedMultiplerBasedOnTime = 0.1f;

	public ObstaclePrefabDifficulty Difficulty
	{
		get 
		{
			return difficulty;
		}
	}

    private bool collectedPoints;
	private SpawnController spawnController;

	private void Start()
	{
		spawnController = FindObjectOfType<SpawnController>();
	}

	public bool IsUpcoming()
	{
		//if we've not collected points for this, it means the obstacle is upcoming;
		return !collectedPoints;
	}

	public void Update()
	{
		
		float speed = baseSpeed + (speedMultiplerBasedOnTime * spawnController.TotalGameTime);

		transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        yLocationToAddPoint = GameObject.FindGameObjectWithTag("Player").transform.position.y;

		if (transform.position.y > yLocationToDelete)
		{
			Destroy(gameObject);
		}

        if (transform.position.y > yLocationToAddPoint && !collectedPoints && !this.tag.Equals("Branch"))
        {
   
            collectedPoints = true;
            
            PointsManager playerPoints = FindObjectOfType<PointsManager>();
            playerPoints.SetPoints(1);

        }
	}

}
