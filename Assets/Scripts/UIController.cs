using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text pctText;
    public TMP_Text phoneticText;
    public TMP_Text timerText;
    public TMP_InputField areaInput;
    public Toggle phoneticCheckbox;
    public Button restartButton;
    public Button menuButton;
    private int timerSecs;
    private bool countdownOn;

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
        phoneticCheckbox.onValueChanged.AddListener(delegate{ChangePhoneticDisplay(phoneticCheckbox.isOn);});
        phoneticCheckbox.isOn = false;
        ChangePhoneticDisplay(phoneticCheckbox.isOn);
        restartButton.onClick.AddListener(delegate{SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);});
        menuButton.onClick.AddListener(delegate{SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);});
        timerSecs = 300;
        countdownOn = true;
        CountDown();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        else
        {
            countdownOn = false;
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
        ChangePhoneticDisplay(phoneticCheckbox.isOn);
    }

    void ChangePhoneticDisplay(bool isChecked)
    {
        phoneticText.text = isChecked ? selectedArea.phoneticName : "";
    }

    void CountDown()
    {
        if(timerSecs > 1 && countdownOn)
        {
            timerSecs--;
            timerText.text = $"{(int)(timerSecs / 60)}:{(timerSecs % 60).ToString("D2")}";
            Invoke("CountDown", 1f);
        }
    }
}
