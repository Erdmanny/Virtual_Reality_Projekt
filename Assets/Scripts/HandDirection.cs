using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDirection : MonoBehaviour
{
    public UDPReceive udpReceive;
    public CharacterController controller;

    public AudioSource audioSrc;
    bool isMoving = false;

    public float speed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;


    // Update is called once per frame
    void Update()
    {
        string data = udpReceive.data;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(data.Length != 0){
        string direction = data.Split(',')[0];

        Vector3 move = transform.right * 0 + transform.forward * 0;

        switch(direction)
        {
            case "1":
                move = transform.right * -0.5f + transform.forward * 0;
                isMoving = true;
                break;
            case "3":
                move = transform.right * 0 + transform.forward * 0.5f;
                isMoving = true;
                break;
            case "5":
                move = transform.right * 0.5f + transform.forward * 0;      
                isMoving = true;          
                break;
            case "-1":
                move = transform.right * 0 + transform.forward * -0.5f;
                isMoving = true;
                break;
            default:
                isMoving = false;
                break;
        }

        if(isMoving){
            if(!audioSrc.isPlaying){
                audioSrc.Play();
            }
        } else {
            audioSrc.Stop();
        }

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

       }
    }
}
