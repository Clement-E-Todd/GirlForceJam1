using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
	public GameObject ski1;
	public GameObject ski2;
	public GameObject leg1;
	public GameObject leg2;
	public GameObject waist;

	public Vector3 leg1outerPosition;
	public Vector3 leg2outerPosition;
	public Vector3 leg1innerPosition;
	public Vector3 leg2innerPosition;

	public float leg1outerRotation;
	public float leg2outerRotation;
	public float leg1innerRotation;
	public float leg2innerRotation;

	void Update()
	{
		float ski1TriggerValue = ski1.GetComponent<SkiMovement>().GetTriggerValue();
		float ski2TriggerValue = ski2.GetComponent<SkiMovement>().GetTriggerValue();

		leg1.transform.position = Vector3.LerpUnclamped(leg1outerPosition, leg1innerPosition, ski1TriggerValue);
		leg2.transform.position = Vector3.LerpUnclamped(leg2outerPosition, leg2innerPosition, ski2TriggerValue);

		leg1.transform.eulerAngles = new Vector3(0, 0,
			Mathf.LerpUnclamped(leg1outerRotation, leg1innerRotation, ski1TriggerValue));
		leg2.transform.eulerAngles = new Vector3(0, 0,
			Mathf.LerpUnclamped(leg2outerRotation, leg2innerRotation, ski2TriggerValue));
	}
}
