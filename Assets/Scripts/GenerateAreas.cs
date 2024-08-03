using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

public class GenerateAreas : MonoBehaviour
{
    AreaData[] areas;

    // Preallocate space for individual region object components
    GameObject areaObject;
    AreaScript areaScript;
    SpriteRenderer areaRenderer;

    GameObject background;
    SpriteRenderer bgRenderer;

    float UIHeightBuffer = 6.5f;
    void Start()
    {
        string jsonContents = Resources.Load<TextAsset>($"{GameParams.FolderName}/{GameParams.JsonName}").ToString();
        areas = JsonConvert.DeserializeObject<AreaData[]>(jsonContents);

        background = new GameObject("Background");
        bgRenderer = background.AddComponent<SpriteRenderer>();
        bgRenderer.sprite = Resources.Load<Sprite>($"{GameParams.FolderName}/Background");

        float heightScaleFactor = (Camera.main.orthographicSize * 2f - UIHeightBuffer) / bgRenderer.sprite.bounds.size.y;
        bgRenderer.transform.localScale = new Vector3(heightScaleFactor, heightScaleFactor, 1);
        
        // float positionFactor = -Camera.main.orthographicSize * Camera.main.aspect + bgRenderer.sprite.bounds.size.x * heightScaleFactor / 2f;
        background.transform.position = new Vector3(0f, -UIHeightBuffer / 2f, 0f);

        foreach (AreaData areaData in areas)
        {
            areaObject = new GameObject(areaData.LatinName);
            areaObject.transform.parent = transform;
            areaObject.transform.position = background.transform.position;
            areaObject.transform.localScale = background.transform.localScale;

            areaScript = areaObject.AddComponent<AreaScript>();
            areaScript.ordinate = areaData.Ordinate;
            areaScript.nativeName = areaData.NativeName;
            areaScript.phoneticName = areaData.PhoneticName;

            areaRenderer = areaObject.AddComponent<SpriteRenderer>();
            areaRenderer.sprite = Resources.Load<Sprite>(areaData.SpriteLocation);
            areaRenderer.sortingOrder = 1;
        }
    }
}
