using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public float follow_speed;
    public Transform target_to_follow;

    private Transform _transform;
    private Vector3 next_position;
    private Vector3 offset;

	// Use this for initialization
	void Awake () {
        _transform = GetComponent<Transform>();
        offset = target_to_follow.position - _transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        next_position = target_to_follow.position;
        next_position -= offset;
        _transform.position = next_position;
	}
}
