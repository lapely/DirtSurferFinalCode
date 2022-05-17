using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSlip : MonoBehaviour
{
    public CarBody carBody;
    public RectTransform FL;
    public RectTransform FR;
    public RectTransform BL;
    public RectTransform BR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FL.localPosition = new Vector3(-22, -265 + carBody.WheelSlip(1) * 2, 0);
        FR.localPosition = new Vector3(22, -265 + carBody.WheelSlip(2) * 2, 0);
        BL.localPosition = new Vector3(-66, -265 + carBody.WheelSlip(3) * 2, 0);
        BR.localPosition = new Vector3(66, -265 + carBody.WheelSlip(4) * 2, 0);

    }
}
