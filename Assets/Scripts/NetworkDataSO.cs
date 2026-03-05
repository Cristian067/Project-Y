using System;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/NetworkData")]
public class NetworkDataSO : ScriptableObject
{


    public enum NetworkType
    {
        classification,
        verify,
        rateGame

    }

    public string apiUrl;
    public string token;
}
