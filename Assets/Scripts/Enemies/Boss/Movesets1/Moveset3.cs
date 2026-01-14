using System.Collections;
using UnityEngine;

public class Moveset3 : Moveset
{
    public override IEnumerator Use()
    {

        


        for (int i = 0; i < 30; i++)
        {
            for(int j = 0; j < 20; j++)
            {
                float radual = 360/ 20;
                GameObject bulletInScene = Instantiate(bullet,transform.position,Quaternion.Euler(0,(j+1)*radual,0));
                bulletInScene.transform.localScale = new Vector3(0.25f, 0.25f,0.25f);
                bulletInScene.GetComponent<BulletsBehavior>().SetRotation(new Vector3(0,-25,0),2);
            }
            float time = 0f;
            while (time < 0.1f)
            {
                yield return null;
                if (GameManager.instance.paused)
                continue;
                time += Time.deltaTime;
            }
        }
        



        GetComponent<BossBehavior>().ChangeInAttack(false);

        
    }
}
