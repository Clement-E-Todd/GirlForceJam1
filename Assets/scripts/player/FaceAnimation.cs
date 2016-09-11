using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FaceAnimation : MonoBehaviour {

	public enum State
	{
		React,
		Flinch,
		Grimace
	}

	State CurrentState = State.React;

	public SkiMovement Ski1;
	public SkiMovement Ski2;

	public RuntimeAnimatorController LowIntensityAnim;
	public RuntimeAnimatorController MedIntensityAnim;
	public RuntimeAnimatorController HiIntensityAnim;
	public RuntimeAnimatorController FlinchAnim;
	public RuntimeAnimatorController GrimaceAnim;


	const int MaxMoveSamples = 10;
	const float SampleInterval = 0.05f;
	Vector3 lastSampledSki1Position = Vector3.zero;
	Vector3 lastSampledSki2Position = Vector3.zero;
	Queue<float> MoveSamples = new Queue<float>();
	const float FlinchThreshold = 9f;
	const float FlinchTime = 0.5f;
	const float GrimaceTime = 0.1f;

	void Start ()
	{
		lastSampledSki1Position = Ski1.transform.position;
		lastSampledSki2Position = Ski2.transform.position;
		InvokeRepeating("SampleMovement", SampleInterval, SampleInterval);
	}


	// Update is called once per frame
	void Update ()
	{
		if (CurrentState == State.React)
		{
			HandleReactState();
		}
	}

	void HandleReactState()
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

	void SampleMovement()
	{
		float ski1Distance = Vector3.Distance(lastSampledSki1Position, Ski1.transform.position);
		float ski2Distance = Vector3.Distance(lastSampledSki2Position, Ski2.transform.position);
		MoveSamples.Enqueue(ski1Distance + ski2Distance);
		lastSampledSki1Position = Ski1.transform.position;
		lastSampledSki2Position = Ski2.transform.position;

		while (MoveSamples.Count > MaxMoveSamples)
		{
			MoveSamples.Dequeue();
		}

		float totalSampledDistance = 0f;

		foreach (float sample in MoveSamples)
		{
			totalSampledDistance += sample;
		}

		if (totalSampledDistance > FlinchThreshold && CurrentState != State.Flinch)
		{
			CurrentState = State.Flinch;
			GetComponent<Animator>().runtimeAnimatorController = FlinchAnim;
			CancelInvoke("StartReact");
			Invoke("StartGrimace", FlinchTime);
		}
	}

	void StartGrimace()
	{
		CurrentState = State.Grimace;
		GetComponent<Animator>().runtimeAnimatorController = GrimaceAnim;
		Invoke("StartReact", GrimaceTime);
	}

	void StartReact()
	{
		CurrentState = State.React;
	}
}
