using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureCSV : MonoBehaviour
{
    public string dataFrePab;
    public List<Dictionary<string, object>> data;

    public List<string> furniture_Name;
    public List<float> furniture_Weight;
    public List<int> furniture_Num;
   
    void Start()
    {
        AN_Furniture_Kind();
    }
    public void AN_Furniture_Kind()
    {
        data = CSVReader.Read(dataFrePab);

        for (int i = 0; i < data.Count; i++)
        {
            furniture_Name.Add(data[i]["FurnitureName"].ToString());
            furniture_Weight.Add(float.Parse(data[i]["Weight"].ToString()));
            furniture_Num.Add(int.Parse(data[i]["No"].ToString()));
        }
    }
}
