using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour 
{
	public bool ignoreFlip;
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		FindObjectOfType<GameOverUI>().GameOver();
	}
}
