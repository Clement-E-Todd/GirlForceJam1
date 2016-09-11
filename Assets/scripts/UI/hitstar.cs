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
		transform.localScale = Vector3.zero;
		startedTime = Time.time;
	}

	private void Update()
	{
		runTime = Time.time - startedTime;;
		transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(totalScale, totalScale,totalScale), runTime/totalScaleTime); 
	}

}
