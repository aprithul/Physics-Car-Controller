using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HudController : MonoBehaviour {

    public Text speed_text;

    public static HudController Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<HudController>();
            if (_instance == null)
                Debug.Log("Scene must have atleast one HudController script");

            return _instance;
        }
    }
    private static HudController _instance;


	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void update_speed(float speed)
    {
        speed_text.text = speed.ToString();
    }
}
