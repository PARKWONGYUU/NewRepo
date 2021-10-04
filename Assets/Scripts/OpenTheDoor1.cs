using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenTheDoor1 : MonoBehaviour
{
    public GameObject doorPopup;
    public GameObject door;
    public bool openDoor;
    public float doorY;
    public bool closeDoor;
    public bool ceiling;
    public List<GameObject> ceilingObject = new List<GameObject>();
    public bool exist = false;
    public bool groundwalking = false;
    public float existCurTime;
    public float existCoolTime = 0.3f;
    public int ceilingNum;

    void Update()
    {
        if (openDoor)
        {
            if (doorY < 90)
            {
                doorY += 0.5f;
                door.transform.parent.rotation = Quaternion.Euler(0, doorY, 0);
                if (doorY == 89)
                {
                    doorY = 90;
                    openDoor = false;
                    door.transform.parent.rotation = Quaternion.Euler(0, doorY, 0);
                }

            }

        }
        if (closeDoor)
        {
            doorY -= 0.5f;
            door.transform.parent.rotation = Quaternion.Euler(0, doorY, 0);
            if (doorY == 1)
            {
                doorY = 0;
                door.transform.parent.rotation = Quaternion.Euler(0, doorY, 0);
                openDoor = false;
                closeDoor = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            if (openDoor == false && closeDoor == false)
            {
                Debug.Log("¹®");
                door = other.gameObject;
                if (door.transform.parent.rotation.y > 5f && door.transform.parent.rotation.y < 89f)
                {
                    Debug.Log("0µÊ");
                    doorY = 0;
                }
                else if (door.transform.parent.rotation.y >= 89f)
                {
                    Debug.Log("90µÊ");
                    doorY = 90;
                }
                Debug.Log(door.transform.parent.rotation.y);
                if (door.transform.parent.rotation.y == 0)
                {
                    doorY = 0;
                    Debug.Log("´ÝÈû »óÅÂ");
                }
                else if (door.transform.parent.rotation.y == 0.7071068f)
                {
                    doorY = 90;
                    Debug.Log("¿­¸² »óÅÂ");
                }
                Debug.Log(other.gameObject.transform.parent.name);
                doorPopup.SetActive(true);
            }
        }
        if (other.gameObject.tag == "Floor")
        {
            ceilingObject.Insert(0, other.gameObject.transform.GetChild(0).gameObject);
            ceilingObject[0].name = other.gameObject.transform.parent.name;
            Debug.Log("ÁöºØ ¾ø¾îÁü");
            ceilingObject[0].SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
            for (int i = 0; i < ceilingObject.Count; i++)
            {

                if (ceilingObject[i].name == other.gameObject.transform.parent.name)
                {
                    ceilingObject[i].SetActive(true);
                    ceilingObject.RemoveAt(i);
                    Debug.Log("ÁöºØ »ý±è");
                }
            }
        }
        if(other.gameObject.tag == "Door")
        {
            if(doorPopup.activeInHierarchy == true)
            {
                doorPopup.SetActive(false);
            }
        }
    }

    public void Taeho_OpenBtn()
    {
        if (doorY == 0)
        {
            openDoor = true;
            closeDoor = false;
            doorPopup.SetActive(false);
        }
        if (doorY >= 89)
        {
            closeDoor = true;
            openDoor = false;
            doorPopup.SetActive(false);
        }

    }
    void Taeho_CancelBtn()
    {
        doorPopup.SetActive(false);
    }
}
