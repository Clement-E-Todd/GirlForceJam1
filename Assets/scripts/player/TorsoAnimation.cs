using UnityEngine;
using System.Collections;

public class TorsoAnimation : MonoBehaviour {

	public SkiMovement Ski1;
	public SkiMovement Ski2;

	public RuntimeAnimatorController LowFlailAnim;
	public RuntimeAnimatorController MedFlailAnim;
	public RuntimeAnimatorController HiFlailAnim;

	const float mediumFlailStart = 0.333f;
	const float lowFlailStart = 0.666f;

	// Update is called once per frame
	void Update ()
	{
		float skiAverage = (Ski1.GetTriggerValue() + Ski2.GetTriggerValue()) / 2;

		if (skiAverage > lowFlailStart)
		{
			GetComponent<Animator>().runtimeAnimatorController = LowFlailAnim;
		}

		else if (skiAverage > mediumFlailStart)
		{
			GetComponent<Animator>().runtimeAnimatorController = MedFlailAnim;
		}

		else
		{
			GetComponent<Animator>().runtimeAnimatorController = HiFlailAnim;
		}
	}
}
