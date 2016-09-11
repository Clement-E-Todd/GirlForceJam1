using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreScreen : MonoBehaviour 
{
	public List<GameObject> toggleObjects;

	public void ToggleScoreScreen(bool show)
	{
		foreach(var obj in toggleObjects)
		{
			obj.SetActive(show);
		}
	}
}
