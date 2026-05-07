using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Moveset2_5 : Moveset
{

    //public GameObject enemy;
    public GameObject dangerZone;
    public override IEnumerator Use()
    {

        var dangerZone = Instantiate(Resources.Load<GameObject>("Prefabs/DangerZone"), new Vector3(0,0,0),Quaternion.identity).GetComponent<DangerZone>();
        var dangerZone2 = Instantiate(Resources.Load<GameObject>("Prefabs/DangerZone"), new Vector3(0,0,0),Quaternion.identity).GetComponent<DangerZone>();

        dangerZone.SetUp(new Vector3(-4.3f,0,2.4f),new Vector3(5,2,21),DangerZone.Type.Square);
        dangerZone2.SetUp(new Vector3(4.3f,0,2.4f),new Vector3(5,2,21),DangerZone.Type.Square);

        yield return new WaitForSeconds(1);

        GameObject bulletOut = Instantiate(bullet, new Vector3(-4.3f,0,30), Quaternion.Euler(0,180,0));
        bulletOut.transform.localScale = new Vector3(5,5,5);
        bulletOut.GetComponent<BulletsBehavior>().SetSpeed(24);
        GameObject bulletOut2 = Instantiate(bullet, new Vector3(4.3f,0,30), Quaternion.Euler(0,180,0));
        bulletOut2.transform.localScale = new Vector3(5,5,5);
        bulletOut2.GetComponent<BulletsBehavior>().SetSpeed(24);


        yield return new WaitForSeconds(1.7f);
        Destroy(dangerZone.gameObject);
        Destroy(dangerZone2.gameObject);

        
        GetComponent<BossBehavior>().ChangeInAttack(false);
        
    }
}
