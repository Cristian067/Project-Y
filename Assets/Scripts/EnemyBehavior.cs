using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{



    [SerializeField] private int life;
    [SerializeField] private float speed;

    [SerializeField] private bool canShoot;
    

    [SerializeField] private GameObject pointGo;

    [SerializeField] private bool isMoving = true;
    // [SerializeField] private Vector3 posToMove;
    // [SerializeField] private Vector3 directionToMove;
    // [SerializeField] private (Vector3,bool) testmove;

    [SerializeField] private EnemyMoveSO movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
        }
        


        if(transform.position.z <= -5.5f)
        {
            Destroy(gameObject);
        }
    }


    public void Go()
    {

    }
    public void Stop()
    {

        isMoving = false;

    }


    public void Hurt(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            Instantiate(pointGo,transform.position,Quaternion.Euler(0, 180, 0));
            Destroy(gameObject);
        }
    }

    // void OnDestroy()
    // {
        
    // }


}
