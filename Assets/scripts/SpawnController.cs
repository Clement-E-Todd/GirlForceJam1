using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour {

	//variables

    //how many obstacles are created per second
	public float PerSecond;
    public List<GameObject> ObCollection;
    //timer that calculates how much time has passed since last created
    private float spawnTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        spawnTimer += Time.deltaTime;

        if(spawnTimer > PerSecond) {
            spawnObstacle();
        }
	}

    private void spawnObstacle()
    {
        Debug.Log("Hi");

        int getNewObstacle = Random.Range(0, 5);//maxlist length

        spawnTimer = 0;
    }
}
