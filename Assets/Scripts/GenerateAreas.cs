using System.Collections;
using System.Collections.Generic;
using DigitalRuby.AdvancedPolygonCollider;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class GenerateAreas : MonoBehaviour
{
    AreaData[] areas;

    // Preallocate space for individual region object components
    GameObject areaObject;
    AreaScript areaScript;
    SpriteRenderer areaRenderer;
    AdvancedPolygonCollider areaCollider;

    GameObject background;
    SpriteRenderer bgRenderer;

    Vector3 mapPosition;
    void Start()
    {
        string jsonContents = Resources.Load<TextAsset>($"{GameParams.FolderName}/{GameParams.JsonName}").ToString();
        areas = JsonConvert.DeserializeObject<AreaData[]>(jsonContents);

        background = new GameObject("Background");
        bgRenderer = background.AddComponent<SpriteRenderer>();
        bgRenderer.sprite = Resources.Load<Sprite>($"{GameParams.FolderName}/Background");

        mapPosition = bgRenderer.sprite.bounds.max - CameraScript.absoluteMax;
        background.transform.position = mapPosition;

        foreach (AreaData areaData in areas)
        {
            areaObject = new GameObject(areaData.LatinName);
            areaObject.transform.parent = this.transform;
            areaObject.transform.position = mapPosition;

            areaScript = areaObject.AddComponent<AreaScript>();
            areaScript.ordinate = areaData.Ordinate;
            areaScript.nativeName = areaData.NativeName;
            areaScript.phoneticName = areaData.PhoneticName;

            areaRenderer = areaObject.AddComponent<SpriteRenderer>();
            areaRenderer.sprite = Resources.Load<Sprite>(areaData.SpriteLocation);
            areaRenderer.sortingOrder = 1;

            areaCollider = areaObject.AddComponent<AdvancedPolygonCollider>();
        }
    }
}
