using System.Collections;
using UnityEngine;

public class Moveset2_3 : Moveset
{
    public override IEnumerator Use()
    {

        var dangerZone = Instantiate(Resources.Load<GameObject>("Prefabs/DangerZone"), new Vector3(0,0,0),Quaternion.identity).GetComponent<DangerZone>();
        var dangerZone2 = Instantiate(Resources.Load<GameObject>("Prefabs/DangerZone"), new Vector3(0,0,0),Quaternion.identity).GetComponent<DangerZone>();
        var dangerZone3 = Instantiate(Resources.Load<GameObject>("Prefabs/DangerZone"), new Vector3(0,0,0),Quaternion.identity).GetComponent<DangerZone>();

        dangerZone.SetUp(new Vector3(0f,0,0f),new Vector3(20,0.5f,1.5f),DangerZone.Type.Square);
        dangerZone2.SetUp(new Vector3(0f,0,6f),new Vector3(20,0.5f,1.5f),DangerZone.Type.Square);
        dangerZone3.SetUp(new Vector3(0f,0,-6f),new Vector3(20,0.5f,1.5f),DangerZone.Type.Square);


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
