using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{

    public enum GoalType
    {
        Reach,
        KillBoss,
    }

    [SerializeField]private GoalType type;

    [SerializeField]private GameObject boss;

    [SerializeField]private bool haveTextBeforeBoss;

    //[SerializeField]private int levelNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.paused)
        {
            return;
        }
        
        // switch(type)
        // {
        //     case GoalType.Reach:
                transform.Translate(new Vector3(0,0,-1)*Time.deltaTime * 1); 
                if (transform.position.z <= 12)
                {
                    transform.Translate(new Vector3(0,0,-1)*Time.deltaTime * 30);

                }
                //break;

            //case GoalType.KillBoss:
            //transform.Translate(new Vector3(0,0,-1)*Time.deltaTime * 1); 
            

            //break;
        //}
        
        

    }


    private void OnTriggerEnter(Collider other)
    {
        

        if(type == GoalType.KillBoss)
        {
            if (haveTextBeforeBoss)
            {
                DialoguesManager.instance.StartDialogue("Boss");
            }
            Instantiate(boss);
        }
        else if(type == GoalType.Reach)
        {
            if(other.gameObject.tag == "Hitbox")
            {
                GameManager.instance.Win();
            }
        }
        GetComponent<Collider>().enabled = false;
        
    }



}
