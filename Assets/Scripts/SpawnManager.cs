using System;
using System.Linq;
using UnityEngine;



[Serializable]
public enum SpawnEventType
{
    Spawn,
    Dialogue,
    Goal,
    MusicChange,
    SpeedChange,
}
[Serializable]
public enum SpawnType
{
    Enemy,
    PowerUp,
    Obstacle,
    Boss
}
[Serializable]
public class EventSpawn
{
    public bool active = true;
    
    public float distanceToSpawn;

    public float timeThatWillSpawn;
    public SpawnEventType eventType;
    public SpawnType spawnType;



    [Header("If Spawn")]

    public GameObject prefab;
    [Header("If Dialogue"),Space(3)]

    public string dialogueName;


    [Header("If MusicChange"), Space(3)]
    public AudioClip newMusic;
    [Header("If SpeedChange"), Space(3)]
    public float newSpeed;
    

}



public class SpawnManager : MonoBehaviour
{

    public float distance = 0;
    public float speed = 5;
    public EventSpawn[] eventsToSpawn;
    public GameObject backgroundPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.paused)
        {
            return;
        }
        distance += Time.deltaTime * speed;

        backgroundPanel.GetComponent<Renderer>().material.SetFloat("_Speed", 0.5f);

        foreach (EventSpawn eventSpawn in eventsToSpawn)
        {
            if (distance >= eventSpawn.distanceToSpawn && eventSpawn.active)
            {
                switch (eventSpawn.eventType)
                {
                    case SpawnEventType.Spawn:
                        switch (eventSpawn.spawnType)
                        {
                            case SpawnType.Enemy:
                                Instantiate(eventSpawn.prefab, transform.position + new Vector3(0,0,15), Quaternion.identity);
                                break;
                            case SpawnType.PowerUp:
                                Instantiate(eventSpawn.prefab, transform.position + new Vector3(0,0,15), Quaternion.identity);
                                break;
                            case SpawnType.Obstacle:
                                Instantiate(eventSpawn.prefab, transform.position + new Vector3(0,0,15), Quaternion.identity);
                                break;
                            case SpawnType.Boss:
                                DialoguesManager.instance.StartDialogue("Boss");
                                Instantiate(eventSpawn.prefab, transform.position + new Vector3(0,0,15), Quaternion.identity);
                                break;
                        }
                        
                        break;
                    case SpawnEventType.Dialogue:
                        DialoguesManager.instance.StartDialogue(eventSpawn.dialogueName);
                        break;
                    case SpawnEventType.Goal:
                        GameManager.instance.Win();
                        break;
                    case SpawnEventType.MusicChange:
                        AudioManager.Instance.ChangeMusic(eventSpawn.newMusic);
                        break;
                    case SpawnEventType.SpeedChange:
                        speed = eventSpawn.newSpeed;
                        backgroundPanel.GetComponent<Renderer>().material.SetFloat("_Speed", speed);
                        break;
                }
                //eventSpawn.distanceToSpawn = float.MaxValue; // Para que no se vuelva a spawnear
                eventSpawn.active = false;
            }
        }
        
        
    }

    public void SpawnRemoteEvent(EventSpawn eventSpawn)
    {
        switch (eventSpawn.eventType)
        {
            case SpawnEventType.Spawn:
                switch (eventSpawn.spawnType)
                {
                    case SpawnType.Enemy:
                        Instantiate(eventSpawn.prefab, transform.position + new Vector3(0,0,15), Quaternion.identity);
                        break;
                    case SpawnType.PowerUp:
                        Instantiate(eventSpawn.prefab, transform.position + new Vector3(0,0,15), Quaternion.identity);
                        break;
                    case SpawnType.Obstacle:
                        Instantiate(eventSpawn.prefab, transform.position + new Vector3(0,0,15), Quaternion.identity);
                        break;
                    case SpawnType.Boss:
                        DialoguesManager.instance.StartDialogue("Boss");
                        Instantiate(eventSpawn.prefab, transform.position + new Vector3(0,0,15), Quaternion.identity);
                        break;
                }
                break;
            case SpawnEventType.Dialogue:
                DialoguesManager.instance.StartDialogue(eventSpawn.dialogueName);
                break;
            case SpawnEventType.Goal:
                GameManager.instance.Win();
                break;
            case SpawnEventType.MusicChange:
                AudioManager.Instance.ChangeMusic(eventSpawn.newMusic);
                break;
            case SpawnEventType.SpeedChange:
                speed = eventSpawn.newSpeed;
                break;
        }
    }

    [ContextMenu("Order Spawn Events")]
    public void OrderSpawnEvents()
    {
        eventsToSpawn = eventsToSpawn.OrderBy(e => e.distanceToSpawn).ToArray();
        for (int i = 0; i < eventsToSpawn.Length; i++)
        {
            if (i == 0)
            {
                eventsToSpawn[i].timeThatWillSpawn = eventsToSpawn[i].distanceToSpawn / speed;
                continue;
            }
            else
            {
                eventsToSpawn[i].timeThatWillSpawn = (eventsToSpawn[i].distanceToSpawn - eventsToSpawn[i-1].distanceToSpawn) / (speed + eventsToSpawn[i-1].newSpeed);
            }
            
            
        }
    }

    void OnValidate()
    {

        for (int i = 0; i < eventsToSpawn.Length; i++)
        {
            if (i == 0)
            {
                eventsToSpawn[i].timeThatWillSpawn = eventsToSpawn[i].distanceToSpawn / speed;
                continue;
            }
            else
            {
                eventsToSpawn[i].timeThatWillSpawn = (eventsToSpawn[i].distanceToSpawn - eventsToSpawn[i-1].distanceToSpawn) / (speed + eventsToSpawn[i-1].newSpeed);
            }
            
            
        }
        foreach (EventSpawn eventSpawn in eventsToSpawn)
        {
            if (eventSpawn.eventType != SpawnEventType.SpeedChange)
            {
                eventSpawn.newSpeed = 0;
            }
        }
        
    }

}
