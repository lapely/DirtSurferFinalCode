using System.Collections;
using System.Collections.Generic;

using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    private CarBody carBody;
    public CarEngine carEngine;
    public CarSteering carSteering;
    public CarBrakes carBrakes;
    private Rigidbody carRigidbody;

    private float forwardInput; // how much you want to accelerate (0 to 1)
    private float reverseInput; // how much you want to decelerate (0 to -1)
    private float throttleInput; // computed result of forward and reverse
    private float pedalBrakeInput;
    private float handBrakeInput;
    private float steerInput; // steering scale from -1 (left) to 1 (right)

    public float carSpeed; // speed parallel to the car body (m/s)

    public float driftScale;

    private float steerAngle;
    private float wheelTorque;

    public WheelCollider wColliderFL;
    public WheelCollider wColliderFR;
    public WheelCollider wColliderBL;
    public WheelCollider wColliderBR;

    void Start()
    {
        carBody = GetComponent<CarBody>();
    }

    void FixedUpdate()
    {

        carSpeed = carBody.BodyVelocity() * Mathf.Sign(carBody.ForwardSpeed());

        ConvertThrottle();
        steerAngle = carSteering.SetSteer(steerInput);
        wheelTorque = carEngine.GetTorque(throttleInput); // TODO change [Get] for [Set]

        carBrakes.SetBrake(pedalBrakeInput, handBrakeInput);


    }
    // --------------------------- user input ---------------------
    public void OnSteering(InputValue input)
    {
        steerInput = input.Get<float>();
    }
    public void OnForward(InputValue input)
    {
        forwardInput = input.Get<float>();
    }
    public void OnReverse(InputValue input)
    {
        reverseInput = -input.Get<float>();
    }
    public void OnHandbrake(InputValue input)
    {
        handBrakeInput = input.Get<float>();
    }
    public void OnGearUp(InputValue input)
    {
        if (carEngine.gear < 10) carEngine.gear++;
    }
    public void OnGearDown(InputValue input)
    {
        if (carEngine.gear > 1) carEngine.gear--;
    }
    public void OnRespawn()
    {
        // put the car back at the starting point
        // doesn't flip the car...
        transform.position = new Vector3(0, 11, 0);
    }
    // --------------------------- user input end -----------------

    private void ConvertThrottle()
    {
        throttleInput = forwardInput + reverseInput;

        if (carSpeed > 5)
        {
            throttleInput = Mathf.Clamp01(throttleInput);
            pedalBrakeInput = -reverseInput;
        }
        else if (carSpeed < -5)
        {
            throttleInput = Mathf.Clamp(throttleInput, -1, 0);
            pedalBrakeInput = forwardInput;
        }
        else
        {
            pedalBrakeInput = 0f;
        }
    }

}
