using System.Collections;
using UnityEngine;

public class Moveset2_4 : Moveset
{

    public override IEnumerator Use()
    {

        GameObject bullet1 = Instantiate(bullet,transform.position,Quaternion.Euler(0,180,0));
        GameObject bullet2 = Instantiate(bullet,transform.position,Quaternion.Euler(0,180+15,0));
        GameObject bullet3 = Instantiate(bullet,transform.position,Quaternion.Euler(0,180-15,0));
            //63036

        GetComponent<BossBehavior>().ChangeInAttack(false);
        yield return null;

        
    }
}
