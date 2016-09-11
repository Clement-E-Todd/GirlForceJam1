using UnityEngine;
using System.Collections;

public class MaterialOffset : MonoBehaviour {

    public float scrollSpeed;
    private Renderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        float materialOffset = Time.time * scrollSpeed;
        Vector2 vect = new Vector2(0, materialOffset);

        renderer.material.SetTextureOffset("_MainTex", vect);
	}
}
