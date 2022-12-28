using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AreaData
{
    public int Ordinate {get; set;}
    public string RomajiName {get; set;}
    public string KanjiName {get; set;}
    public string FuriganaName {get; set;}
    public string SpriteLocation {get; set;}

    public AreaData(int ordinate, string romajiname, string kanjiname, string furigananame, string spritelocation)
    {
        Ordinate = ordinate;
        RomajiName = romajiname;
        KanjiName = kanjiname;
        FuriganaName = furigananame;
        SpriteLocation = spritelocation;
    }
}
