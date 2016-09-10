using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour 
{
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log("hit rock");

		FindObjectOfType<GameOverUI>().GameOver();
	}
}
