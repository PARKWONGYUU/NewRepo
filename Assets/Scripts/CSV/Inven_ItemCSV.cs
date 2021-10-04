using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inven_ItemCSV : MonoBehaviour
{
    public List <string> dataFrePab;
    public List<Dictionary<string, object>> data;

    public List<string> item_Name;
    public List<float> item_Weight;
    public List<int> item_unique_Num1;
    public List<int> item_unique_Num2;

    private void Awake()
    {
        // data = CSVReader.Read(dataFrePab[0]);
        AN_Item_Kind();
    }

    // 모든 아이템 데이터 저장
    public void AN_Item_Kind()
	{
		for (int i = 0; i < dataFrePab.Count; i++)
		{
			data = CSVReader.Read(dataFrePab[i]);

			for (int j = 0; j < data.Count; j++)
			{
                item_Name.Add (data[j]["ItemName"].ToString());
				item_Weight.Add(float.Parse(data[j]["Weight"].ToString()));
				item_unique_Num1.Add(int.Parse(data[j]["MainItemNum"].ToString()));
				item_unique_Num2.Add(int.Parse(data[j]["ServeItemNum"].ToString()));
			}
		}
	}
}
