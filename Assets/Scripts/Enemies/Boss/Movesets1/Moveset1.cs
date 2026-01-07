using System.Collections;
using UnityEngine;

public class Moveset1 : Moveset
{
    public override IEnumerator Use()
    {
        Instantiate(bullet, transform.position, Quaternion.Euler(0, 180, 0));
        yield return null;
    }
}
