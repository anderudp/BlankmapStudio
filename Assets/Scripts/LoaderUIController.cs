using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoaderUIController : MonoBehaviour
{
    public TMP_Dropdown folderDropdown;
    public TMP_Dropdown jsonDropdown;
    public TMP_Dropdown gamemodeDropdown;
    public Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        var folders = Directory.GetDirectories("Assets/Resources").ToList();
        folderDropdown.options = new List<TMP_Dropdown.OptionData>();
        folderDropdown.AddOptions(folders);
        folderDropdown.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
