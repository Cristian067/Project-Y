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
            switch (actualPhase)
            {
                case TutorialPhases.Phase1:
                StartCoroutine(Phase1());
                break;
                case TutorialPhases.Phase1Pac:
                StartCoroutine(Phase1Pac());
                break;
            }
        }
        

        
    }


    private IEnumerator Phase1()
    {
        onPhase = true;
        GameObject enemyForTrain = Instantiate(enemyToTrain,new Vector3(0,0,15.5f),Quaternion.Euler(0,180,0));
        yield return new WaitForSeconds(4f);
        DialoguesManager.instance.StartDialogue(1);
        yield return new WaitForSeconds(10);

        
        if(enemyForTrain.gameObject == null)
        {
            DialoguesManager.instance.StartDialogue(2);
        }
        else
        {
            DialoguesManager.instance.StartDialogue(3);
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
            DialoguesManager.instance.StartDialogue(2);
        }
        else
        {
            DialoguesManager.instance.StartDialogue(4);
            yield return new WaitForSeconds(5);
            goal.transform.position = new Vector3(0,0,12);
            onPhase = false;
            //actualPhase = TutorialPhases.Phase1Pac;
        }
    }







}
