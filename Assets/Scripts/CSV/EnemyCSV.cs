using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCSV : MonoBehaviour
{
    public string dataFrePab;
    public List<Dictionary<string, object>> data;

    public string[] enemy_Name;
    public int[] enemy_No;
    public int[] enemyHP;
    public int[] range;
    public float[] moveSpeed;
    public float[] walkSpeed;
    public float[] stopSpeed;
    public float[] enemy_Strength;
    public float[] attack_Percentage;

    private void Awake()
    {
        data = CSVReader.Read(dataFrePab);
    }
    void Start()
    {
        CSV_Data();
    }
    public void CSV_Data()
    {
        for (int i = 0; i < data.Count; i++)
        {
            enemy_No[i] = int.Parse(data[i]["No"].ToString());
            enemy_Name[i] = data[i]["EnemyName"].ToString();
            enemyHP[i] = int.Parse(data[i]["EnemyHP"].ToString());
            enemy_Strength[i] = float.Parse(data[i]["Strength"].ToString());
            moveSpeed[i] = float.Parse(data[i]["MoveSpeed"].ToString());
			range[i] = int.Parse(data[i]["range"].ToString());
            attack_Percentage[i] = float.Parse(data[i]["Attack Percentage"].ToString());
            walkSpeed[i] = float.Parse(data[i]["Walk"].ToString());
            stopSpeed[i] = float.Parse(data[i]["Stop"].ToString());
        }
    }
}
