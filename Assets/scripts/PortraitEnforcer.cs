using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitEnforcer : MonoBehaviour
{
    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        Screen.SetResolution((int)(Screen.currentResolution.height * 0.5625f), Screen.currentResolution.height, true);
    }

    // Update is called once per frame
    void Update()
    {
        //Camera.main.aspect = 0.5625f;
        //Camera.main.ResetProjectionMatrix();
    }
}
