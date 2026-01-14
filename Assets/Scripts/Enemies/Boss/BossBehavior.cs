using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Stats 
{
    public float life;
    public float speed;


}



public class BossBehavior : MonoBehaviour
{



    public MonoBehaviour[] Movesets;
    public UpgradeSO[] upgrades;

    [SerializeField]private Stats basicStats;
    [SerializeField]private Stats statsToModify;



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
    [SerializeField]private bool busy;
    [SerializeField]private bool inAttack;

    






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.paused)
        {
            speed = 0;
            return;
        }
        speed = basicStats.speed;

        if (activated && !busy)
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
                    //Debug.Log("Esperando");
                    break;
            }
            StartCoroutine(Wait());
        }

        
    }



    public void UseMoveset()
    {
        int r = Random.Range(0, Movesets.Length);

        if (!inAttack)
        {
            inAttack = true;
            Movesets[r].StartCoroutine("Use");
        }
        
        //inAttack = false;

        //Debug.Log("Moveset: " + Movesets[r]);

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
        while (busy || inAttack)
        {

            //Debug.Log("esoerabdi");
            yield return null;

        }


        
        
        
        busy = true;
        yield return new WaitForSeconds(cooldownBetweenMovesets);
        actState = nextState;
        busy = false;

       
    }

    public IEnumerator Move(bool cancel = false)
    {
        
        //idle = true;
        float r = Random.Range(mapAnchor.x, mapAnchor.y);

        Vector3 destination = new Vector3(r,transform.position.y,transform.position.z);
        //Debug.Log("destinacion: "+destination);
        
        if (cancel)
        {

            destination = transform.position;
            
        }
        while (Vector3.Distance(transform.position,destination) > 0.2)
        {
            if (inAttack)
            {
                break;
            }
            //Debug.Log("distancia: " +Vector3.Distance(transform.position,destination));
            transform.Translate(-(destination - transform.position).normalized * Time.deltaTime * speed);
            yield return null;
        }

        //destination = transform;

        
    }

    public void ChangeBusy(bool isBusy)
    {
        busy = isBusy;
    }

    public void ChangeInAttack(bool isInAttack)
    {
        inAttack = isInAttack;
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
