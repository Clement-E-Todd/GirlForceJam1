using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
	public GameObject ski1;
	public GameObject ski2;
	public GameObject leg1;
	public GameObject leg2;
	public GameObject waist;
	public GameObject torso;
	public GameObject head;

	public Vector3 leg1outerPosition;
	public Vector3 leg2outerPosition;
	public Vector3 leg1innerPosition;
	public Vector3 leg2innerPosition;

	public float leg1outerRotation;
	public float leg2outerRotation;
	public float leg1innerRotation;
	public float leg2innerRotation;

	public Vector3 waistUpperPosition;
	public Vector3 waistLowerPosition;
	public float waistRotationAmount;

	public Vector3 torsoUpperPosition;
	public Vector3 torsoLowerPosition;
	public float torsoRotationAmount;

	public Vector3 headUpperPosition;
	public Vector3 headLowerPosition;
	public float headRotationAmount;

	void Update()
	{
		float ski1TriggerValue = ski1.GetComponent<SkiMovement>().GetTriggerValue();
		float ski2TriggerValue = ski2.GetComponent<SkiMovement>().GetTriggerValue();
		float averageTriggerValue = (ski1TriggerValue + ski2TriggerValue) / 2;
		float rotationValue = 0.5f + (ski2TriggerValue - ski1TriggerValue) / 2;

		leg1.transform.localPosition = Vector3.LerpUnclamped(leg1outerPosition, leg1innerPosition, ski1TriggerValue);
		leg2.transform.localPosition = Vector3.LerpUnclamped(leg2outerPosition, leg2innerPosition, ski2TriggerValue);

		leg1.transform.localEulerAngles = new Vector3(0, 0,
			Mathf.LerpUnclamped(leg1outerRotation, leg1innerRotation, ski1TriggerValue));
		leg2.transform.localEulerAngles = new Vector3(0, 0,
			Mathf.LerpUnclamped(leg2outerRotation, leg2innerRotation, ski2TriggerValue));

		waist.transform.localPosition = Vector3.LerpUnclamped(waistLowerPosition, waistUpperPosition, averageTriggerValue);
		waist.transform.localEulerAngles = new Vector3(0, 0,
			Mathf.LerpUnclamped(-waistRotationAmount , waistRotationAmount, rotationValue));

		torso.transform.localPosition = Vector3.LerpUnclamped(torsoUpperPosition, torsoLowerPosition, averageTriggerValue);
		torso.transform.localEulerAngles = new Vector3(0, 0,
			Mathf.LerpUnclamped(torsoRotationAmount , -torsoRotationAmount, rotationValue));

		head.transform.localPosition = Vector3.LerpUnclamped(headUpperPosition, headLowerPosition, averageTriggerValue);
		head.transform.localEulerAngles = new Vector3(0, 0,
			Mathf.LerpUnclamped(headRotationAmount , -headRotationAmount, rotationValue));
	}
}
