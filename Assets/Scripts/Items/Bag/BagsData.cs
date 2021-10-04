using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagsData : MonoBehaviour
{
    //CSV
    public List<Dictionary<string, object>> bagCSV;
    public string dataPath;

    public List<GameObject> bags;
    void Start()
    {
        bagCSV = CSVReader.Read(dataPath);
        for (int i = 0; i < bagCSV.Count; i++)
        {
            bags[i].GetComponent<Bags>().maxWeight = float.Parse(bagCSV[i]["MaxWeight"].ToString());
            bags[i].GetComponent<Bags>().maxItem = int.Parse(bagCSV[i]["MaxItem"].ToString());
           /* Debug.Log(bags[i].GetComponent<Bags>().maxWeight);
            Debug.Log(bags[i].GetComponent<Bags>().maxItem);*/
        }
    }
}
