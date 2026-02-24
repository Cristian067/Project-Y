using System.Collections.Generic;
using UnityEngine;

public class Data
{

    public string username;
    public string email;
    public int[] levelsHighScore = new int[10];
    public bool[] levelsCompleted = new bool[10];
    


    public List<string> discoveredUpgrades;




    

}

public class Settings
{

    //public float generalVolume;
    public float sfxVolume = 1;
    public float musicVolume = 1;

    public bool fullscreen;

    public Vector2 resolution = new Vector2(1920,1080);



}


