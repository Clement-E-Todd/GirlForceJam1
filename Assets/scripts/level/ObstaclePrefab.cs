using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstaclePrefab : MonoBehaviour 
{
	public float speed = 30;
	public float yLocationToDelete;

	public void Update()
	{
		transform.position += new Vector3(0, speed * Time.deltaTime, 0);

		if (transform.position.y > yLocationToDelete)
		{
			Destroy(gameObject);
		}
	}

}
