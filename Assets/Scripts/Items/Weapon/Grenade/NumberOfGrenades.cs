using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberOfGrenades : MonoBehaviour
{
    [Header("Number Of Grenades")] // ����ź �� 
    public int curMolotov; //ȭ����
    public int grenade; // ����ź
    public int carrion; // ���� ���
    public Transform GrenPos; // ����ź ������ġ
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void Taeho_Molotov()
    {
        curMolotov++;
    }

}
