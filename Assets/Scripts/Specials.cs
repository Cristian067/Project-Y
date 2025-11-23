using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Specials/Eternal Bloom")]
public class EternalBloom : SpecialSO
{

    public override IEnumerator Use(GameObject owner)
    { 
        float speedOg = GameManager.instance.GetSpeed();
        GameManager.instance.SetSpeed(speedOg * 1.5f);
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(5);
        Time.timeScale = 1;
        GameManager.instance.SetSpeed(speedOg);
    }

}


[CreateAssetMenu(menuName = "ScriptableObject/Specials/Bomb Rush")]
public class BombRush : SpecialSO
{
    public GameObject bomb;
    public GameObject allyBullet;
    public override IEnumerator Use(GameObject owner)
    {
        float speedOg = GameManager.instance.GetSpeed();
        
        GameObject bomb1 = Instantiate(bomb,new Vector3(0,0,-9.5f), Quaternion.identity);
        bomb1.GetComponent<BulletsBehavior>().ChangeEnemy(false);
        bomb1.GetComponent<BulletsBehavior>().SetBomb(5,18,4);
        bomb1.GetComponent<BulletsBehavior>().bulletForExplode = allyBullet;
        bomb1.transform.localScale = new Vector3(2,2,2);

        GameObject bomb2 = Instantiate(bomb, new Vector3(4, 0, -9.5f), Quaternion.identity);
        bomb2.GetComponent<BulletsBehavior>().ChangeEnemy(false);
        bomb2.GetComponent<BulletsBehavior>().SetBomb(5, 18, 4);
        bomb2.GetComponent<BulletsBehavior>().bulletForExplode = allyBullet;
        bomb2.transform.localScale = new Vector3(2, 2, 2);

        GameObject bomb3 = Instantiate(bomb, new Vector3(-4, 0, -9.5f), Quaternion.identity);
        bomb3.GetComponent<BulletsBehavior>().ChangeEnemy(false);
        bomb3.GetComponent<BulletsBehavior>().SetBomb(5, 18, 4);
        bomb3.GetComponent<BulletsBehavior>().bulletForExplode = allyBullet;
        bomb3.transform.localScale = new Vector3(2, 2, 2);
        yield return new WaitForSeconds(3);
        bomb1.GetComponent<BulletsBehavior>().ChangeSpeed(0);
        bomb2.GetComponent<BulletsBehavior>().ChangeSpeed(0);
        bomb2.transform.rotation = Quaternion.Euler(0,25,0);
        bomb3.GetComponent<BulletsBehavior>().ChangeSpeed(0);
        bomb3.transform.rotation = Quaternion.Euler(0, 50, 0);
        //bomb1.transform.Translate(0, 0, 1);


        Time.timeScale = 1;
        GameManager.instance.SetSpeed(speedOg);
    }

}
