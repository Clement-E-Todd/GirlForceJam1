using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicButtonToggle : MonoBehaviour 
{
	public AudioSource MusicSource;
	public Button uiButton;

	private bool activated;

	public void Toggle()
	{
		activated = !activated;

		MusicSource.enabled = activated;

		uiButton.Select();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
