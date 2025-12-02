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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        switch (actualPhase)
        {
            case TutorialPhases.Phase1:
            break;
        }

        
    }
}
