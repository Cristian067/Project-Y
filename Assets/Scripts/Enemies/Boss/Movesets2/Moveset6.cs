using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Moveset2_6 : Moveset
{

    //public GameObject enemy;
    //public GameObject dangerZone;
    public override IEnumerator Use()
    {

        for (int i = 0; i < 5; i++)
        {

            GameObject bullet1 = Instantiate(bullet,transform.position,Quaternion.Euler(0,180,0));
            GameObject bullet2 = Instantiate(bullet,transform.position,Quaternion.Euler(0,180+15,0));
            GameObject bullet3 = Instantiate(bullet,transform.position,Quaternion.Euler(0,180-15,0));
            float time = 0f;
            while (time < 0.5f)
            {
                yield return null;
                if (GameManager.instance.paused)
                continue;
                time += Time.deltaTime;
            }

            GameObject bullet4 = Instantiate(bullet,transform.position,Quaternion.Euler(0,180 + 7.5f,0));
            GameObject bullet5 = Instantiate(bullet,transform.position,Quaternion.Euler(0,180 - 7.5f,0));
            GameObject bullet6 = Instantiate(bullet,transform.position,Quaternion.Euler(0,180 + 7.5f+15,0));
            GameObject bullet7 = Instantiate(bullet,transform.position,Quaternion.Euler(0,180 - 7.5f-15,0));
 
            time = 0f;
            while (time < 0.5f)
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
