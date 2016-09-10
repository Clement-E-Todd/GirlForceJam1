using UnityEngine;
using System.Collections;

public class SkiMovement : MonoBehaviour {

	public string TriggerName;

	public Vector3 OuterPosition;
	public Vector3 InnerPosisition;

	private static bool UseMacHack;

	// Update is called once per frame
	void Update ()
	{
		float triggerValue = GetTriggerValue();
		transform.position = Vector3.LerpUnclamped(OuterPosition, InnerPosisition, triggerValue);
	}

	public float GetTriggerValue()
	{
		float triggerValue = Input.GetAxis(TriggerName);
		if (!UseMacHack && triggerValue < 0f)
		{
			UseMacHack = true;
		}

		if (UseMacHack)
		{
			triggerValue = (triggerValue/2) + 0.5f;
		}
		return triggerValue;
	}
}
