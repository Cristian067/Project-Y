using System.Collections;
using UnityEngine;

public abstract class SpecialSO : ScriptableObject
{
    public abstract IEnumerator Use(GameObject user);

}
