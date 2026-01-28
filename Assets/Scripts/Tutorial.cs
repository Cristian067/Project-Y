using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Tutorial : MonoBehaviour
{


    public enum TutorialPhases
    {
        Phase1,
        Phase1Pac,
        Phase2,
        Phase3,
        Phase4,
        Phase5,
        Phase6,
        Phase7,
        Phase8,
    }

    public TutorialPhases actualPhase;
    public GameObject phases;

    public GameObject enemyToTrain;
    public GameObject point;

    [SerializeField] private UpgradeSO upgradeToGive;

    private bool onPhase;

    [SerializeField]private GameObject goal;

    private float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!DialoguesManager.onDialogue && !onPhase)
        {
            GameManager.instance.SetHealth(3);
            switch (actualPhase)
            {
                case TutorialPhases.Phase1:
                StartCoroutine(Phase1());
                break;
                case TutorialPhases.Phase1Pac:
                StartCoroutine(Phase1Pac());
                break;
                case TutorialPhases.Phase2:
                StartCoroutine(Phase2());
                break;
                case TutorialPhases.Phase3:
                StartCoroutine(Phase3());
                break;
            }
        }
        

        
    }


    private IEnumerator Phase1()
    {
        onPhase = true;
        GameObject enemyForTrain = Instantiate(enemyToTrain,new Vector3(0,0,15.5f),Quaternion.Euler(0,180,0));
        time = 0f;
        while (time < 4)
        {
            yield return null;
            if (GameManager.instance.paused)
                continue;
            time += Time.deltaTime;
        }
        DialoguesManager.instance.StartDialogue("TutorialPhase1");
        time = 0f;
        while (time < 10)
        {
            yield return null;
            if (GameManager.instance.paused)
                continue;
            time += Time.deltaTime;
        }

        if (enemyForTrain.gameObject == null)
        {
            Debug.Log("To2");
            DialoguesManager.instance.StartDialogue("TutorialPhase1To2");
            onPhase = false;
            actualPhase = TutorialPhases.Phase2;
        }
        else
        {
            Debug.Log("ToPac");
            DialoguesManager.instance.StartDialogue("TutorialPhasePacifist");
            onPhase = false;
            actualPhase = TutorialPhases.Phase1Pac;
        }
    }


    private IEnumerator Phase1Pac()
    {
        onPhase = true;
        GameObject enemyForTrain = Instantiate(enemyToTrain,new Vector3(0,0,15.5f),Quaternion.Euler(0,180,0));
        time = 0f;
        while (time < 4)
        {
            yield return null;
            if (GameManager.instance.paused)
                continue;
            time += Time.deltaTime;
        }

        time = 0f;
        while (time < 10)
        {
            yield return null;
            if (GameManager.instance.paused)
                continue;
            time += Time.deltaTime;
        }


        if (enemyForTrain.gameObject == null)
        {
            DialoguesManager.instance.StartDialogue("TutorialPhasePacifistExit");
            actualPhase = TutorialPhases.Phase2;
            onPhase = false;
        }
        else
        {
            DialoguesManager.instance.StartDialogue("TutorialPhasePacifistAgain");
            time = 0f;
            while (time < 5)
            {
                yield return null;
                if (GameManager.instance.paused)
                    continue;
                time += Time.deltaTime;
            }
            goal.transform.position = new Vector3(0,0,12);
            onPhase = false;
            //actualPhase = TutorialPhases.Phase1Pac;
        }
        
    }

    private IEnumerator Phase2()
    {
        onPhase = true;
        GameObject pointForTrain = Instantiate(point,new Vector3(0,0,28f),Quaternion.Euler(0,180,0));
        pointForTrain.GetComponent<Item>().speed = 2;
        pointForTrain.GetComponent<Item>().SetType(Item.ItemType.Point);
        pointForTrain.GetComponent<Item>().ChangePointsValue(150);
        

        time = 0f;
        while (time < 1)
        {
            yield return null;
            if (GameManager.instance.paused)
                continue;
            time += Time.deltaTime;
        }
        DialoguesManager.instance.StartDialogue("TutorialPhase2");
        //DialoguesManager.instance.StartDialogue("TutorialPhase2_point");

        pointForTrain.transform.localScale = new Vector3(15,1,15);
        time = 0f;
        while (time < 13)
        {
            yield return null;
            if (GameManager.instance.paused)
                continue;
            time += Time.deltaTime;
        }
        DialoguesManager.instance.StartDialogue("TutorialPhase2_upgrade");
        onPhase = false;
        actualPhase = TutorialPhases.Phase3;


        
        // if(enemyForTrain.gameObject == null)
        // {
        //     DialoguesManager.instance.StartDialogue("TutorialPhase2");
        //     onPhase = false;
        //     actualPhase = TutorialPhases.Phase2;
        // }
        // else
        // {
        //     DialoguesManager.instance.StartDialogue("TutorialPhasePacifist");
        //     onPhase = false;
        //     actualPhase = TutorialPhases.Phase1Pac;
        // }
    }

    private IEnumerator Phase3()
    {
        onPhase = true;
        // GameObject pointForTrain = Instantiate(point,new Vector3(0,0,28f),Quaternion.Euler(0,180,0));
        // pointForTrain.GetComponent<Points>().speed = 2;
        // pointForTrain.GetComponent<Points>().ChangePointsValue(100);
        
    
        DialoguesManager.instance.StartDialogue("TutorialPhase3");

        if(UpgradesManager.instance.special == null)
        {
            DialoguesManager.instance.StartDialogue("TutorialPhase3_NoSpecial");

            UpgradesManager.instance.AdquireUpgrade(upgradeToGive, null);
            //GameManager.instance.AdquireUpgrade(0,upgradeToGive);

        }
        else
        {
            //yield return new WaitForSeconds(0.01f);
            
            DialoguesManager.instance.StartDialogue("TutorialPhase3_HaveSpecial");
            //Debug.Log("Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }
        //DialoguesManager.instance.StartDialogue("TutorialPhase2_point");

        DialoguesManager.instance.StartDialogue("TutorialPhase3_SpecialTest");
        //pointForTrain.transform.localScale = new Vector3(10,10,10);

        for(int i = -7;i < 8; i++)
        {
            GameObject enemyForTrain = Instantiate(enemyToTrain,new Vector3(i,0,15f),Quaternion.Euler(0,180,0));
            enemyForTrain.GetComponent<EnemyBehavior>().life = 8;
            EnemiesMovement movement = enemyForTrain.GetComponent<EnemiesMovement>();
            movement.positions[0] = new Vector3(i,0,-15);

        }
        time = 0f;
        while (time < 15)
        {
            yield return null;
            if (GameManager.instance.paused)
                continue;
            time += Time.deltaTime;
        }

        DialoguesManager.instance.StartDialogue("TutorialPhaseFinal");
        time = 0f;
        while (time < 0.2)
        {
            yield return null;
            if (GameManager.instance.paused)
                continue;
            time += Time.deltaTime;
        }
        End();
        
        // if(enemyForTrain.gameObject == null)
        // {
        //     DialoguesManager.instance.StartDialogue("TutorialPhase2");
        //     onPhase = false;
        //     actualPhase = TutorialPhases.Phase2;
        // }
        // else
        // {
        //     DialoguesManager.instance.StartDialogue("TutorialPhasePacifist");
        //     onPhase = false;
        //     actualPhase = TutorialPhases.Phase1Pac;
        // }
    }


    private void End()
    {
        GameManager.instance.Win();
    }


}
