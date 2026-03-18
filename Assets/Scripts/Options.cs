using System.IO;
using System;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{

    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    [SerializeField] private TextMeshProUGUI musicValue;
    [SerializeField] private TextMeshProUGUI sfxValue;


    [SerializeField] private Toggle fullscreenToogle;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private string pathSettings = "save/settings.json";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //LoadSettings();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVisualValue(float value)
    {

        musicValue.text = musicSlider.value.ToString();
        sfxValue.text = sfxSlider.value.ToString();
        
    }


    public void ApplySettings()
    {
        
        Settings settings = new Settings();

        settings.sfxVolume = sfxSlider.value/100;
        settings.musicVolume = musicSlider.value/100;
        settings.fullscreen = fullscreenToogle.isOn;

        string settingsJson = JsonUtility.ToJson(settings,true);
        ApplyResolution();
        File.WriteAllText(pathSettings, settingsJson);

        Log.AddToLog($"Applied options: -Music: {settings.musicVolume*100}% -Sfx: {settings.sfxVolume*100}% -FullScreen: {settings.fullscreen}");
    }


    private void ApplyResolution()
    {
        string[] resolution = resolutionDropdown.options[resolutionDropdown.value].text.Split('x');


        Vector2 resolutionInt = new Vector2(int.Parse(resolution[0]),int.Parse(resolution[1]));

        Screen.SetResolution(Mathf.RoundToInt(resolutionInt.x),Mathf.RoundToInt(resolutionInt.y),fullscreenToogle.isOn);
    }
    // private void LoadSettings()
    // {
    //     if (File.Exists(pathSettings))
    //     {
    //         Settings settings= new Settings();
    //         string settingsJson = File.ReadAllText(pathSettings);
    //         settings = JsonUtility.FromJson<Settings>(settingsJson);

    //         sfxSlider.value = settings.sfxVolume;
    //         musicSlider.value = settings.musicVolume;
    //         fullscreenToogle.isOn = settings.fullscreen;
    //     }

    // }

    // public void SaveSettings()
    // {
        

    // }


    void OnEnable()
    {
        Settings settings = new Settings();

        if (!File.Exists(pathSettings))
        {
            Directory.CreateDirectory("save");
            

            string settingsJson = JsonUtility.ToJson(settings,true);
            //File.Create(pathSettings);

            Debug.Log(settingsJson);
            File.WriteAllText(pathSettings, settingsJson);
        }

        else
        {
            string json = File.ReadAllText(pathSettings);
            settings = JsonUtility.FromJson<Settings>(json);
        }

        sfxSlider.value = settings.sfxVolume *100;
        musicSlider.value = settings.musicVolume*100;
        fullscreenToogle.isOn = settings.fullscreen;
    }
}
