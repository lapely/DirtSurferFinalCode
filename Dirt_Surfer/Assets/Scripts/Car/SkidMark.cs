using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkidMark : MonoBehaviour
{
    public CarBody carBody;
    public WheelCollider wheelCollider;
    private TrailRenderer trail;
    private Vector3 wPos;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        trail = this.GetComponent<TrailRenderer>();
        //offset = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        wheelCollider.GetWorldPose(out wPos, out Quaternion wRot);
        transform.position = wPos + offset;
        if(Mathf.Abs(carBody.DriftAngle()) > 15 || Mathf.Abs(carBody.WheelMeanSlip()) > 8)
        {
            trail.emitting = true;
        } else
        {
            trail.emitting = false;
        }

        //transform.position = transform.position + offset;
    }
}
