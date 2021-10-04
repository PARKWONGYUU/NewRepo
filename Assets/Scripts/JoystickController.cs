using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour
{
    public Transform playerPos;
    public RectTransform stickPos;
    public float radiusRange;
    void Start()
    {
        radiusRange = 45f;
    }

    // Update is called once per frame
    void Update()
    {
        

    }

}
