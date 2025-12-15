using System.IO;
using System;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class Options : MonoBehaviour
{

    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    [SerializeField] private Toggle fullscreenToogle;
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



    public void ApplySettings()
    {
        
        Settings settings = new Settings();

        settings.sfxVolume = sfxSlider.value;
        settings.musicVolume = musicSlider.value;
        settings.fullscreen = fullscreenToogle.isOn;

        string settingsJson = JsonUtility.ToJson(settings,true);
        File.WriteAllText(pathSettings, settingsJson);
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

        sfxSlider.value = settings.sfxVolume;
        musicSlider.value = settings.musicVolume;
        fullscreenToogle.isOn = settings.fullscreen;
    }
}
