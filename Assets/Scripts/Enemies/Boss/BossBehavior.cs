using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Stats 
{
    public float health;
    public float speed;


}




public class BossBehavior : MonoBehaviour
{

    [SerializeField]private string bossName;

    [SerializeField]private MonoBehaviour[] moveset;
    [SerializeField]private MonoBehaviour previousMove;
    [SerializeField] float acutalValues = 0;
    
    public UpgradeSO[] upgrades;

    [SerializeField]private Stats baseStats;
    [SerializeField]private Stats modStats;

    [SerializeField]private string finalDialogueName = "";



    public enum States
    {
        Move,
        Attack,
        Wait
    }

    public enum Event
    {
        Enter,
        Update,
        Exit
    }

    private States actState;
    private States nextState;




    [SerializeField]private float health;
    [SerializeField]private float speed;
    [SerializeField]private Vector2 mapAnchor;
    [SerializeField]private float cooldownBetweenMovesets;

    [SerializeField]private bool activated;
    [SerializeField]private bool busy;
    [SerializeField]private bool inAttack;


    private bool dead;

    






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetUpgrades();
        SendInfoToUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.paused)
        {
            return;
        }
        //speed = baseStats.speed;

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

    private void SendInfoToUI()
    {
        UIManager.instance.DisplayBossThings(bossName,health);
    }

    [ContextMenu("Get enemy upgrades")]
    public void GetUpgrades()
    {
        upgrades = UpgradesManager.instance.enemyUpgrades.ToArray();
        UpdateModStats();
        UpdateStats();
    }

    private void UpdateModStats()
    {
        foreach (UpgradeSO upgrade in upgrades)
        {
            if(upgrade.type == UpgradeSO.UpgradeType.StatModification)
            {
                for(int i = 0; i < upgrade.modify.Length; i++)
                {
                    if(upgrade.modify[i] == UpgradeSO.StatToModify.Speed)
                    {
                        modStats.speed += upgrade.valuesToAdd[i];
                    }
                    if(upgrade.modify[i] ==UpgradeSO.StatToModify.Health)
                    {
                        modStats.health += upgrade.valuesToAdd[i];
                    }
                }
            }
        }

    }

    private void UpdateStats()
    {
        speed = baseStats.speed + modStats.speed;
        health = baseStats.health + modStats.health;
    }

    public void UseMoveset()
    {
        int r = Random.Range(0, moveset.Length);

        while (previousMove == moveset[r])
        {
            r = Random.Range(0, moveset.Length);
        }

        if (!inAttack)
        {
            inAttack = true;
            moveset[r].StartCoroutine("Use");
            previousMove = moveset[r];
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
        
        //Debug.Log(r +" ");

        Vector3 destination = new Vector3(r,transform.position.y,transform.position.z);

        while (r > mapAnchor.y || r < mapAnchor.x)
        {
            r = Random.Range(mapAnchor.x, mapAnchor.y);
            
            
        }
        destination = new Vector3(r,transform.position.y,transform.position.z);
        Debug.Log(r +" " + destination);
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
            if (!GameManager.instance.paused)
            {
                transform.Translate(-(destination - transform.position).normalized * Time.deltaTime * speed);
                yield return null;
            }
            //Debug.Log("distancia: " +Vector3.Distance(transform.position,destination));
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
        health -= damage;
        UIManager.instance.HurtHealthBar(health);
        if (health < 0 && !dead)
        {   
            StartCoroutine(Kill());
            
        }
    }

    private IEnumerator Kill()
    {

        dead = true;
        DialoguesManager.instance.StartDialogue("AfterBoss");
        
        while (DialoguesManager.onDialogue)
        {
            yield return null;
        }
        GameManager.instance.Win();
        Destroy(gameObject);
            
        
    }

}
