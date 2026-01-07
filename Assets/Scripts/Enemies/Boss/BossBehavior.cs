using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;





public class BossBehavior : MonoBehaviour
{



    public MonoBehaviour[] Movesets;
    public UpgradeSO[] Upgrades;


    public enum States
    {
        Move,
        Attack,
        Wait
    }

    private States actState;
    private States nextState;

    [SerializeField]private float life;
    [SerializeField]private float speed;
    [SerializeField]private Vector2 mapAnchor;
    [SerializeField]private float cooldownBetweenMovesets;

    [SerializeField]private bool activated;
    [SerializeField]private bool idle;

    






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (activated && !idle)
        {
            switch (actState)
            {
                case States.Move:
                    //idle = true;
                    StartCoroutine(Move());
                    break;
                case States.Attack:
                    UseMoveset();
                    break;
                case States.Wait:
                    Debug.Log("Esperando");
                    break;
            }
            StartCoroutine(Wait());
        }

        
    }



    public void UseMoveset()
    {
        int r = Random.Range(0, Movesets.Length);

        Movesets[r].Invoke("Use",0);

    }

    public IEnumerator Wait()
    {
        if (actState == States.Attack)
        {
            nextState = States.Move;
        }
        if (actState == States.Move)
        {
            nextState = States.Attack;
            
        }
        actState = States.Wait;
        
        idle = true;
        yield return new WaitForSeconds(cooldownBetweenMovesets);
        actState = nextState;
        idle = false;

       
    }

    public IEnumerator Move()
    {
        //idle = true;
        float r = Random.Range(mapAnchor.x, mapAnchor.y);

        Vector3 destination = new Vector3(r,transform.position.y,transform.position.z);
        //Debug.Log("destinacion: "+destination);

        while (Vector3.Distance(transform.position,destination) > 0.2)
        {
            //Debug.Log("distancia: " +Vector3.Distance(transform.position,destination));
            transform.Translate(-(destination - transform.position).normalized * Time.deltaTime * speed);
            
            yield return null;
        }

        //destination = transform;

        
    }


    public void Hurt(float damage)
    {
        life -= damage;
        if (life < 0)
        {
            Destroy(gameObject);
        }
    }


}
