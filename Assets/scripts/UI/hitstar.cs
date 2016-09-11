using UnityEngine;
using System.Collections;

public class hitstar : MonoBehaviour 
{
	public float totalScaleTime = 1.0f;
	public float totalScale = 20f;

	private float runTime;
	private float startedTime;
	private void Awake()
	{
		
	}

	private void OnEnable()
	{
		transform.localScale = Vector3.zero;
		startedTime = Time.realtimeSinceStartup;
	}

	void Update() 
	{
		runTime = Time.realtimeSinceStartup - startedTime;
		transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(totalScale, totalScale,totalScale), runTime/totalScaleTime); 
	}

}
