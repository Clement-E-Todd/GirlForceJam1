using UnityEngine;
using System.Collections;

public class SkiMovement : MonoBehaviour {

	public string TriggerName;

	public Vector3 OuterPosition;
	public Vector3 InnerPosisition;

	// Update is called once per frame
	void Update ()
	{
		float triggerValue = GetTriggerValue();
		transform.position = Vector3.Lerp(OuterPosition, InnerPosisition, triggerValue);
	}

	public float GetTriggerValue()
	{
		return Input.GetAxis(TriggerName);
	}
}
