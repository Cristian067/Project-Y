using System;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/NetworkData")]
public class NetworkDataSO : ScriptableObject
{

    public string apiUrl;
    public string token;
}
