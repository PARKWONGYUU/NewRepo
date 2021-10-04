using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmsData : MonoBehaviour
{
   
    public List<Dictionary<string, object>> firearmData;
    public string dataPath;

    public List<GameObject> firearms;
    void Start()
    {
        firearmData = CSVReader.Read(dataPath);
        for (int i = 0; i < firearmData.Count; i++)
        {
            firearms[i].GetComponent<Firearms>().attackPower = int.Parse(firearmData[i]["AttackPower"].ToString());
            firearms[i].GetComponent<Firearms>().attackRange = float.Parse(firearmData[i]["AttackRange"].ToString());
            firearms[i].GetComponent<Firearms>().attackSpeed = float.Parse(firearmData[i]["AttackSpeed"].ToString());
            firearms[i].GetComponent<Firearms>().weight = float.Parse(firearmData[i]["Weight"].ToString());
            firearms[i].GetComponent<Firearms>().durability = float.Parse(firearmData[i]["Durability"].ToString());
            firearms[i].GetComponent<Firearms>().endurance = float.Parse(firearmData[i]["Endurance"].ToString());
            firearms[i].GetComponent<Firearms>().agility = float.Parse(firearmData[i]["Agility"].ToString());
            firearms[i].GetComponent<Firearms>().ReloadSpeed = float.Parse(firearmData[i]["ReloadSpeed"].ToString());
            firearms[i].GetComponent<Firearms>().AimingSpeed = float.Parse(firearmData[i]["AimingSpeed"].ToString());
            firearms[i].GetComponent<Firearms>().accuracy = int.Parse(firearmData[i]["Accuracy"].ToString());
            firearms[i].GetComponent<Firearms>().maxloadbullet = int.Parse(firearmData[i]["MaxLoadBullet"].ToString());
            firearms[i].GetComponent<Firearms>().mainItemNum = int.Parse(firearmData[i]["MainItemNum"].ToString());
            firearms[i].GetComponent<Firearms>().subItemNum = int.Parse(firearmData[i]["ServeItemNum"].ToString());
            /* Debug.Log(firearms[i].GetComponent<Firearms>().attackPower);
             Debug.Log(firearms[i].GetComponent<Firearms>().attackRange);
             Debug.Log(firearms[i].GetComponent<Firearms>().attackSpeed);
             Debug.Log(firearms[i].GetComponent<Firearms>().weight);
             Debug.Log(firearms[i].GetComponent<Firearms>().durability);
             Debug.Log(firearms[i].GetComponent<Firearms>().endurance);
             Debug.Log(firearms[i].GetComponent<Firearms>().agility);
           Debug.Log(firearms[i].GetComponent<Firearms>().ReloadSpeed);
           Debug.Log(firearms[i].GetComponent<Firearms>().AimingSpeed);
           Debug.Log(firearms[i].GetComponent<Firearms>().accuracy);*/
        }
    }

   
}
