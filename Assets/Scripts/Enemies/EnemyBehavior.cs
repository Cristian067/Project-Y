using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{



    [SerializeField] public float life;
    [SerializeField] private float speed;
    [SerializeField] private float activationZ;
    [SerializeField] private bool activated;
    [SerializeField] private bool haveToDestroyAfterPath;
    [SerializeField] private float timeToFlee;

    [SerializeField] private bool canShoot;
    

    [SerializeField] private GameObject pointGo;
    [SerializeField] private int setPointValue;

    [SerializeField] private bool isMoving = true;
    // [SerializeField] private Vector3 posToMove;
    // [SerializeField] private Vector3 directionToMove;
    // [SerializeField] private (Vector3,bool) testmove;

    [SerializeField] private int positionCount;

    EnemiesMovement movementBehavior;
    Animator animator;

    public MeshRenderer meshRenderer;

    private bool dead = false;

    Color colorOg;

    // Start is called before the first frame update
    void Start()
    {
        colorOg = meshRenderer.material.color;
        movementBehavior = GetComponent<EnemiesMovement>();
        animator = GetComponent<Animator>();
        transform.position = new Vector3(movementBehavior.positions[0].x, 0, transform.position.z);

        
            
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.instance.paused)
        {
            return;
        }

        
            
        
        CheckForActivation();

        if (activated)
        {
            if (isMoving)
            {

                if (positionCount >= movementBehavior.positions.Count)
                {
                    float countTime = Time.deltaTime;

                        
                    if (haveToDestroyAfterPath)
                    {
                        Destroy(gameObject);
                    }
                    else if (timeToFlee > countTime)
                    {
                        GetComponent<EnemyShoot>().StopShooting();
                        transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * speed * 4);
                    }
                        
                        
                }

                else
                {
                    transform.Translate(-(movementBehavior.positions[positionCount] - transform.position).normalized * speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, movementBehavior.positions[positionCount]) < 0.1f)
                    {
                        //Debug.Log("cambio");
                        positionCount++;
                    }
                        
                        
                }

            }
            if (transform.position.z <= -7.5f)
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

        if (transform.position.z > activationZ && !activated)
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

    public void Hurt(float damage)
    {
        life -= damage;
        animator.SetTrigger("hurt");
        //StartCoroutine(HurtVisual());

        if (life <= 0 && !dead)
        {
            dead = true;
            GameObject point = Instantiate(pointGo,transform.position,Quaternion.Euler(0, 180, 0));
            point.GetComponent<Item>().ChangePointsValue(setPointValue);
            Destroy(gameObject);
        }
    }

    // private IEnumerator HurtVisual()
    // {
        
    //     

    //     // Debug.Log(colorOg.ToString());

    //     // meshRenderer.material.color = new Color(1,1,1,1);
    //     // yield return new WaitForSeconds(0.1f);
    //     // meshRenderer.material.color = colorOg;
    // }

    public void ChangePointsValue(int value)
    {
        setPointValue = value;
    }

    public void ChangeSpeed(float value)
    {
        speed = value;
    }
    // void OnDestroy()
    // {
        
    // }


}
