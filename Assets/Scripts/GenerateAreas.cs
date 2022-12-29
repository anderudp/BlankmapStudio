using System.Collections;
using System.Collections.Generic;
using DigitalRuby.AdvancedPolygonCollider;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class GenerateAreas : MonoBehaviour
{
    public string jsonName;

    AreaData[] areas;

    // Preallocate space for individual region object components
    GameObject areaObject ;
    AreaScript areaScript;
    SpriteRenderer areaRenderer;
    AdvancedPolygonCollider areaCollider;

    void Start()
    {
        string jsonContents = Resources.Load<TextAsset>(jsonName).ToString();
        areas = JsonConvert.DeserializeObject<AreaData[]>(jsonContents);

        foreach (AreaData areaData in areas)
        {
            areaObject = new GameObject(areaData.RomajiName);
            areaObject.transform.parent = this.transform;

            areaScript = areaObject.AddComponent<AreaScript>();
            areaScript.ordinate = areaData.Ordinate;
            areaScript.kanjiName = areaData.KanjiName;
            areaScript.furiganaName = areaData.FuriganaName;

            areaRenderer = areaObject.AddComponent<SpriteRenderer>();
            areaRenderer.sprite = Resources.Load<Sprite>(areaData.SpriteLocation);
            areaRenderer.sortingOrder = 1;

            areaCollider = areaObject.AddComponent<AdvancedPolygonCollider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
