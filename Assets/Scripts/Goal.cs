using UnityEngine;

public class Goal : MonoBehaviour
{

    public enum GoalType
    {
        Reach,
        KillBoss,
    }

    [SerializeField]private GoalType type;

    //[SerializeField]private int levelNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.paused)
        {
            
        
            switch(type)
            {
                case GoalType.Reach:
                    transform.Translate(new Vector3(0,0,-1)*Time.deltaTime * 1); 
                    if (transform.position.z <= 12)
                    {
                        transform.Translate(new Vector3(0,0,-1)*Time.deltaTime * 30);

                    }
                    break;
            }
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
