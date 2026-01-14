using System.Collections;
using UnityEngine;

public class Moveset4 : Moveset
{

    public GameObject enemy;
    public override IEnumerator Use()
    {

        

            //63036

        GameObject enemyInScene;

        for (int i = -6; i < 7; i += 3)
        {
            enemyInScene = Instantiate(enemy, new Vector3(0,0,15),Quaternion.Euler(0,180,0));
            enemyInScene.GetComponent<EnemiesMovement>().positions[0] = new Vector3(i,0,-15);
            enemyInScene.GetComponent<EnemyBehavior>().ChangePointsValue(0);
            enemyInScene.GetComponent<EnemyBehavior>().ChangeSpeed(2.5f);
        }
        

        // enemyInScene = Instantiate(enemy, new Vector3(3,0,15),Quaternion.Euler(0,180,0));
        // enemyInScene.GetComponent<EnemiesMovement>().positions[0] = new Vector3(3,0,-15);
        // enemyInScene.GetComponent<EnemyBehavior>().ChangePointsValue(0);
        // enemyInScene.GetComponent<EnemyBehavior>().ChangeSpeed(2.5f);

        // enemyInScene = Instantiate(enemy, new Vector3(0,0,15),Quaternion.Euler(0,180,0));
        // enemyInScene.GetComponent<EnemiesMovement>().positions[0] = new Vector3(0,0,-15);
        // enemyInScene.GetComponent<EnemyBehavior>().ChangePointsValue(0);
        // enemyInScene.GetComponent<EnemyBehavior>().ChangeSpeed(2.5f);

        // enemyInScene = Instantiate(enemy, new Vector3(-3,0,15),Quaternion.Euler(0,180,0));
        // enemyInScene.GetComponent<EnemiesMovement>().positions[0] = new Vector3(-3,0,-15);
        // enemyInScene.GetComponent<EnemyBehavior>().ChangePointsValue(0);
        // enemyInScene.GetComponent<EnemyBehavior>().ChangeSpeed(2.5f);

        // enemyInScene = Instantiate(enemy, new Vector3(-6,0,15),Quaternion.Euler(0,180,0));
        // enemyInScene.GetComponent<EnemiesMovement>().positions[0] = new Vector3(-6,0,-15);
        // enemyInScene.GetComponent<EnemyBehavior>().ChangePointsValue(0);
        // enemyInScene.GetComponent<EnemyBehavior>().ChangeSpeed(2.5f);
  




        GetComponent<BossBehavior>().ChangeInAttack(false);
        yield return null;

        
    }
}
