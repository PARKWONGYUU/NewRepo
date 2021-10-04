using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArmorsData : MonoBehaviour
{
    [Header("ChangeAmors")] // 아머갯수 조절 가능 -> 대신에 게임오브젝트도 같이 조정해야함
    public int capNum = 5;
    public int shoesNum = 5;
    public int shirtsNum = 5;
    public int pantsNum = 5;


    public bool capCSV = true;
    public bool shoesCSV = false;
    public bool shirtsCSV = false;
    public bool pantsCSV = false;

    //Prefabs

    //1 : ㅁㅁ, 2 : ㅁㅁㅁ, 3 : ㅁㅁㅁㅁ, 4 : ㅁㅁㅁㅁㅁ, 5 : ㅁㅁㅁㅁㅁ -> 모자 종류
    public List<GameObject> caps;

    //1 : ㅁㅁ, 2 : ㅁㅁㅁ, 3 : ㅁㅁㅁㅁ, 4 : ㅁㅁㅁㅁㅁ, 5 : ㅁㅁㅁㅁㅁ -> 신발 종류
    public List<GameObject> shoes;

    //1 : ㅁㅁ, 2 : ㅁㅁㅁ, 3 : ㅁㅁㅁㅁ, 4 : ㅁㅁㅁㅁㅁ, 5 : ㅁㅁㅁㅁㅁ -> 셔츠 종류
    public List<GameObject> shirts;

    //1 : ㅁㅁ, 2 : ㅁㅁㅁ, 3 : ㅁㅁㅁㅁ, 4 : ㅁㅁㅁㅁㅁ, 5 : ㅁㅁㅁㅁㅁ -> 바지 종류
    public List<GameObject> pants;

    //CSV
    public List<Dictionary<string, object>> armorCSV;
    public string dataPath;


    void Start()
    {
        armorCSV = CSVReader.Read(dataPath);

        //CSV 작동 원리 
        /*Debug.Log(armorCSV[0]["ArmorHp"]);
        Debug.Log(armorCSV[0]["Weight"]);
        Debug.Log(armorCSV[0]["Durability"]);
        Debug.Log(armorCSV[0]["Agility"]);
        Debug.Log(armorCSV[0]["Endurance"]);
        Debug.Log(armorCSV[0]["PreventZombie"]);*/

        //caps[0].GetComponent<Amors>

        /* for (int i = 0; i < armorCSV.Count; i++)
         {
             //첫번째 12 4 67 3 5 10
             Debug.Log(armorCSV[i]["InfectionRate"]);
             Debug.Log(armorCSV[i]["ArmorHp"]);
             Debug.Log(armorCSV[i]["Weight"]);
             Debug.Log(armorCSV[i]["Durability"]);
             Debug.Log(armorCSV[i]["Agility"]);
             Debug.Log(armorCSV[i]["Endurance"]);
             Debug.Log(armorCSV[i]["PreventZombie"]);
         }*/

        //Debug.Log(armorCSV.Count);
        //Debug.Log(shoesNum + capNum);
        //Debug.Log(shoesNum + capNum + shirtsNum);


        if(capCSV)
        {
            StartCoroutine(TaehoCap());
            capCSV = false;
            shoesCSV = true;
        }
        

        if(shoesCSV)
        {
            StartCoroutine(TaehoShoes());
            shoesCSV = false;
            shirtsCSV = true;
        }
        if(shirtsCSV)
        {
            StartCoroutine(TaehoShirts());
            shirtsCSV = false;
            pantsCSV = true;
        }
        if(pantsCSV)
        {
            StartCoroutine(TaehoPants());
            pantsCSV = false;
        }
    }
    
    IEnumerator TaehoCap()
    {
        int armorCSV_CapNum = armorCSV.Count - (pantsNum + shirtsNum + shoesNum);
        for (int i = 0; i < armorCSV_CapNum; i++) //20- 5+ 5+ 5 
        {
            caps[i].GetComponent<Armors>().infectionRate = float.Parse(armorCSV[i]["InfectionRate"].ToString());
            caps[i].GetComponent<Armors>().armorHp = int.Parse(armorCSV[i]["ArmorHp"].ToString());
            caps[i].GetComponent<Armors>().weight = float.Parse(armorCSV[i]["Weight"].ToString());
            caps[i].GetComponent<Armors>().durability = float.Parse(armorCSV[i]["Durability"].ToString());
            caps[i].GetComponent<Armors>().agility = float.Parse(armorCSV[i]["Agility"].ToString());
            caps[i].GetComponent<Armors>().endurance = float.Parse(armorCSV[i]["Endurance"].ToString());
            caps[i].GetComponent<Armors>().preventZombie = float.Parse(armorCSV[i]["PreventZombie"].ToString());
            caps[i].GetComponent<Armors>().mainItemNum = int.Parse(armorCSV[i]["MainItemNum"].ToString());
            caps[i].GetComponent<Armors>().subItemNum = int.Parse(armorCSV[i]["ServeItemNum"].ToString());
            /*  Debug.Log(caps[i].GetComponent<Armors>().infectionRate);
              Debug.Log(caps[i].GetComponent<Armors>().armorHp);
              Debug.Log(caps[i].GetComponent<Armors>().weight);
              Debug.Log(caps[i].GetComponent<Armors>().durability);
              Debug.Log(caps[i].GetComponent<Armors>().agility);
              Debug.Log(caps[i].GetComponent<Armors>().endurance);
              Debug.Log(caps[i].GetComponent<Armors>().preventZombie);*/

        }

        yield return null;
    }
    IEnumerator TaehoShoes()
    {
        int armorCSV_ShoesNum = armorCSV.Count - (pantsNum + shirtsNum + capNum); //capNum
        int CSV = armorCSV.Count - (capNum + shirtsNum + pantsNum); // 20 - (5 + 5 + 5)  = 5;
        for (int i = 0; i < armorCSV_ShoesNum; i++)
        {
            shoes[i].GetComponent<Armors>().infectionRate = float.Parse(armorCSV[CSV]["InfectionRate"].ToString());
            shoes[i].GetComponent<Armors>().armorHp = int.Parse(armorCSV[CSV]["ArmorHp"].ToString());
            shoes[i].GetComponent<Armors>().weight = float.Parse(armorCSV[CSV]["Weight"].ToString());
            shoes[i].GetComponent<Armors>().durability = float.Parse(armorCSV[CSV]["Durability"].ToString());
            shoes[i].GetComponent<Armors>().agility = float.Parse(armorCSV[CSV]["Agility"].ToString());
            shoes[i].GetComponent<Armors>().endurance = float.Parse(armorCSV[CSV]["Endurance"].ToString());
            shoes[i].GetComponent<Armors>().mainItemNum = int.Parse(armorCSV[i]["MainItemNum"].ToString());
            shoes[i].GetComponent<Armors>().subItemNum = int.Parse(armorCSV[i + 5]["ServeItemNum"].ToString());
            /* Debug.Log(shoes[i].GetComponent<Armors>().infectionRate);
             Debug.Log(shoes[i].GetComponent<Armors>().armorHp);
             Debug.Log(shoes[i].GetComponent<Armors>().weight);
             Debug.Log(shoes[i].GetComponent<Armors>().durability);
             Debug.Log(shoes[i].GetComponent<Armors>().agility);
             Debug.Log(shoes[i].GetComponent<Armors>().endurance);
             Debug.Log(shoes[i].GetComponent<Armors>().preventZombie);*/
            CSV++;

        }
        //shoesCSV = true;
        yield return null;
        
    }
    IEnumerator TaehoShirts()
    {
        int armorCSV_ShirtsNum = armorCSV.Count - (pantsNum + capNum + shoesNum); //capNum+shoesNum
        int CSV = armorCSV.Count - (shirtsNum + pantsNum); // 20 - (5 + 5) = 10;
        for (int i = 0; i < armorCSV_ShirtsNum; i++)
        {
            shirts[i].GetComponent<Armors>().infectionRate = float.Parse(armorCSV[CSV]["InfectionRate"].ToString());
            shirts[i].GetComponent<Armors>().armorHp = int.Parse(armorCSV[CSV]["ArmorHp"].ToString());
            shirts[i].GetComponent<Armors>().weight = float.Parse(armorCSV[CSV]["Weight"].ToString());
            shirts[i].GetComponent<Armors>().durability = float.Parse(armorCSV[CSV]["Durability"].ToString());
            shirts[i].GetComponent<Armors>().agility = float.Parse(armorCSV[CSV]["Agility"].ToString());
            shirts[i].GetComponent<Armors>().endurance = float.Parse(armorCSV[CSV]["Endurance"].ToString());
            shirts[i].GetComponent<Armors>().preventZombie = float.Parse(armorCSV[CSV]["PreventZombie"].ToString());
            shirts[i].GetComponent<Armors>().mainItemNum = int.Parse(armorCSV[i]["MainItemNum"].ToString());
            shirts[i].GetComponent<Armors>().subItemNum = int.Parse(armorCSV[i + 10]["ServeItemNum"].ToString());
            /*  Debug.Log(shirts[i].GetComponent<Armors>().infectionRate);
              Debug.Log(shirts[i].GetComponent<Armors>().armorHp);
              Debug.Log(shirts[i].GetComponent<Armors>().weight);
              Debug.Log(shirts[i].GetComponent<Armors>().durability);
              Debug.Log(shirts[i].GetComponent<Armors>().agility);
              Debug.Log(shirts[i].GetComponent<Armors>().endurance);
              Debug.Log(shirts[i].GetComponent<Armors>().preventZombie);*/
            CSV++;
        }
        //shirtsCSV = true;
        yield return null;
    }

    IEnumerator TaehoPants()
    {
        int armorCSV_PantsNum = armorCSV.Count - (capNum + shoesNum + shirtsNum);
        int CSV = armorCSV.Count - pantsNum;
         for (int i = 0; i< armorCSV_PantsNum; i++) // capNum+shoesNum+shirtsNum
        {
            
            pants[i].GetComponent<Armors>().infectionRate = float.Parse(armorCSV[CSV]["InfectionRate"].ToString());
            pants[i].GetComponent<Armors>().armorHp = int.Parse(armorCSV[CSV]["ArmorHp"].ToString());
            pants[i].GetComponent<Armors>().weight = float.Parse(armorCSV[CSV]["Weight"].ToString());
            pants[i].GetComponent<Armors>().durability = float.Parse(armorCSV[CSV]["Durability"].ToString());
            pants[i].GetComponent<Armors>().agility = float.Parse(armorCSV[CSV]["Agility"].ToString());
            pants[i].GetComponent<Armors>().endurance = float.Parse(armorCSV[CSV]["Endurance"].ToString());
            pants[i].GetComponent<Armors>().preventZombie = float.Parse(armorCSV[CSV]["PreventZombie"].ToString());
            pants[i].GetComponent<Armors>().mainItemNum = int.Parse(armorCSV[i]["MainItemNum"].ToString());
            pants[i].GetComponent<Armors>().subItemNum = int.Parse(armorCSV[i + 15]["ServeItemNum"].ToString());
            /*   Debug.Log(pants[i].GetComponent<Armors>().infectionRate);
               Debug.Log(pants[i].GetComponent<Armors>().armorHp);
               Debug.Log(pants[i].GetComponent<Armors>().weight);
               Debug.Log(pants[i].GetComponent<Armors>().durability);
               Debug.Log(pants[i].GetComponent<Armors>().agility);
               Debug.Log(pants[i].GetComponent<Armors>().endurance);
               Debug.Log(pants[i].GetComponent<Armors>().preventZombie);*/
            CSV++;
         }
        yield return null;
    }
}






