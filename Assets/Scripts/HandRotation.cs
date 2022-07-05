using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRotation : MonoBehaviour
{

    public UDPReceive udpReceive;
    public float speed = 100f;

    public Transform playerBody;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        string data = udpReceive.data;

        if(data.Length != 0){
            string rotation = data.Split(',')[1];
       
            float handX;
            if(rotation == "1")
            {
                handX = -0.6f * speed * Time.deltaTime;
                playerBody.Rotate(Vector3.up * handX);
            }
            if(rotation == "2")
            {
                handX = -0.3f * speed * Time.deltaTime;
                playerBody.Rotate(Vector3.up * handX);
            }
            if(rotation == "4")
            {
                handX = 0.3f * speed * Time.deltaTime;
                playerBody.Rotate(Vector3.up * handX);
            }
            if(rotation == "5")
            {
                handX = 0.6f * speed * Time.deltaTime;
                playerBody.Rotate(Vector3.up * handX);
            }

        }
    }
}
