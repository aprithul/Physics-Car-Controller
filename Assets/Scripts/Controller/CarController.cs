using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public enum DriveType
    {
        front_wheel = 0,
        rear_wheel  = 1,
        four_wheel  = 2
    }


    public float max_speed;
    public float max_motor_torque;
    public float max_brake_troque;
    public float max_steering_angle;
    public float brake_percet_front;
    public DriveType drive_type;
    public Transform center_of_mass;
    public WheelCollider fl;
    public WheelCollider fr;
    public WheelCollider rl;
    public WheelCollider rr;

    private float front_wheel_brake_torque;
    private float rear_wheel_brake_torque;
    private Rigidbody _rigidbody;
    // Use this for initialization
    void Awake () {
        this.max_speed = max_speed * 1000f / 3600f;
        this._rigidbody = GetComponent<Rigidbody>();
        this._rigidbody.centerOfMass = center_of_mass.position;
        this.front_wheel_brake_torque = max_brake_troque * brake_percet_front;
        this.rear_wheel_brake_torque = max_brake_troque * (1-brake_percet_front);
    }

    // Update is called once per frame
    void FixedUpdate () {
        HudController.Instance.update_speed(_rigidbody.velocity.magnitude*3600f/1000f);
 
        accelerate(max_motor_torque * Input.GetAxisRaw("Accelerate"));
        brake(front_wheel_brake_torque * Input.GetAxis("Brake"), rear_wheel_brake_torque * Input.GetAxis("Brake"));
        steer(max_steering_angle    * Input.GetAxis("Steer"));
    }

    public void accelerate(float motor_torque)
    {
        if (_rigidbody.velocity.magnitude < max_speed)
        {
            switch (drive_type)
            {
                case DriveType.front_wheel:
                    fl.motorTorque = motor_torque;
                    fr.motorTorque = motor_torque;
                    break;
                case DriveType.rear_wheel:
                    rl.motorTorque = motor_torque;
                    rr.motorTorque = motor_torque;
                    break;
                case DriveType.four_wheel:
                    fl.motorTorque = motor_torque;
                    fr.motorTorque = motor_torque;
                    rl.motorTorque = motor_torque;
                    rr.motorTorque = motor_torque;
                    break;
            }
        }

    }

    public void brake(float front_wheel_brake_torque, float rear_wheel_brake_torque)
    {
        fl.brakeTorque = front_wheel_brake_torque;
        fr.brakeTorque = front_wheel_brake_torque;
        rl.brakeTorque = rear_wheel_brake_torque;
        rr.brakeTorque = rear_wheel_brake_torque;
    }
    
    public void steer(float angle)
    {
        fl.steerAngle = angle;
        fr.steerAngle = angle;
    }
}
