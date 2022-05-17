using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBody : MonoBehaviour
{
    private Rigidbody carRigidbody;
    public Vector3 CoM;

    private Vector3 localVelocity;

    private float avgWheelRPM;
    private float driftAngle;

    public WheelCollider wColliderFL;
    public WheelCollider wColliderFR;
    public WheelCollider wColliderBL;
    public WheelCollider wColliderBR;

    // Start is called before the first frame update
    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        //Set the car's center of mass (CoM)
        carRigidbody.centerOfMass = CoM;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // average RPM of all 4 wheels
        avgWheelRPM = (wColliderBR.rpm + wColliderBL.rpm + wColliderFR.rpm + wColliderFL.rpm) / 4;

        // velocity vector relative to the car rotation
        localVelocity = transform.InverseTransformDirection(carRigidbody.velocity);

        // angle between the direction the car is going and the direction the car is pointing
        driftAngle = Mathf.Rad2Deg * Mathf.Asin(localVelocity.x / localVelocity.magnitude);
    }
    // --------------------------------------------- Convertion ---------------------------------------------
    /// <summary>
    /// Convert the speed (<paramref name="inputSpeed"/>) from m/s to RPM
    /// </summary>
    public float SpeedToRpm(float inputSpeed)
    {
        return 60 * inputSpeed / (wColliderFR.radius * Mathf.PI);
    }
    /// <summary>
    /// Convert the RPM (<paramref name="inputRpm"/>) from RPM to m/s
    /// </summary>
    public float RpmToSpeed(float inputRpm)
    {
        return Mathf.PI * wColliderFR.radius * inputRpm / 60;
    }
    // --------------------------------------------- Body Info ---------------------------------------------
    public float BodyVelocity()
    {
        return localVelocity.magnitude;
    }
    /// <summary>
    /// The forward speed of the car's body. The car's speed along its local Z axis in m/s.
    /// </summary>
    public float ForwardSpeed()
    {
        return localVelocity.z;
    }

    /// <summary>
    /// The Sideway speed of the car's body. The car's speed along its local X axis in m/s.
    /// </summary>
    public float SidewaySpeed()
    {
        return localVelocity.x;
    }

    /// <summary>
    /// The forward speed of the car's body converted to RPM.
    /// </summary>
    public float BodyRPM()
    {
        return SpeedToRpm(localVelocity.z);
    }

    public float DriftAngle()
    {
        return driftAngle;
    }
    // --------------------------------------------- Wheel Info ---------------------------------------------
    /// <summary>
    /// The average RPM of all 4 wheels.
    /// </summary>
    public float WheelRPM()
    {
        return avgWheelRPM;
    }
    /// <summary>
    /// The average speed of all 4 wheels in m/s.
    /// </summary>
    public float WheelSpeed()
    {
        return RpmToSpeed(avgWheelRPM);
    }
    /// <summary>
    /// The speed difference beetween a wheel and the car.
    /// <paramref name="position"/> is the position of the wheel.
    /// | 0 All Wheels | 1 Front Left | 2 Front Right | 3 Back Left | 4 Back Right
    /// </summary>
    public float WheelSlip(int position)
    {
        return position switch // what kind of cursed switch syntax is this
        {
            1 => RpmToSpeed(wColliderFL.rpm) - localVelocity.z,
            2 => RpmToSpeed(wColliderFR.rpm) - localVelocity.z,
            3 => RpmToSpeed(wColliderBL.rpm) - localVelocity.z,
            4 => RpmToSpeed(wColliderBR.rpm) - localVelocity.z,
            _ => RpmToSpeed(avgWheelRPM) - localVelocity.z
        };
    }
    public float MeanRPM()
    {
        float mean = 0;

        mean = avgWheelRPM * 4;
        mean -= Mathf.Max(wColliderBR.rpm, wColliderBL.rpm, wColliderFR.rpm, wColliderFL.rpm);

        mean = mean / 3;

        return mean;
    }

    public float WheelMeanSlip()
    {
        float meanSlip = 0;
        float minSlip = 0;
        float maxSlip = 0;

        for (int i = 1; i <= 4; i++)
        {
            float slip = WheelSlip(i);
            meanSlip += slip;
            if (minSlip > slip) minSlip = slip;
            if (maxSlip < slip) maxSlip = slip;
        }
        meanSlip -= minSlip;
        meanSlip -= maxSlip;
        meanSlip = meanSlip / 2;

        return meanSlip;
    }

    public void ApplyTorque(float inputTorque)
    {
        wColliderFL.motorTorque = inputTorque;
        wColliderFR.motorTorque = inputTorque;
        wColliderBL.motorTorque = inputTorque;
        wColliderBR.motorTorque = inputTorque;
    }
    public void ApplyBrake(float brakeFL, float brakeFR, float brakeBL, float brakeBR)
    {
        wColliderFL.brakeTorque = brakeFL;
        wColliderFR.brakeTorque = brakeFR;
        wColliderBL.brakeTorque = brakeBL;
        wColliderBR.brakeTorque = brakeBR;
    }
    public void ApplySteering(float inputSteer)
    {
        wColliderFL.steerAngle = inputSteer;
        wColliderFR.steerAngle = inputSteer;
    }

}
