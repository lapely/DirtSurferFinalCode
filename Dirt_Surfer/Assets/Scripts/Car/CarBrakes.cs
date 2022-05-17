using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBrakes : MonoBehaviour
{
    private CarBody carBody;

    public float maxBrakeForce;

    private float brakeFL;
    private float brakeFR;
    private float brakeBL;
    private float brakeBR;

    public float outputBrakeFL;
    public float outputBrakeFR;
    public float outputBrakeBL;
    public float outputBrakeBR;

    void Start()
    {
        carBody = GetComponent<CarBody>();
    }

    public void SetBrake(float pedalBrake, float handBrake)
    {
        // for each wheel
        // -------------------------------------------------------------------
        brakeFL -= 100 * (1 - pedalBrake); // slowly release the brake
        brakeFL += pedalBrake * (-100) / Mathf.Clamp(carBody.WheelSlip(1), -500f, -1f); // increase brake force based on how little the car is slipping
        if (carBody.WheelSlip(1) < -0.1f)
        {
            brakeFL += 2 * carBody.WheelSlip(1); // release brake force in case of slippage 
        }
        brakeFL = Mathf.Clamp(brakeFL, 0, maxBrakeForce);
        outputBrakeFL = brakeFL;

        // -------------------------------------------------------------------
        brakeFR -= 100 * (1 - pedalBrake);
        brakeFR += pedalBrake * (-100) / Mathf.Clamp(carBody.WheelSlip(2), -500f, -1f);
        if (carBody.WheelSlip(2) < -0.1f)
        {
            brakeFR += 2 * carBody.WheelSlip(2);
        }
        brakeFR = Mathf.Clamp(brakeFR, 0, maxBrakeForce);
        outputBrakeFR = brakeFR;

        // -------------------------------------------------------------------
        brakeBL -= 100 * (1 - pedalBrake);
        brakeBL += pedalBrake * (-100) / Mathf.Clamp(carBody.WheelSlip(3), -500f, -1f);
        if (carBody.WheelSlip(3) < -0.1f)
        {
            brakeBL += 2 * carBody.WheelSlip(3);
        }
        brakeBL = Mathf.Clamp(brakeBL, 0, maxBrakeForce);
        outputBrakeBL = Mathf.Max(brakeBL, handBrake * 1500);

        // -------------------------------------------------------------------
        brakeBR -= 100 * (1 - pedalBrake);
        brakeBR += pedalBrake * (-100) / Mathf.Clamp(carBody.WheelSlip(4), -500f, -1f);
        if (carBody.WheelSlip(4) < -0.1f)
        {
            brakeBR += 2 * carBody.WheelSlip(4);
        }
        brakeBR = Mathf.Clamp(brakeBR, 0, maxBrakeForce);
        outputBrakeBR = Mathf.Max(brakeBR, handBrake * 1500);


        carBody.ApplyBrake(outputBrakeFL, outputBrakeFR, outputBrakeBL, outputBrakeBR);
    }
}
