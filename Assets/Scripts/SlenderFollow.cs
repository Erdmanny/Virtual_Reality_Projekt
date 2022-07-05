using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlenderFollow : MonoBehaviour
{
    public GameOverScript gameOverScript;
    private Transform slenderTransform;
    private Transform playerTransform;
    private float speed = 0.5f;
    private float currentSpeed = 0f;
    private float rotationSpeed = 0.8f;
    // Start is called before the first frame update
    private Light flashlight;
    private float randomDirection = 0f;
    void Start()
    {
        
        slenderTransform = transform;
        playerTransform = GameObject.FindWithTag("Player").transform;
        flashlight = GameObject.FindWithTag("Flashlight").GetComponent<Light>();
        InvokeRepeating(nameof(ChangeRandom), 0f, 5f);
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(flashlight.intensity == 3f){
        slenderTransform.rotation = Quaternion.Slerp(slenderTransform.rotation,
            Quaternion.LookRotation(playerTransform.position - slenderTransform.position),
            rotationSpeed*Time.deltaTime);
        } else if(flashlight.intensity == 0f){
            slenderTransform.rotation = Quaternion.Euler(new Vector3(0, randomDirection, 0));
        }
        slenderTransform.position += slenderTransform.forward * currentSpeed * Time.deltaTime;

        if (Vector3.Distance (slenderTransform.position, playerTransform.position) < 3)
        {
            GameOver();
        }
    }

    public void updateSpeed(PlayerInventory playerInventory)
    {
        currentSpeed = speed * (playerInventory.NumberOfPapers + 1);
    }


    public void GameOver()
    {
        gameOverScript.Setup();
    }

    void ChangeRandom()
    {
        randomDirection = Random.rotation.eulerAngles.y;
    }


}
