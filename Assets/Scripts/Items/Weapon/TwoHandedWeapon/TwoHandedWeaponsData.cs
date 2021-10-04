using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHandedWeaponsData : MonoBehaviour
{
    public List<Dictionary<string, object>> twoHandedWeaponData;
    public string dataPath;

    public List<GameObject> twoHandedWeapons;
    void Start()
    {
        //dataPath = "TwoHandeWeapon";
        twoHandedWeaponData = CSVReader.Read(dataPath);
        for (int i = 0; i < twoHandedWeaponData.Count; i++)
        {
            twoHandedWeapons[i].GetComponent<TwoHandedWeapons>().attackPower = int.Parse(twoHandedWeaponData[i]["AttackPower"].ToString());
            twoHandedWeapons[i].GetComponent<TwoHandedWeapons>().attackRange = float.Parse(twoHandedWeaponData[i]["AttackRange"].ToString());
            twoHandedWeapons[i].GetComponent<TwoHandedWeapons>().attackSpeed = float.Parse(twoHandedWeaponData[i]["AttackSpeed"].ToString());
            twoHandedWeapons[i].GetComponent<TwoHandedWeapons>().weight = float.Parse(twoHandedWeaponData[i]["Weight"].ToString());
            twoHandedWeapons[i].GetComponent<TwoHandedWeapons>().durability = float.Parse(twoHandedWeaponData[i]["Durability"].ToString());
            twoHandedWeapons[i].GetComponent<TwoHandedWeapons>().endurance = float.Parse(twoHandedWeaponData[i]["Endurance"].ToString());
            twoHandedWeapons[i].GetComponent<TwoHandedWeapons>().agility = float.Parse(twoHandedWeaponData[i]["Agility"].ToString());
            twoHandedWeapons[i].GetComponent<TwoHandedWeapons>().mainItemNum = int.Parse(twoHandedWeaponData[i]["MainItemNum"].ToString());
            twoHandedWeapons[i].GetComponent<TwoHandedWeapons>().subItemNum = int.Parse(twoHandedWeaponData[i]["ServeItemNum"].ToString());
            /*  Debug.Log(twoHandedWeapons[i].GetComponent<TwoHandedWeapons>().attackPower);
              Debug.Log(twoHandedWeapons[i].GetComponent<TwoHandedWeapons>().attackRange);
              Debug.Log(twoHandedWeapons[i].GetComponent<TwoHandedWeapons>().attackSpeed);
              Debug.Log(twoHandedWeapons[i].GetComponent<TwoHandedWeapons>().weight);
              Debug.Log(twoHandedWeapons[i].GetComponent<TwoHandedWeapons>().durability);
              Debug.Log(twoHandedWeapons[i].GetComponent<TwoHandedWeapons>().endurance);
              Debug.Log(twoHandedWeapons[i].GetComponent<TwoHandedWeapons>().agility);*/
        }
    }
}
