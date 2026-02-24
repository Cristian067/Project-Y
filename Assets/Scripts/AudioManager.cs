using System.IO;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip musicClip;


    private string pathSettings = "save/settings.json";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ApplyVolume()
    {
        if (File.Exists(pathSettings))
        {
            string raw = File.ReadAllText(pathSettings);
            Settings settings = JsonUtility.FromJson<Settings>(raw);
            musicSource.volume = settings.musicVolume;
        }
    }

    public float GetSfxVolume()
    {
        if (File.Exists(pathSettings))
        {
            string raw = File.ReadAllText(pathSettings);
            Settings settings = JsonUtility.FromJson<Settings>(raw);
            return settings.sfxVolume;
        }
        else return 1f;
    }

    public void PlaySfx(AudioClip clip)
    {
        
    }

    public void ChangeMusic(AudioClip music)
    {
        if (music == null)
        {
            return;
        }
        if (music != musicClip)
        {
            musicClip = music;
        }

        musicSource.clip = musicClip;
        
    }


}
