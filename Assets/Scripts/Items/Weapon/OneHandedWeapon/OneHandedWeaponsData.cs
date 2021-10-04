using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHandedWeaponsData : MonoBehaviour
{
    public List<Dictionary<string, object>> oneHandedWeaponData;
    public string dataPath;

    public List<GameObject> oneHandedWeapons;
    void Start()
    {
        oneHandedWeaponData = CSVReader.Read(dataPath);
        for (int i = 0; i < oneHandedWeaponData.Count; i++)
        {
            oneHandedWeapons[i].GetComponent<OneHandedWeapons>().attackPower = int.Parse(oneHandedWeaponData[i]["AttackPower"].ToString());
            oneHandedWeapons[i].GetComponent<OneHandedWeapons>().attackRange = float.Parse(oneHandedWeaponData[i]["AttackRange"].ToString());
            oneHandedWeapons[i].GetComponent<OneHandedWeapons>().attackSpeed = float.Parse(oneHandedWeaponData[i]["AttackSpeed"].ToString());
            oneHandedWeapons[i].GetComponent<OneHandedWeapons>().weight = float.Parse(oneHandedWeaponData[i]["Weight"].ToString());
            oneHandedWeapons[i].GetComponent<OneHandedWeapons>().durability = float.Parse(oneHandedWeaponData[i]["Durability"].ToString());
            oneHandedWeapons[i].GetComponent<OneHandedWeapons>().endurance = float.Parse(oneHandedWeaponData[i]["Endurance"].ToString());
            oneHandedWeapons[i].GetComponent<OneHandedWeapons>().agility = float.Parse(oneHandedWeaponData[i]["Agility"].ToString());
            oneHandedWeapons[i].GetComponent<OneHandedWeapons>().mainItemNumm = int.Parse(oneHandedWeaponData[i]["MainItemNum"].ToString());
            oneHandedWeapons[i].GetComponent<OneHandedWeapons>().subItemNum = int.Parse(oneHandedWeaponData[i]["ServeItemNum"].ToString());
            /*Debug.Log(onHandedWeapons[i].GetComponent<OneHandedWeapons>().attackPower);
            Debug.Log(onHandedWeapons[i].GetComponent<OneHandedWeapons>().attackRange);
            Debug.Log(onHandedWeapons[i].GetComponent<OneHandedWeapons>().attackSpeed);
            Debug.Log(onHandedWeapons[i].GetComponent<OneHandedWeapons>().weight);
            Debug.Log(onHandedWeapons[i].GetComponent<OneHandedWeapons>().durability);
            Debug.Log(onHandedWeapons[i].GetComponent<OneHandedWeapons>().endurance);
            Debug.Log(onHandedWeapons[i].GetComponent<OneHandedWeapons>().agility);*/
        }
    }
}
