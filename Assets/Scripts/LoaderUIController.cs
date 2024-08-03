using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        DisplayFolders();
        folderDropdown.value = 0;
        DisplayJsons(folderDropdown.options[folderDropdown.value].text);
        jsonDropdown.value = 0;
        folderDropdown.onValueChanged.AddListener(delegate {DisplayJsons(folderDropdown.options[folderDropdown.value].text);});
        gamemodeDropdown.enabled = false;
        startButton.onClick.AddListener(delegate {
            LoadGame(folderDropdown.options[folderDropdown.value].text, jsonDropdown.options[jsonDropdown.value].text);
        });
    }

    void DisplayFolders()
    {
        var folders = Directory.GetDirectories("Assets/Resources").ToList();
        for (int i = 0; i < folders.Count; i++) folders[i] = folders[i].Split("/").Last();
        folderDropdown.options = new List<TMP_Dropdown.OptionData>();
        folderDropdown.AddOptions(folders);
    }

    void DisplayJsons(string folder)
    {
        var jsons = Directory.GetDirectories($"Assets/Resources/{folder}").ToList();
        for (int i = 0; i < jsons.Count; i++) jsons[i] = jsons[i].Split("/").Last();
        jsonDropdown.options = new List<TMP_Dropdown.OptionData>();
        jsonDropdown.AddOptions(jsons);
    }

    void LoadGame(string folder, string json)
    {
        GameParams.FolderName = folder;
        GameParams.JsonName = json;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
