using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyCarController : MonoBehaviour {


    public float engine_force;
    public float max_steering_angle;

    public Transform front_wheel;
    public Transform rear_wheel;

    public Transform fl;
    public Transform fr;
    public Transform rl;
    public Transform rr;


    private Rigidbody front_wheel_rb;
    private Rigidbody rear_wheel_rb;
    private Quaternion front_wheel_rotation;
    private Quaternion rear_wheel_rotation;
    private Quaternion car_rotation;
    private Vector3 car_heading;
    private Vector3 front_wheel_last_position;
    private float front_wheel_displacement_magnitude;
    private float rear_wheel_displacement_magnitude;
    private float car_length;
    private float half_axle_length;

    // Use this for initialization
    void Awake () {

        front_wheel_rb = front_wheel.GetComponent<Rigidbody>();
        rear_wheel_rb = rear_wheel.GetComponent<Rigidbody>();

        front_wheel_last_position = front_wheel.position;
        car_heading = front_wheel.position - rear_wheel.position;
        car_length = car_heading.magnitude;
        half_axle_length = Vector3.Distance(fl.position, fr.position)/2f;
        car_rotation = Quaternion.LookRotation(car_heading, Vector3.up);
        
    }

    // Update is called once per frame
    void FixedUpdate () {


        front_wheel_rotation = Quaternion.AngleAxis(Input.GetAxis("Steer") * max_steering_angle, front_wheel.up);
        front_wheel_rb.MoveRotation(car_rotation * front_wheel_rotation);
        front_wheel_rb.AddForce(engine_force * Input.GetAxis("Accelerate") * front_wheel.forward);

        front_wheel_rb.velocity = front_wheel.forward * MathUtil.get_y_component(front_wheel.forward, front_wheel_rb.velocity) + front_wheel.right * MathUtil.get_x_component(front_wheel.forward, front_wheel_rb.velocity)*0.7f;

        float _x_component = MathUtil.get_x_component(car_heading, front_wheel.position - front_wheel_last_position);
        float unknown      =  Mathf.Sqrt((car_length * car_length) - (_x_component * _x_component)) - MathUtil.get_y_component(car_heading, front_wheel.position - front_wheel_last_position);

        rear_wheel.position = rear_wheel.position + ((car_length - unknown) * car_heading.normalized);
        rear_wheel.rotation = car_rotation;

        update_wheel_graphics();
        front_wheel_last_position = front_wheel.position;
        car_heading = front_wheel.position - rear_wheel.position;
        car_rotation = Quaternion.LookRotation(car_heading, Vector3.up);
        
    }

    void update_wheel_graphics()
    {

        Vector3 right = MathUtil.get_right_vector(car_heading);
        Debug.DrawRay(front_wheel.position, right * 1000, Color.red);

        fl.position = front_wheel.position + (-right.normalized * half_axle_length);
        fr.position = front_wheel.position + (right.normalized * half_axle_length);
        rl.position = rear_wheel.position + (-right.normalized * half_axle_length);
        rr.position = rear_wheel.position + (right.normalized * half_axle_length);

        Debug.Log(fl.position);

        fl.rotation = front_wheel.rotation;
        fr.rotation = front_wheel.rotation;
        rl.rotation = rear_wheel.rotation;
        rr.rotation = rear_wheel.rotation;
    }



}
