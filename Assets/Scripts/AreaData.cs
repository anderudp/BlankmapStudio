using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AreaData
{
    public int Ordinate {get; set;}
    public string LatinName {get; set;}
    public string NativeName {get; set;}
    public string PhoneticName {get; set;}
    public string SpriteLocation {get; set;}

    public AreaData(int ordinate, string latinname, string nativename, string phoneticname, string spritelocation)
    {
        Ordinate = ordinate;
        LatinName = latinname;
        NativeName = nativename;
        PhoneticName = phoneticname;
        SpriteLocation = spritelocation;
    }
}
