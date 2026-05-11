using System.Collections;
using UnityEngine;

public class Moveset2_2 : Moveset
{
    public override IEnumerator Use()
    {

        //GetComponent<BossBehavior>().Move(true);;
        for (int i = 0; i < 5; i++)
        {
            var bullet1 = Instantiate(bullet, new Vector3(7,0,12), Quaternion.Euler(0, 180, 0));
            bullet1.transform.localScale = new Vector3(2.5f, 2.5f,2.5f);
            float time = 0f;
            while (time < 0.1f)
            {
                yield return null;
                if (GameManager.instance.paused)
                continue;
                time += Time.deltaTime;
            }
            var bullet2 = Instantiate(bullet, new Vector3(0,0,12), Quaternion.Euler(0, 180, 0));
            bullet2.transform.localScale = new Vector3(2.5f, 2.5f,2.5f);
            time = 0f;
            while (time < 0.1f)
            {
                yield return null;
                if (GameManager.instance.paused)
                continue;
                time += Time.deltaTime;
            }

            var bullet3 = Instantiate(bullet, new Vector3(-7,0,12), Quaternion.Euler(0, 180, 0));
            bullet3.transform.localScale = new Vector3(2.5f, 2.5f,2.5f);
            time = 0f;
            while (time < 0.1f)
            {
                yield return null;
                if (GameManager.instance.paused)
                continue;
                time += Time.deltaTime;
            }
            // for(int j = 0; j < 20; j++)
            // {
            //     float radual = 360/ 20;
            //     GameObject bulletInScene = Instantiate(bullet,transform.position,Quaternion.Euler(0,(j+1)*radual,0));
            //     bulletInScene.transform.localScale = new Vector3(0.25f, 0.25f,0.25f);
            //     bulletInScene.GetComponent<BulletsBehavior>().SetRotation(new Vector3(0,25,0),2);
            // }
            
        }
        
        GetComponent<BossBehavior>().ChangeInAttack(false);

        
    }
}
