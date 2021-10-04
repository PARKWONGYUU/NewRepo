using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodsData : MonoBehaviour
{
    public List<Dictionary<string, object>> FoodData;
    public string dataPath;

    public List<GameObject> Foods;
    void Start()
    {
        FoodData = CSVReader.Read(dataPath);
        for (int i = 0; i < FoodData.Count; i++)
        {
            Foods[i].GetComponent<Foods>().shelfLife = int.Parse(FoodData[i]["ShelfLife"].ToString());
            Foods[i].GetComponent<Foods>().thirst = int.Parse(FoodData[i]["Thirst"].ToString());
            Foods[i].GetComponent<Foods>().hungry = int.Parse(FoodData[i]["Hungry"].ToString());
            Foods[i].GetComponent<Foods>().weight = int.Parse(FoodData[i]["Weight"].ToString());
            Foods[i].GetComponent<Foods>().state = int.Parse(FoodData[i]["State"].ToString());
            Foods[i].GetComponent<Foods>().infection = int.Parse(FoodData[i]["Infection"].ToString());
            Foods[i].GetComponent<Foods>().foodsName = FoodData[i]["ItemName"].ToString();
            Foods[i].GetComponent<Foods>().mainItemNum = int.Parse(FoodData[i]["MainItemNum"].ToString());
            Foods[i].GetComponent<Foods>().subItemNum = int.Parse(FoodData[i]["ServeItemNum"].ToString());


            /*Debug.Log(Foods[i].GetComponent<Foods>().shelfLife);
            Debug.Log(Foods[i].GetComponent<Foods>().thirst);
            Debug.Log(Foods[i].GetComponent<Foods>().hungry);
            Debug.Log(Foods[i].GetComponent<Foods>().weight);
            Debug.Log(Foods[i].GetComponent<Foods>().state);
            Debug.Log(Foods[i].GetComponent<Foods>().infection);
            Debug.Log(Foods[i].GetComponent<Foods>().foodsName);*/
        }
    }
}
