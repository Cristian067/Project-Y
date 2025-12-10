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
        yield return new WaitForSeconds(4f);
        DialoguesManager.instance.StartDialogue("TutorialPhase1");
        yield return new WaitForSeconds(10);
        
        if(enemyForTrain.gameObject == null)
        {
            DialoguesManager.instance.StartDialogue("TutorialPhase1To2");
            onPhase = false;
            actualPhase = TutorialPhases.Phase2;
        }
        else
        {
            DialoguesManager.instance.StartDialogue("TutorialPhasePacifist");
            onPhase = false;
            actualPhase = TutorialPhases.Phase1Pac;
        }
    }


    private IEnumerator Phase1Pac()
    {
        onPhase = true;
        GameObject enemyForTrain = Instantiate(enemyToTrain,new Vector3(0,0,15.5f),Quaternion.Euler(0,180,0));
        yield return new WaitForSeconds(4f);
 
        yield return new WaitForSeconds(10);

        
        if(enemyForTrain.gameObject == null)
        {
            DialoguesManager.instance.StartDialogue("TutorialPhasePacifistExit");
            actualPhase = TutorialPhases.Phase2;
        }
        else
        {
            DialoguesManager.instance.StartDialogue("TutorialPhasePacifistAgain");
            yield return new WaitForSeconds(5);
            goal.transform.position = new Vector3(0,0,12);
            onPhase = false;
            //actualPhase = TutorialPhases.Phase1Pac;
        }
    }

    private IEnumerator Phase2()
    {
        onPhase = true;
        GameObject pointForTrain = Instantiate(point,new Vector3(0,0,28f),Quaternion.Euler(0,180,0));
        pointForTrain.GetComponent<Points>().speed = 2;
        pointForTrain.GetComponent<Points>().ChangePointsValue(100);
        
        yield return new WaitForSeconds(1f);
        DialoguesManager.instance.StartDialogue("TutorialPhase2");
        //DialoguesManager.instance.StartDialogue("TutorialPhase2_point");

        pointForTrain.transform.localScale = new Vector3(10,10,10);
        yield return new WaitForSeconds(13);
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

        if(GameManager.instance.GetSpecial() == null)
        {
            DialoguesManager.instance.StartDialogue("TutorialPhase3_NoSpecial");

            GameManager.instance.AdquireUpgrade(0,upgradeToGive);

        }
        //DialoguesManager.instance.StartDialogue("TutorialPhase2_point");

        DialoguesManager.instance.StartDialogue("TutorialPhase3_SpecialTest");
        //pointForTrain.transform.localScale = new Vector3(10,10,10);

        for(int i = -7;i < 7; i++)
        {
            GameObject enemyForTrain = Instantiate(enemyToTrain,new Vector3(i,0,15.5f),Quaternion.Euler(0,180,0));
            enemyForTrain.GetComponent<EnemyBehavior>().life = 8;
            EnemiesMovement movement = enemyForTrain.GetComponent<EnemiesMovement>();
            movement.positions[0] = new Vector3(i,0,-15);

        }
        yield return new WaitForSeconds(15);
        
        DialoguesManager.instance.StartDialogue("TutorialPhase2_upgrade");


        
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




}
