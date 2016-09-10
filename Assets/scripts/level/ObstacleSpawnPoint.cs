using UnityEngine;
using System.Collections;

public class ObstacleSpawnPoint : MonoBehaviour 
{

	public GameObject obstaclePrefab;

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position, 0.3f);
	}

	// Use this for initialization
	void Awake () 
	{
		GameObject newObject = Instantiate(obstaclePrefab, transform.position, Quaternion.identity) as GameObject;
		newObject.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
