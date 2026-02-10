using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class TestSuite
{
    // A Test behaves as an ordinary method
    // [Test]
    // public void TestSuiteSimplePasses()
    // {
    //     // Use the Assert class to test conditions
    // }

    // // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // // `yield return null;` to skip a frame.
    // [UnityTest]
    // public IEnumerator TestSuiteWithEnumeratorPasses()
    // {
    //     GameObject gameManager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"));

    //     MonoBehaviour.Destroy(gameManager.GetComponent<Background>());
    //     MonoBehaviour.Destroy(gameManager.GetComponent<DialoguesManager>());

    //     GameObject gameGameObject = 
    //     MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Points1"));

    //     float initialYPos = gameGameObject.transform.position.y;

    //     gameGameObject.transform.rotation = Quaternion.Euler(0,180,0);
    //     yield return new WaitForSeconds(0.1f);
    //     Assert.Less(gameGameObject.transform.position.y, initialYPos);


    //     // Use the Assert class to test conditions.
    //     // Use yield to skip a frame.
        
    // }


    [UnityTest]
    public IEnumerator PointsGoDown()
    {
        GameObject gameManager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"));
        GameObject canvasGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Canvas"));

        MonoBehaviour.Destroy(gameManager.GetComponent<Background>());
        //MonoBehaviour.Destroy(gameManager.GetComponent<DialoguesManager>());

        gameManager.GetComponent<GameManager>().AddPoints(10);

        GameObject gameGameObject = 
        MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Points1"));

        float initialZPos = gameGameObject.transform.position.z;

        gameGameObject.transform.rotation = Quaternion.Euler(0,180,0);
        yield return new WaitForSeconds(5);
        Assert.Less(gameGameObject.transform.position.z, initialZPos);


        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        
    }

    // [UnityTest]
    // public IEnumerator AllyBulletsDestroy()
    // {
    //     GameObject gameManager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"));
    //     GameObject canvasGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Canvas"));

    //     MonoBehaviour.Destroy(gameManager.GetComponent<Background>());
    //     //MonoBehaviour.Destroy(gameManager.GetComponent<DialoguesManager>());

    //     GameObject bullet1 = 
    //     MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/AllyBullet"));
    //     GameObject bullet2 = 
    //     MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/AllyBullet"));
    //     GameObject bullet3 = 
    //     MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/AllyBullet"));
    //     GameObject bullet4 = 
    //     MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/AllyBullet"));

    //     float initialZPos = bullet1.transform.position.z;

    //     bullet1.GetComponent<BulletsBehavior>().SetSpeed(0);

    //     bullet1.transform.rotation = Quaternion.Euler(0,0,0);
    //     bullet2.transform.rotation = Quaternion.Euler(0,90,0);
    //     bullet3.transform.rotation = Quaternion.Euler(0,180,0);
    //     bullet4.transform.rotation = Quaternion.Euler(0,270,0);
    //     yield return null;

    //     Debug.Log((bullet1 && bullet2 && bullet3 && bullet4));

    //     //Assert.IsFalse((bullet1 && bullet2 && bullet3 && bullet4) != null); //.Less(gameGameObject.transform.position.z, initialZPos);


    //     // Use the Assert class to test conditions.
    //     // Use yield to skip a frame.
        
    // }


    [Test]
    public void GetPointsUntilUpgrade()
    {
        GameObject gameManager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"));
        GameObject canvasGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Canvas"));

        MonoBehaviour.Destroy(gameManager.GetComponent<Background>());
        //MonoBehaviour.Destroy(gameManager.GetComponent<DialoguesManager>());

        //for (int i = 0;)
        gameManager.GetComponent<GameManager>().AddPoints(150);

        // GameObject gameGameObject = 
        // MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Points1"));

        // float initialZPos = gameGameObject.transform.position.z;

        // gameGameObject.transform.rotation = Quaternion.Euler(0,180,0);
        Assert.AreEqual(0, gameManager.GetComponent<GameManager>().GetPoints());
        //Assert.Less(gameGameObject.transform.position.z, initialZPos);


        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        
    }


    [UnityTest]
    public IEnumerator BackgroundMoves()
    {

        GameObject background = 
        MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/BackgroundPlane"));

        background.name = "BackgroundPlane";
        GameObject gameManager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"));
        GameObject canvasGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Canvas"));

        //MonoBehaviour.Destroy(gameManager.GetComponent<Background>());
        MonoBehaviour.Destroy(gameManager.GetComponent<DialoguesManager>());

        

        float initialOffset = background.GetComponent<MeshRenderer>().material.GetVector("_Offset").y;

        yield return new WaitForSeconds(5f);
        Assert.Greater(background.GetComponent<MeshRenderer>().material.GetVector("_Offset").y, initialOffset);


        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        
    }


}
