using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    private CarBody carBody;

    public float maxForce;
    public float maxRPM;
    public const float idleRPM = 1000;

    public float input;
    public float RPM;
    public float gear;

    public float wheelRPM;
    public float efficiency;
    private float outputForce;

    private float targetRPM;

    void Start()
    {
        carBody = GetComponent<CarBody>();
        gear = 0f;
        input = 0f;
        RPM = 0f;
        maxRPM = Mathf.Clamp(maxRPM, 1000, 10000);
        maxForce = Mathf.Clamp(maxForce, 10, 10000);
    }

    public float GetTorque(float masterInput)
    {
        input = masterInput;
        wheelRPM = Mathf.Clamp(carBody.MeanRPM(), -500f, 2000f);
        ShiftGear();
        WheelToEngineRPM();
        EngineEfficiency();
        ApplyEfficiency();
        carBody.ApplyTorque(outputForce);
        return outputForce;
    }


    private void ShiftGear()
    {
        // set the gear based on the wheel's RPM
        if (gear > 0)
        {
            gear = Mathf.Clamp(Mathf.CeilToInt(wheelRPM / 200), 1, 10);
        }

        // shit to first gear after the engine has passed the RPM threshold 
        if (gear == 0 && RPM > 8000) // not a [if else] because of gear -1
        {
            gear = 1 * Mathf.Sign(input);
        }

        // prevent engine stall
        if (RPM < idleRPM)
        {
            gear = 0;
        }
    }

    private void WheelToEngineRPM()
    {

        if (gear > 0)
        {
            targetRPM = Mathf.Clamp(
                10f * maxRPM * wheelRPM / (2000f * gear), //convert wheel RPM to engine RPM 
                0f,
                1.1f * maxRPM
                );
            RPM += (targetRPM - RPM) / (10f / gear); // smooth the RPM transition (clutch)
        }
        else if (gear == 0)
        {
            targetRPM = idleRPM;
            RPM = RPM + 500 * Mathf.Abs(input) + (targetRPM - RPM) / 20f; // rev up the engine on neutral gear
            RPM = Mathf.Clamp(RPM, 0f, maxRPM);
        }
        else //gear == -1
        {
            targetRPM = Mathf.Clamp(
                10f * maxRPM * wheelRPM / (2000f * gear), //convert wheel RPM to engine RPM 
                0f,
                1.1f * maxRPM
                );
            RPM += (targetRPM - RPM) / 20f; // smooth the RPM transition (clutch)
        }

    }

    private void EngineEfficiency()
    {

        //this is just the graph converted to text
        if (gear != 0)
        {
            float var1 = Mathf.Abs(gear) * RPM / maxRPM + 0.2f;
            float var2 = 1.2f * (var1 - Mathf.Abs(gear)) / Mathf.Sqrt(Mathf.Abs(gear));
            float var3 = Mathf.Pow((Mathf.Sin(var2) / var2), 5f) - Mathf.Pow(var1 / 10f, 2f);
            float var4 = var3 - Mathf.Pow(10f, (RPM - 1.1f * maxRPM) / 500f);
            efficiency = Mathf.Clamp01(var4);
        }
        else //in case of gear 0 (division by 0)
        {
            efficiency = 0;
        }

    }

    private void ApplyEfficiency()
    {
        outputForce = input * efficiency * maxForce; //scale the input (0 to 1) with the efficiency (0 to 1) and the torque (>0)
    }

}
