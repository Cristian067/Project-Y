using UnityEngine;

public class Goal : MonoBehaviour
{

    public enum GoalType
    {
        Reach,
        KillBoss,
    }

    [SerializeField]private GoalType type;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        switch(type)
        {
            case GoalType.Reach:
                transform.Translate(new Vector3(0,0,-1)*Time.deltaTime * 1); 
                break;
        }



    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hitbox")
        {
            GameManager.instance.Win();
        }
    }



}
