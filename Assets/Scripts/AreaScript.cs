using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScript : MonoBehaviour
{
    public event EventHandler AreaClicked;
    public int ordinate;
    public string nativeName;
    public string phoneticName;

    private bool hasBeenClicked = false;

    private SpriteRenderer sr;

    public static Color32 defaultColor = new Color32(239, 239, 122, 255);
    public static Color32 hoverColor = new Color32(239, 179, 122, 255);
    public static Color32 correctColor = new Color32(122, 239, 122, 255);
    public static Color32 missedColor = new Color32(239, 122, 122, 255);

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = defaultColor;
    }

    private void OnMouseEnter() 
    {
        if(!hasBeenClicked) sr.color = hoverColor;
    }

    private void OnMouseExit() 
    {
        if(!hasBeenClicked) sr.color = defaultColor;
    }

    private void OnMouseUpAsButton() 
    {
        hasBeenClicked = true;
        sr.color = UIController.selectedArea == this ? correctColor : missedColor;
        AreaClicked?.Invoke(this, EventArgs.Empty);
    }
}
