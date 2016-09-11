using UnityEngine;
using System.Collections;

public class FaceAnimation : MonoBehaviour {

	public SkiMovement Ski1;
	public SkiMovement Ski2;

	public RuntimeAnimatorController LowIntensityAnim;
	public RuntimeAnimatorController MedIntensityAnim;
	public RuntimeAnimatorController HiIntensityAnim;
	public RuntimeAnimatorController FlinchAnim;
	public RuntimeAnimatorController GrimaceAnim;


	// Update is called once per frame
	void Update ()
	{
		//Get all data needed to calculate intensity score.
		float skiAverage = (Ski1.GetTriggerValue() + Ski2.GetTriggerValue()) / 2;

		ObstaclePrefabDifficulty difficulty = ObstaclePrefabDifficulty.Easy;

		foreach (ObstaclePrefab obstacle in
			FindObjectOfType<SpawnController>().GetUpcomingPrefabs())
		{
			if (obstacle.Difficulty > difficulty)
			{
				difficulty = obstacle.Difficulty;
			}
		}

		//Calculate intesity score.
		int difficultyScore = (int)difficulty;
		int skiScore = (int)((0.25f + (1f - skiAverage)) * 2f);

		int intensity = difficultyScore + skiScore;

		//Select face based on intensity score.
		if (intensity >= 3)
		{
			GetComponent<Animator>().runtimeAnimatorController = HiIntensityAnim;
		}

		else if (intensity >= 2)
		{
			GetComponent<Animator>().runtimeAnimatorController = MedIntensityAnim;
		}

		else
		{
			GetComponent<Animator>().runtimeAnimatorController = LowIntensityAnim;
		}
	}
}
