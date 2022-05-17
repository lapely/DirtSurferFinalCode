using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EngineInfo : MonoBehaviour
{
    public CarEngine carEngine;
    public TextMeshProUGUI rpmTxt;
    public RectTransform rpmNeedPos;
    public TextMeshProUGUI gearTxt;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rpmTxt.text = (Mathf.Round(carEngine.RPM / 100) * 100).ToString() + " rpm";

        rpmNeedPos.localPosition = new Vector3(267 + carEngine.RPM / 52, -280, 0);

        gearTxt.text = carEngine.gear.ToString();
    }
}
