using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinksData : MonoBehaviour
{

    public List<Dictionary<string, object>> drinksData;
    public string dataPath;

    public List<GameObject> drinks;
    void Start()
    {
        drinksData = CSVReader.Read(dataPath);
        for (int i = 0; i < drinksData.Count; i++)
        {
            drinks[i].GetComponent<Drinks>().thirst = float.Parse(drinksData[i]["Thirst"].ToString());
            drinks[i].GetComponent<Drinks>().hungry = float.Parse(drinksData[i]["Hungry"].ToString());
            drinks[i].GetComponent<Drinks>().weight = float.Parse(drinksData[i]["Weight"].ToString());
            drinks[i].GetComponent<Drinks>().state = int.Parse(drinksData[i]["State"].ToString());
            drinks[i].GetComponent<Drinks>().tired = float.Parse(drinksData[i]["Tired"].ToString());
            drinks[i].GetComponent<Drinks>().infection = int.Parse(drinksData[i]["Infection"].ToString());
            drinks[i].GetComponent<Drinks>().mainItemNum = int.Parse(drinksData[i]["MainItemNum"].ToString());
            drinks[i].GetComponent<Drinks>().subItemNum = int.Parse(drinksData[i]["ServeItemNum"].ToString());
            /*Instantiate(dirnks[i]);
             Debug.Log(dirnks[i].GetComponent<Drinks>().thirst);
             Debug.Log(dirnks[i].GetComponent<Drinks>().hungry);
             Debug.Log(dirnks[i].GetComponent<Drinks>().weight);
             Debug.Log(dirnks[i].GetComponent<Drinks>().state);
             Debug.Log(dirnks[i].GetComponent<Drinks>().infection);
             Debug.Log(dirnks[i].GetComponent<Drinks>().tired);*/
        }
    }
}
