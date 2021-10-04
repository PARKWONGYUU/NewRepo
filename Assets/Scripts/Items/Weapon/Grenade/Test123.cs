using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test123 : MonoBehaviour
{
    private LineRenderer line;
    private Vector3 TouchPos;
    private Vector3 startPos;
    private Vector3 endPos;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(line == null)
                createLine();
            TouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TouchPos.z = 0;
            line.SetPosition(0, TouchPos);
            line.SetPosition(1, TouchPos);
            startPos = TouchPos;
        }
        else if(Input.GetMouseButtonUp(0)&&line)
        {
            if(line)
            {
                TouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                TouchPos.z = 0;
                line.SetPosition(1, TouchPos);
                endPos = TouchPos;
                addColliderToLine();
                line = null;
            }
        }
        else if(Input.GetMouseButton(0))
        {
            if(line)
            {
                TouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                TouchPos.z = 0;
                line.SetPosition(1, TouchPos);
            }
        }

    }

    private void createLine()
    {
        line = new GameObject("Line").AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Diffuse"));
       /* line.SetVertexCount(2);
        line.SetWidth(0.1f, 0.1f);
        line.SetColors(Color.black, Color.black);*/
        line.useWorldSpace = true;
    }
    void addColliderToLine()
    {
        BoxCollider col = new GameObject("Collider").AddComponent<BoxCollider>();
        col.transform.parent = line.transform;
        float lineLength = Vector3.Distance(startPos, endPos);
        col.size = new Vector3(lineLength, 0.1f, 1f);
        Vector3 midPoint = (startPos + endPos) / 2;
        col.transform.position = midPoint;
        float angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x));
        if((startPos.y < endPos.y && startPos.x > endPos.x)||(endPos.y < startPos.y && endPos.x> startPos.x))
        {
            angle *= -1;
        }

        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        col.transform.Rotate(0, 0, angle);
    }
 
}
