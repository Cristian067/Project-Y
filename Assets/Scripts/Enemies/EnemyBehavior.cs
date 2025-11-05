using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{



    [SerializeField] private int life;
    [SerializeField] private float speed;
    [SerializeField] private float activationZ;
    [SerializeField] private bool activated;
    [SerializeField] private bool haveToDestroyAfterPath;

    [SerializeField] private bool canShoot;
    

    [SerializeField] private GameObject pointGo;

    [SerializeField] private bool isMoving = true;
    // [SerializeField] private Vector3 posToMove;
    // [SerializeField] private Vector3 directionToMove;
    // [SerializeField] private (Vector3,bool) testmove;

    [SerializeField] private int positionCount;

    EnemiesMovement movementBehavior;

    // Start is called before the first frame update
    void Start()
    {
        movementBehavior = GetComponent<EnemiesMovement>();
        transform.position = new Vector3(movementBehavior.positions[0].x, 0, transform.position.z);

        
            
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForActivation();

        if (activated)
        {
            if (isMoving)
            {

                if (positionCount >= movementBehavior.positions.Count)
                {
                    if (haveToDestroyAfterPath)
                    {
                        Destroy(gameObject);
                    }
                    
                }

                else
                {
                    transform.Translate(-(movementBehavior.positions[positionCount] - transform.position).normalized * speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, movementBehavior.positions[positionCount]) < 0.1f)
                    {
                        Debug.Log("cambio");
                        positionCount++;
                    }
                    
                    
                }

            }
            if (transform.position.z <= -5.5f)
            {
                Destroy(gameObject);
            }
        }

        else
        {
            transform.Translate(new Vector3(0, 0, 1).normalized * 1 * Time.deltaTime);
        }
        
    }
    
    private void CheckForActivation()
    {


        if (transform.position.z > activationZ)
        {
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            if (!activated)
            {
                GetComponent<Collider>().enabled = true;
                if (canShoot)
                {
                    GetComponent<EnemyShoot>().StartShooting();

                }
                activated = true;
            }


            
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
