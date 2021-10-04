using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveTest : MonoBehaviour
{
    public float speed = 10f;
    public float rotSpeed = 100f;
    void Start()
    {
        
    }

    void Update()
    {
        float v = Input.GetAxis("Vertical")*speed;
        float h = Input.GetAxis("Horizontal") * rotSpeed;


        transform.Translate(0, 0, v * Time.deltaTime);
        transform.Rotate(0, h * Time.deltaTime, 0);
    }
}
