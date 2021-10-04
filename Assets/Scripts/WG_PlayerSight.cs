using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class WG_PlayerSight : MonoBehaviour
{
    public List<GameObject> targets;

    public float angleRange = 45f;
    public float distance = 5f;
    public bool isCollision = false;

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);
    
    Vector3 direction;

    float dotValue = 0f;

    void Update()
    {
        
        for (int i = 0; i < targets.Count; i++)
        {
            dotValue = Mathf.Cos(Mathf.Deg2Rad * (angleRange / 2));
            direction = targets[i].transform.position - transform.position;
            if (direction.magnitude < distance)
            {
                if (Vector3.Dot(direction.normalized, transform.forward) > dotValue)
                    targets[i].transform.GetChild(0).gameObject.SetActive(true);
                else
                {
                    if (direction.magnitude < 1.5f)
                    {
                        targets[i].transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else
                    {
                        targets[i].transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
            else
                targets[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    /*private void OnDrawGizmos()
    {
        Handles.color = isCollision ? _red : _blue;
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, distance);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, distance);

    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            targets.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            targets.Remove(other.gameObject);
        }
    }
}