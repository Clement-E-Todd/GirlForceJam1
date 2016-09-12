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

	Queue<float> Ski1MoveSamples = new Queue<float>();
	Queue<float> Ski2MoveSamples = new Queue<float>();

	const float FlinchThreshold = 9f;
	const float SoundThreshold = 4f;
	const float FlinchTime = 0.5f;
	const float GrimaceTime = 0.1f;

	float lastVOPlayTime = 0f;

	public AudioClip[] MidIntensityVOs;
	const float chanceToPlayMidIntensityVO = 0.125f;
	const float minPauseBeforeMidIntensityVO = 2.0f;

	public AudioClip[] HiIntensityVOs;
	const float chanceToPlayHiIntensityVO = 0.5f;
	const float minPauseBeforeHiIntensityVO = 1.0f;

	public AudioClip[] FlinchVOs;
	const float chanceToPlayFlinchVO = 1.0f;
	const float minPauseBeforeFlinchVO = 0.1f;

	public AudioClip[] GrimaceVOs;
	const float chanceToPlayGrimaceVO = 1.0f;
	const float minPauseBeforeGrimaceVO = 0.1f;

	void Start ()
	{
		lastSampledSki1Position = Ski1.transform.position;
		lastSampledSki2Position = Ski2.transform.position;
		InvokeRepeating("SampleMovement", SampleInterval, SampleInterval);

		lastVOPlayTime = Time.time;
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
		Animator animator = GetComponent<Animator>();

		if (intensity >= 3)
		{
			if (animator.runtimeAnimatorController != HiIntensityAnim)
			{
				animator.runtimeAnimatorController = HiIntensityAnim;
				PlayHiIntensityVO();
			}
		}

		else if (intensity >= 2)
		{
			if (animator.runtimeAnimatorController != MedIntensityAnim)
			{
				animator.runtimeAnimatorController = MedIntensityAnim;
				PlayMedIntensityVO();
			}
		}

		else if (intensity < 2)
		{
			if (animator.runtimeAnimatorController != LowIntensityAnim)
			{
				animator.runtimeAnimatorController = LowIntensityAnim;
			}
		}
	}

	void SampleMovement()
	{
		float ski1Distance = Vector3.Distance(lastSampledSki1Position, Ski1.transform.position);
		Ski1MoveSamples.Enqueue(ski1Distance);
		lastSampledSki1Position = Ski1.transform.position;

		float ski2Distance = Vector3.Distance(lastSampledSki2Position, Ski2.transform.position);
		Ski2MoveSamples.Enqueue(ski2Distance);
		lastSampledSki2Position = Ski2.transform.position;

		while (Ski1MoveSamples.Count > MaxMoveSamples)
		{
			Ski1MoveSamples.Dequeue();
		}

		while (Ski2MoveSamples.Count > MaxMoveSamples)
		{
			Ski2MoveSamples.Dequeue();
		}

		float ski1TotalSampledDistance = 0f;
		float ski2TotalSampledDistance = 0f;
		float realTotalSampledDistance = 0f;

		foreach (float sample in Ski1MoveSamples)
		{
			ski1TotalSampledDistance += sample;
			realTotalSampledDistance += sample;
		}

		foreach (float sample in Ski2MoveSamples)
		{
			ski2TotalSampledDistance += sample;
			realTotalSampledDistance += sample;
		}

		if (realTotalSampledDistance > FlinchThreshold && CurrentState != State.Flinch)
		{
			CurrentState = State.Flinch;
			GetComponent<Animator>().runtimeAnimatorController = FlinchAnim;
			PlayFlinchVO();
			CancelInvoke("StartReact");
			Invoke("StartGrimace", FlinchTime);
		}

		Ski1.GetComponent<AudioSource>().volume = 0.5f + (ski1TotalSampledDistance / SoundThreshold);
		Ski1.GetComponent<AudioSource>().pitch = 1f + (ski1TotalSampledDistance / SoundThreshold) * 0.25f;

		Ski2.GetComponent<AudioSource>().volume = 0.5f + (ski2TotalSampledDistance / SoundThreshold);
		Ski2.GetComponent<AudioSource>().pitch = 1f + (ski2TotalSampledDistance / SoundThreshold) * 0.25f;
	}

	void StartGrimace()
	{
		CurrentState = State.Grimace;
		GetComponent<Animator>().runtimeAnimatorController = GrimaceAnim;
		PlayGrimaceVO();
		Invoke("StartReact", GrimaceTime);
	}

	void StartReact()
	{
		CurrentState = State.React;
	}

	void PlayMedIntensityVO()
	{
		if (Time.time < lastVOPlayTime + minPauseBeforeMidIntensityVO ||
			Random.value > chanceToPlayMidIntensityVO)
		{
			return;
		}

		AudioSource audioSource = GetComponent<AudioSource>();
		if (audioSource.isPlaying)
		{
			audioSource.Stop();
		}
		audioSource.clip = MidIntensityVOs[Random.Range(0, MidIntensityVOs.Length)];
		audioSource.Play();

		lastVOPlayTime = Time.time;
	}

	void PlayHiIntensityVO()
	{
		if (Time.time < lastVOPlayTime + minPauseBeforeHiIntensityVO ||
			Random.value > chanceToPlayHiIntensityVO)
		{
			return;
		}

		AudioSource audioSource = GetComponent<AudioSource>();
		if (audioSource.isPlaying)
		{
			audioSource.Stop();
		}
		audioSource.clip = HiIntensityVOs[Random.Range(0, HiIntensityVOs.Length)];
		audioSource.Play();

		lastVOPlayTime = Time.time;
	}

	void PlayFlinchVO()
	{
		if (Time.time < lastVOPlayTime + minPauseBeforeFlinchVO ||
			Random.value > chanceToPlayFlinchVO)
		{
			return;
		}

		AudioSource audioSource = GetComponent<AudioSource>();
		if (audioSource.isPlaying)
		{
			audioSource.Stop();
		}
		audioSource.clip = FlinchVOs[Random.Range(0, FlinchVOs.Length)];
		audioSource.Play();

		lastVOPlayTime = Time.time;
	}

	void PlayGrimaceVO()
	{
		if (Time.time < lastVOPlayTime + minPauseBeforeGrimaceVO ||
			Random.value > chanceToPlayGrimaceVO)
		{
			return;
		}

		AudioSource audioSource = GetComponent<AudioSource>();
		if (audioSource.isPlaying)
		{
			audioSource.Stop();
		}
		audioSource.clip = GrimaceVOs[Random.Range(0, GrimaceVOs.Length)];
		audioSource.Play();

		lastVOPlayTime = Time.time;
	}
}
