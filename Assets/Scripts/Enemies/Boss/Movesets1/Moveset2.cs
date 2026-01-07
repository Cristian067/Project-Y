using System.Collections;
using UnityEngine;

public class Moveset2 : Moveset
{
    public override IEnumerator Use()
    {

        //GetComponent<BossBehavior>().Move(true);;
        for (int i = 0; i < 30; i++)
        {
            for(int j = 0; j < 20; j++)
            {
                float radual = 360/ 20;
                GameObject bulletInScene = Instantiate(bullet,transform.position,Quaternion.Euler(0,(j+1)*radual,0));
                bulletInScene.GetComponent<BulletsBehavior>().SetRotation(new Vector3(0,25,0),2);
            }
            yield return new WaitForSeconds(0.1f);
        }

        

        
    }
}
