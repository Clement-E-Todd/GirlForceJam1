using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour 
{
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		FindObjectOfType<GameOverUI>().GameOver();
	}
}
