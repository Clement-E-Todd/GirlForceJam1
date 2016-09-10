using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ObstaclePrefab : MonoBehaviour 
{
	public float speed = 30;
	public float yLocationToDelete;
    private float yLocationToAddPoint;


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
            Debug.Log("Passed");
            PointsManager playerPoints = FindObjectOfType<PointsManager>();
            playerPoints.SetPoints(1);

        }
	}

}
