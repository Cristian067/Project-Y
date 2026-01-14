using System.Collections.Generic;
using UnityEngine;

public class Data
{

    public int[] levelsHighScore = new int[99];
    public bool[] levelsCompleted = new bool[99];
    


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


