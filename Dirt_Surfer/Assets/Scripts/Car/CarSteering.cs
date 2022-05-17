using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSteering : MonoBehaviour
{

    private CarBody carBody;
    public float slipScale;

    public float input;
    private float currentSteer; // from -1 to 1
    public float maxAngle; // range of the wheel (in angle)
    public float assistStrengh;

    public float outputAngle;

    void Start()
    {
        carBody = GetComponent<CarBody>();
        assistStrengh = Mathf.Clamp(assistStrengh, 10, 60);
        maxAngle = Mathf.Clamp(maxAngle, 10, 60);
    }

    public float SetSteer(float masterInput)
    {
        input = masterInput;
        UpdateSteer();
        carBody.ApplySteering(outputAngle);
        return outputAngle;
    }

    private void UpdateSteer()
    {
        slipScale = 1 + Mathf.Clamp((input * carBody.DriftAngle() / 15), 0, 9);

        //smooth the wheel turning speed
        currentSteer += Mathf.Clamp(input - currentSteer, -0.1f, 0.1f); // slowly move [current steer] toward [input]

        //limit the turn angle based on current speed
        outputAngle = maxAngle * currentSteer * (assistStrengh * slipScale / (Mathf.Abs(carBody.ForwardSpeed()) + slipScale * assistStrengh));
    }
}
