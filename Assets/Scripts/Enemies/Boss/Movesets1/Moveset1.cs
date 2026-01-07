using UnityEngine;

public class Moveset1 : Moveset
{
    public override void Use()
    {
        Instantiate(bullet, transform.position, Quaternion.Euler(0, 180, 0));
    }
}
