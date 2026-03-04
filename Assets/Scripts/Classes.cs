using System;
using System.Collections.Generic;
using UnityEngine;


public class RateData
{
    public bool rated;
    public string api_token;
    public string email;
    public string name;
    public int general;
    public int jugabilitat;
    public int dificultat;
    public int grafics;
    public int concordnacia;
}


[Serializable]
public class DataFromApi
{
    public List<Scores> data;
}
[Serializable]
public class Scores
{
    public string name;
    public int puntuacion;
}

[Serializable]
public class PostData
{
    public string api_token = "ZHVxZUtGF4E0wzz0400BRy8imjHDgZPmL5m5UD5VYBUCstloOUH2sSbbS9ef";
    public string name;
    public string email;
    public int puntuacion;
    [Header("RateThings")]
    //Rate Things

    public bool rated;
    public int general;
    public int jugabilitat;
    public int dificultat;
    public int grafics;
    public int concordnacia;
   
}