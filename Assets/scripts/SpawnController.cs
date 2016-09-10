using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour {

	//variables

    //how many obstacles are created per second
	public float PerSecond;
    public List<GameObject> ObCollection;
    //medium
    public List<GameObject> ObCollection2;
    //hard
    public List<GameObject> ObCollection3;
    //timer that calculates how much time has passed since last created
    private float spawnTimer;
    private float totalGameTime;

    //game difficulty times
    public float timeUntilMedium;
    public float timeUntilHard;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        spawnTimer += Time.deltaTime;
        totalGameTime += Time.deltaTime;

        if(spawnTimer > PerSecond) {

            spawnTimer = 0;

            if (totalGameTime > timeUntilMedium && totalGameTime < timeUntilHard)
                spawnObstacleMedium();
            if (totalGameTime > timeUntilHard)
                spawnObstacleHard();
            else
                spawnObstacleEasy();
        }
	}

    private void spawnObstacleEasy()
    {
        int getNewObstacle = Random.Range(0, ObCollection.Count);

        GameObject newObstacle = Instantiate(ObCollection[getNewObstacle], transform.position, Quaternion.identity) as GameObject;

   }

   private void spawnObstacleMedium()
    {
        int getNewObstacle = Random.Range(0, ObCollection2.Count);

        GameObject newObstacle = Instantiate(ObCollection2[getNewObstacle], transform.position, Quaternion.identity) as GameObject;

    }

    private void spawnObstacleHard()
    {
        int getNewObstacle = Random.Range(0, ObCollection3.Count);

        GameObject newObstacle = Instantiate(ObCollection3[getNewObstacle], transform.position, Quaternion.identity) as GameObject;

    }
}
