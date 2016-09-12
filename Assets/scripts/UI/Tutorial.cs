using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
	void Awake()
	{
		Show();
	}

	public void Show()
	{
		gameObject.SetActive(true);

		CancelInvoke();
		Invoke("Hide", 5f);
	}

	public void Hide()
	{
		CancelInvoke();
		gameObject.SetActive(false);
	}
}
