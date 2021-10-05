using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ChangeMat : MonoBehaviour
{
    public List<GameObject> wall;
    public GameObject roofObject;
    public Material roofMat;
    public Material wallMat;
    public float matAlpha = 0.01f;
    public float roofalpha;
    public float floorAlpha;
    public bool roof_Aaterial_Alpha_255 = false;
    public bool roof_Material_Alpha_0 = false;
    public MeshRenderer[] roofMeshRenderers;
    public MeshRenderer[] wallMeshRenderers;
    public bool exit = false;
    public bool exitFloor = false;
    public bool buildingIn;
    public bool material_Alpha_0_Wall = false;
    public bool material_Alpha_255_Wall = false;
    public int floorName;


    public float time = 0f;
    public float FadeTime = 2f;


    void Start()
    {
        roofalpha = 255f;
        floorAlpha = 255f;
    }

    // Update is called once per frame
    void Update()
    {
        if (roof_Material_Alpha_0) //메테리얼 알파값 255 만드는 조건
        {
            for (int i = 0; i < roofMeshRenderers.Length; i++)
            {
                roofMeshRenderers[i].material.color = new Color(roofMat.color.r, roofMat.color.g, roofMat.color.b, roofalpha*Time.deltaTime);
                roofalpha += matAlpha;
            }
            if (roofalpha >= 250)
            {
                Debug.Log("0RMas");
                for (int j = 0; j < roofMeshRenderers.Length; j++)
                {
                    roofMeshRenderers[j].material.color = new Color(roofMat.color.r, roofMat.color.g, roofMat.color.b, 255);
                }
                time = 0;
                roofalpha = 255;
                roof_Material_Alpha_0 = false;
            }
        }
        if (roof_Aaterial_Alpha_255)//메테리얼 알파값 0 만드는 조건
        {
            for (int i = 0; i < roofMeshRenderers.Length; i++)
            {
                roofMeshRenderers[i].material.color = new Color(roofMat.color.r, roofMat.color.g, roofMat.color.b, roofalpha * Time.deltaTime);
                roofalpha -= matAlpha;
            }
            if (roofalpha <= 5)
            {
                Debug.Log("255RMas");
                for (int j = 0; j < roofMeshRenderers.Length; j++)
                {
                    roofMeshRenderers[j].material.color = new Color(roofMat.color.r, roofMat.color.g, roofMat.color.b, 0);
                }
                time = 0;
                roofalpha = 0;
                roof_Aaterial_Alpha_255 = false;
            }
        }
        if(material_Alpha_0_Wall)
        {
            for (int i = 0; i < wallMeshRenderers.Length; i++)
            {
                wallMeshRenderers[i].material.color = new Color(roofMat.color.r, roofMat.color.g, roofMat.color.b, floorAlpha * Time.deltaTime);
                floorAlpha += matAlpha;
            }
            if (floorAlpha >= 250)
            {
                Debug.Log("0RMas");
                for (int j = 0; j < wallMeshRenderers.Length; j++)
                {
                    wallMeshRenderers[j].material.color = new Color(roofMat.color.r, roofMat.color.g, roofMat.color.b, 255);
                }
                time = 0;
                floorAlpha = 255;
                roof_Material_Alpha_0 = false;
            }
        }
        if (roof_Aaterial_Alpha_255)//메테리얼 알파값 0 만드는 조건
        {
            for (int i = 0; i < wallMeshRenderers.Length; i++)
            {
                wallMeshRenderers[i].material.color = new Color(roofMat.color.r, roofMat.color.g, roofMat.color.b, floorAlpha * Time.deltaTime);
                floorAlpha -= matAlpha;
            }
            if (floorAlpha <= 5)
            {
                Debug.Log("255RMas");
                for (int j = 0; j < wallMeshRenderers.Length; j++)
                {
                    wallMeshRenderers[j].material.color = new Color(roofMat.color.r, roofMat.color.g, roofMat.color.b, 0);
                }
                time = 0;
                floorAlpha = 0;
                roof_Aaterial_Alpha_255 = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        Debug.Log(other.gameObject.name);

        /*if(other.gameObject.tag == "Floor")
        {
            Debug.Log("플로어 입장1");
                wall[0] = other.gameObject;
                wallMat = wall[0].transform.GetChild(0).GetComponent<MeshRenderer>().material;

                wallMeshRenderers = wall[0].GetComponentsInChildren<MeshRenderer>();

                for (int i = 0; i < wallMeshRenderers.Length; i++)
                {
                    wallMeshRenderers[i].material.color = new Color(roofMat.color.r, roofMat.color.g, roofMat.color.b, 125);
                }
                if (wallMat != null)
                {
                    switch (wallMat.name)
                    {
                        case "PolygonTown_01_C (Instance)": matAlpha = 4.5f; break;
                        case "PolygonTown_01_A (Instance)": matAlpha = 3; break;

                        default:
                            break;
                    }

                }
                wall[0].name = "Floor";
                exitFloor = true;
                material_Alpha_0_Wall = true;
                material_Alpha_255_Wall = false;
                //material_Alpha_255 = true;
        }*/

        if (other.gameObject.tag == "Ceiling")
        {
            Debug.Log("a");
            if (exit == false)
            {
                roofObject = other.gameObject.transform.GetChild(0).gameObject; //Floor의 자식객체(roof)
                roofMat = roofObject.transform.GetChild(0).GetComponent<MeshRenderer>().material;

                roofMeshRenderers = roofObject.GetComponentsInChildren<MeshRenderer>();
                for (int i = 0; i < roofMeshRenderers.Length; i++)
                {
                    roofMeshRenderers[i].material.color = new Color(roofMat.color.r, roofMat.color.g, roofMat.color.b,125);
                }
                Debug.Log(roofMat.name);
                if (roofMat != null)
                {
                    switch (roofMat.name)
                    {
                        case "PolygonTown_01_C (Instance)": matAlpha = 4.5f; break;
                        case "PolygonTown_01_A (Instance)":matAlpha = 3; break;

                        default:
                            break;
                    }

                }
                roof_Aaterial_Alpha_255 = true;
                roof_Material_Alpha_0 = false;
                exit = true;

            }
            else
            {
                
                roofObject = other.gameObject.transform.GetChild(0).gameObject; //Floor의 자식객체
                roofMat = roofObject.transform.GetChild(0).GetComponent<MeshRenderer>().material;

                roofMeshRenderers = roofObject.GetComponentsInChildren<MeshRenderer>();
                for (int i = 0; i < roofMeshRenderers.Length; i++)
                {
                    roofMeshRenderers[i].material.color = new Color(roofMat.color.r, roofMat.color.g, roofMat.color.b, 0);
                }
                if (roofMat != null)
                {
                    switch (roofMat.name)
                    {
                        case "PolygonTown_01_C (Instance)": matAlpha = 2; break;
                        case "PolygonTown_01_A (Instance)": matAlpha = 0.8f; break;

                        default:
                            break;
                    }

                }
                roof_Material_Alpha_0 = true;
                roof_Aaterial_Alpha_255 = false;
                exit = false;
            }
            
        }

    }

   /* private void OnTriggerExit(Collider other)
    {
       if(other.gameObject.tag == "Floor")
        {
            for (int i = 0; i < wall.Count; i++)
            {
                if(other.gameObject.name == "Floor")
                {

                    wall[0] = other.gameObject;
                    wallMat = wall[0].transform.GetChild(0).GetComponent<MeshRenderer>().material;

                    wallMeshRenderers = wall[0].GetComponentsInChildren<MeshRenderer>();
                    for (int j = 0; j < wallMeshRenderers.Length; j++)
                    {
                        wallMeshRenderers[j].material.color = new Color(roofMat.color.r, roofMat.color.g, roofMat.color.b, 0);
                    }
                    if (wallMat != null)
                    {
                        switch (wallMat.name)
                        {
                            case "PolygonTown_01_C (Instance)": matAlpha = 2; break;
                            case "PolygonTown_01_A (Instance)": matAlpha = 0.8f; break;

                            default:
                                break;
                        }

                    }
                    floorName--;
                    exitFloor = true;
                    material_Alpha_255_Wall = true;
                    material_Alpha_0_Wall = false;
                    //material_Alpha_255 = true;
                }
            }
        }
    }*/
}