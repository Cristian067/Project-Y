using System.Collections;
using UnityEngine;

public class Moveset2_3 : Moveset
{
    public override IEnumerator Use()
    {

        var dangerZone = Instantiate(Resources.Load<GameObject>("Prefabs/DangerZone"), new Vector3(0,0,0),Quaternion.identity).GetComponent<DangerZone>();
        var dangerZone2 = Instantiate(Resources.Load<GameObject>("Prefabs/DangerZone"), new Vector3(0,0,0),Quaternion.identity).GetComponent<DangerZone>();
        var dangerZone3 = Instantiate(Resources.Load<GameObject>("Prefabs/DangerZone"), new Vector3(0,0,0),Quaternion.identity).GetComponent<DangerZone>();

        dangerZone.SetUp(new Vector3(0f,0,0f),new Vector3(40,0.5f,2.5f),DangerZone.Type.Square,3);
        dangerZone2.SetUp(new Vector3(0f,0f,6f),new Vector3(40,0.5f,2.5f),DangerZone.Type.Square,3);
        dangerZone3.SetUp(new Vector3(0f,0,-6f),new Vector3(40,0.5f,2.5f),DangerZone.Type.Square,3);


        float time = 0f;
        while (time < 0.5f)
        {
            yield return null;
            if (GameManager.instance.paused)
            continue;
            time += Time.deltaTime;
        }

        GameObject bulletMid = Instantiate(bullet,new Vector3(20f,0,0f),Quaternion.Euler(0,-90,0));
        GameObject bulletTop = Instantiate(bullet,new Vector3(-20f,0,6f),Quaternion.Euler(0,90,0));
        GameObject bulletBot = Instantiate(bullet,new Vector3(-20f,0,-6f),Quaternion.Euler(0,90,0));
        bulletMid.transform.localScale = new Vector3(2,2,2);
        bulletTop.transform.localScale = new Vector3(2,2,2);
        bulletBot.transform.localScale = new Vector3(2f, 2f, 2f);


        time = 0f;
        while (time < 0.5f)
        {
            yield return null;
            if (GameManager.instance.paused)
            continue;
            time += Time.deltaTime;
        }
        // for (int i = 0; i < 30; i++)
        // {
        //     for(int j = 0; j < 20; j++)
        //     {
        //         float radual = 360/ 20;
        //         GameObject bulletInScene = Instantiate(bullet,transform.position,Quaternion.Euler(0,(j+1)*radual,0));
        //         bulletInScene.transform.localScale = new Vector3(0.25f, 0.25f,0.25f);
        //         bulletInScene.GetComponent<BulletsBehavior>().SetRotation(new Vector3(0,-25,0),2);
        //     }
            
        // }
        



        GetComponent<BossBehavior>().ChangeInAttack(false);

        
    }
}
