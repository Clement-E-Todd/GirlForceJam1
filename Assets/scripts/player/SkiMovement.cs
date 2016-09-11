using UnityEngine;

using System.Collections;

public class SkiMovement : MonoBehaviour {

	public string TriggerName;

	public Vector3 OuterPosition;
	public Vector3 InnerPosisition;

	public float OuterTouchScalar;
	public float InnerTouchScalar;

	private static bool UseMacHack;

	private int Leg1TouchId = -1;
	private int Leg2TouchId = -1;

	float CurrentTriggerValue = 0f;
	float PreviousTriggerValue = 0f;
	const float MaxDistancePerSec = 10f;

	// Update is called once per frame
	void Update ()
	{
		PreviousTriggerValue = CurrentTriggerValue;
		float TargetTriggerValue = GetTargetTriggerValue();
		CurrentTriggerValue = Mathf.MoveTowards(CurrentTriggerValue, TargetTriggerValue, MaxDistancePerSec * Time.deltaTime);
		transform.position = Vector3.LerpUnclamped(OuterPosition, InnerPosisition, CurrentTriggerValue);
	}

	public float GetTriggerValue()
	{
		return CurrentTriggerValue;
	}

	float GetTargetTriggerValue()
	{
		float triggerValue = 0f;

		//Game pad input.
		if (Input.GetJoystickNames().Length > 0)
		{
			triggerValue = Input.GetAxis(TriggerName);
			if (!UseMacHack && triggerValue < 0f)
			{
				UseMacHack = true;
			}

			if (UseMacHack)
			{
				triggerValue = (triggerValue / 2) + 0.5f;
			}
		}

		//Touch-screen input.
		else if (Input.touchSupported)
		{
			//Tracking the position of touches for each leg.
			bool leg1TouchInProg = false;
			bool leg2TouchInProg = false;

			foreach (Touch touch in Input.touches)
			{
				if (touch.position.x < Screen.width / 2)
				{
					if (Leg1TouchId == -1 &&
						touch.fingerId != Leg2TouchId)
					{
						Leg1TouchId = touch.fingerId;
					}
				}

				else
				{
					if (Leg2TouchId == -1 &&
						touch.fingerId != Leg1TouchId)
					{
						Leg2TouchId = touch.fingerId;
					}
				}

				//Move the legs to match touch positions.
				if ((touch.fingerId == Leg1TouchId && TriggerName == "Leg 1") ||
					(touch.fingerId == Leg2TouchId && TriggerName == "Leg 2"))
				{
					float outerTouchPosX = Screen.width * OuterTouchScalar;
					float innerTouchPosX = Screen.width * InnerTouchScalar;
					triggerValue = (touch.position.x - outerTouchPosX) / (innerTouchPosX - outerTouchPosX);
					triggerValue = Mathf.Clamp01(triggerValue);
				}

				// Verify that we should keep tracking the touches for each side
				if (touch.fingerId == Leg1TouchId)
				{
					leg1TouchInProg = true;
				}
				if (touch.fingerId == Leg2TouchId)
				{
					leg2TouchInProg = true;
				}
			}

			//If a touch was released, stop tracking it.
			if (!leg1TouchInProg)
			{
				Leg1TouchId = -1;
			}

			if (!leg2TouchInProg)
			{
				Leg2TouchId = -1;
			}
		}

		else
		{
			Debug.LogError("No valid input device was found.");
		}

		return triggerValue;
	}

	/*void OnGUI()
	{
		if (Input.touchSupported)
		{
			string message = string.Format("Leg1ID: {0}\nLeg2ID: {1}", Leg1TouchId, Leg2TouchId);
			GUI.Label(new Rect(Vector2.zero, new Vector2(Screen.width, Screen.height)), message);
		}
	}*/
}
