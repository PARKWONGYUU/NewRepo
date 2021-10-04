using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberOfGrenades : MonoBehaviour
{
    [Header("Number Of Grenades")] // ¼ö·ùÅº ¼ö 
    public int curMolotov; //È­¿°º´
    public int grenade; // ¼ö·ùÅº
    public int carrion; // ½âÀº °í±â
    public Transform GrenPos; // ¼ö·ùÅº »ý¼ºÀ§Ä¡
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
