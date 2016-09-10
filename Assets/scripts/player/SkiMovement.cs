using UnityEngine;
using System.Collections;

public class SkiMovement : MonoBehaviour {

	public string TriggerName;

	public Vector3 OuterPosition;
	public Vector3 InnerPosisition;

	// Update is called once per frame
	void Update ()
	{
		float triggerValue = Input.GetAxis(TriggerName);
		transform.position = Vector3.Lerp(OuterPosition, InnerPosisition, triggerValue);
	}
}
