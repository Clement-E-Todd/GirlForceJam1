using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForegroundSpawner : MonoBehaviour {


    public float minPerSeconds;
    public float maxPerSeconds;

    private float spawnBranchFreq;

    private int currBranch;
    private float spawnTimer;

    public GameObject[] treeBranches;

    private float randomSpawnTime(float min, float max)
    {
        spawnBranchFreq = Random.Range(min, max);
        return spawnBranchFreq;
    }

	// Use this for initialization
	void Start () {
        randomSpawnTime(minPerSeconds, maxPerSeconds);
    }

    // Update is called once per frame
    void Update () {

        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnBranchFreq)
        {
            Debug.Log("hellooo");
            spawnTimer = 0;
            currBranch = Random.Range(0, treeBranches.Length);
            GameObject currTreeBranch = Instantiate(treeBranches[currBranch], transform.position, Quaternion.identity) as GameObject;

            randomSpawnTime(minPerSeconds, maxPerSeconds);
        }


    }
}
