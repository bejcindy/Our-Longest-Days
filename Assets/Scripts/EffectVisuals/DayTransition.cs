using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTransition : MonoBehaviour
{
    float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Allows for object to rotate once the game starts indefinitely by the given speed above
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
