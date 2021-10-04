using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadesData : MonoBehaviour
{
    public List<Dictionary<string, object>> grenadeData;
    public string dataPath;

    public List<GameObject> grenades;
    void Start()
    {
        grenadeData = CSVReader.Read(dataPath);
        for (int i = 0; i < grenadeData.Count; i++)
        {
            grenades[i].GetComponent<Grenades>().attackPower = int.Parse(grenadeData[i]["AttackPower"].ToString());
            grenades[i].GetComponent<Grenades>().attackRange = float.Parse(grenadeData[i]["AttackRange"].ToString());
            grenades[i].GetComponent<Grenades>().attackSpeed = float.Parse(grenadeData[i]["AttackSpeed"].ToString());
            grenades[i].GetComponent<Grenades>().weight = float.Parse(grenadeData[i]["Weight"].ToString());
            grenades[i].GetComponent<Grenades>().mainItemNum = int.Parse(grenadeData[i]["MainItemNum"].ToString());
            grenades[i].GetComponent<Grenades>().subItemNum = int.Parse(grenadeData[i]["ServeItemNum"].ToString());
            /*Debug.Log(grenades[i].GetComponent<Grenades>().attackPower);
            Debug.Log(grenades[i].GetComponent<Grenades>().attackRange);
            Debug.Log(grenades[i].GetComponent<Grenades>().attackSpeed);
            Debug.Log(grenades[i].GetComponent<Grenades>().weight);*/
        }
    }
}
