using UnityEngine;
using System.Collections;

public class MaterialOffset : MonoBehaviour {

    public float scrollSpeed;
    private Renderer renderer;

	private const float speedMultiplerBasedOnTime = 0.1f;
	private SpawnController spawnController;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
		spawnController = FindObjectOfType<SpawnController>();
	}
	
	// Update is called once per frame
	void Update () {
		float materialOffset = Time.time * (scrollSpeed - (speedMultiplerBasedOnTime * spawnController.TotalGameTime));
        Vector2 vect = new Vector2(0, materialOffset);

        renderer.material.SetTextureOffset("_MainTex", vect);
	}
}
