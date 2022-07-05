using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{

    public UDPReceive udpReceive;

    private Light flashlight;

    void Start()
    {
        flashlight = GetComponent<Light>();
        flashlight.intensity = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        string data = udpReceive.data;

        if(data.Length != 0){
            string light = data.Split(',')[2];
       
            if(light == "1")
            {
               flashlight.intensity = 3f;
            }
            if(light == "0")
            {
               flashlight.intensity = 0f;
            }
        }
    }
}
