using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text pctText;
    public TMP_Text phoneticText;
    public TMP_InputField areaInput;
    public Toggle phoneticCheckbox;

    public static AreaScript selectedArea;
    private int maxAreas;
    public static List<AreaScript> correctAreas;
    public static List<AreaScript> missedAreas;

    void Start()
    {
        maxAreas = transform.childCount;
        areaInput.readOnly = true;
        correctAreas = new List<AreaScript>();
        missedAreas = new List<AreaScript>();
        scoreText.text = $"0/{maxAreas}";
        pctText.text = "0%";
        ChangeSelectedArea();
        foreach(Transform area in gameObject.transform)
        {
            area.GetComponent<AreaScript>().AreaClicked += OnAreaClicked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        phoneticText.text = phoneticCheckbox.isOn ? selectedArea.phoneticName : " ";
    }

    void OnAreaClicked(object sender, EventArgs e)
    {
        AreaScript clickedArea = sender as AreaScript;
        bool correctGuess = clickedArea == selectedArea;

        if(correctGuess) correctAreas.Add(clickedArea);
        else missedAreas.Add(clickedArea);

        scoreText.text = $"{correctAreas.Count}/{maxAreas}";
        pctText.text = $"{correctAreas.Count * 100 / maxAreas}%";
        if(missedAreas.Count + correctAreas.Count < maxAreas)
        {
            if(correctGuess) ChangeSelectedArea();
        }
    }

    void ChangeSelectedArea()
    {
        do
        {
            selectedArea = transform.GetChild(UnityEngine.Random.Range(0, transform.childCount)).GetComponent<AreaScript>();
        }
        while(correctAreas.Contains(selectedArea) || missedAreas.Contains(selectedArea));

        areaInput.text = selectedArea.nativeName;
    }
}
