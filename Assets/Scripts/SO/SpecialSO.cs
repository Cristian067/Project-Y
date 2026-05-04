using System.Collections;
using UnityEngine;

public abstract class SpecialSO : ScriptableObject
{

    public float cooldown;
    public abstract IEnumerator Use(GameObject user);

}
